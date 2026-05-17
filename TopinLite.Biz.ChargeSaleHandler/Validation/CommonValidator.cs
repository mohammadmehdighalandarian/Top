using System.Globalization;
using NatsRpcFoundation.Abstractions;
using NatsRpcFoundation.Contracts;
using TopinLite.Biz.ChargeSaleHandler.Orders;
using TopinLite.Domain.Commons;
using TopinLite.Domain.Messaging;
using TopinLite.Domain.TopinApi;
using TopinLite.Infra.Common.Utilities;

namespace TopinLite.Biz.ChargeSaleHandler.Validation;

public interface ICommonValidator
{
    Task<ExecResult> ChargeRequestValidateAsync(ChargeRequestOrderRequest request, CancellationToken ct);
    Task<ExecResult> ChargeConfirmValidateAsync(ChargeConfirmOrderRequest request, CancellationToken ct);
}

public sealed class CommonValidator : ICommonValidator
{
    private readonly IRpcClient _rpc;
    private readonly IChargeTypeResolver _resolver;
    private readonly IOrderStore _store;

    public CommonValidator(IRpcClient rpc, IChargeTypeResolver resolver, IOrderStore store)
    {
        _rpc = rpc;
        _resolver = resolver;
        _store = store;
    }

    public async Task<ExecResult> ChargeRequestValidateAsync(ChargeRequestOrderRequest request, CancellationToken ct)
    {
        CheckTelNumAndChannel(request.TelNum, request.TelGift, request.PayloadId, decimal.Parse(request.ChannelId));

        #region DynamicConditions

        DynamicConditionsResponseModel? dc1 = await TryRpcAsync<DynamicConditionsRequestModel, DynamicConditionsResponseModel>(
            "infra.cache.dynamic-conditions",
            new DynamicConditionsRequestModel { Biztype = "999", KeyStr = "PKG_PINLESS_CHARGE.CALL_SALE_PROVIDER_STOPED" },
            ct).ConfigureAwait(false);
        DynamicConditionsResponseModel? dc2 = await TryRpcAsync<DynamicConditionsRequestModel, DynamicConditionsResponseModel>(
            "infra.cache.dynamic-conditions",
            new DynamicConditionsRequestModel { Biztype = "997", KeyStr = "PKG_PINLESS_CHARGE.CALL_SALE_PROVIDER_BROKERS" },
            ct).ConfigureAwait(false);

        if (dc1?.ValueStr != "1" && dc2?.ValueStr == "SAPID" /* TODO SapId Should be provided by Redis */)
            return Fail(ResultCodes.ChargeDynamicConditions);

        #endregion

        await CheckBroker(ct).ConfigureAwait(false);

        await CheckOffer(decimal.Parse(request.PayloadId), decimal.Parse(request.Amount), decimal.Parse(request.ProductId), ct).ConfigureAwait(false);

        CheckWll(request.TelGift.ToString(), decimal.Parse(request.ProductId), decimal.Parse(request.PayloadId));

        await CheckBrokerOfferAccess(decimal.Parse(request.PayloadId), decimal.Parse(request.ProductId), decimal.Parse(request.Gprs), decimal.Parse(request.Voice),
            decimal.Parse(request.Sms), ct);

        await CheckIntegrationEnquiry(request.TelGift, decimal.Parse(request.ProductId), decimal.Parse(request.Amount), ct);

        return new ExecResult
        {
            ExecStatus = true,
            ResultCode = ResultCodes.Success.Code,
            ResultMessage = ResultCodes.Success.Message
        };
    }

    public async Task<ExecResult> ChargeConfirmValidateAsync(ChargeConfirmOrderRequest request, CancellationToken ct)
    {
        OrderContext? order = await _store.TryGetAsync(request.OrderId, ct);
        if (order is null)
            return Fail(ResultCodes.OrderNotFound);

        switch (order.ChargeStatus)
        {
            case 1:
                return Fail(ResultCodes.AlreadySucceeded);
            case 2:
                return Fail(ResultCodes.OngoingChargeOrder);
        }

        if ((string.IsNullOrEmpty(request.CardNo) && !string.IsNullOrEmpty(request.CardType)) ||
            (string.IsNullOrEmpty(request.CardType) && !string.IsNullOrEmpty(request.CardNo)))
            return Fail(ResultCodes.CardTypeCardNoRestriction);

        if (!string.IsNullOrEmpty(request.CardNo) && !string.IsNullOrEmpty(request.CardType))
            if (StaticCardTypes.Items.All(x => x.CardTypeId != decimal.Parse(request.CardType)))
                return Fail(ResultCodes.BadInput);

        if (StaticBanks.Items.All(x => x.BankId != decimal.Parse(request.BankCode)))
            return Fail(ResultCodes.WrongBankCode);
        if (StaticBanks.Items.Any(x => x.BankId == decimal.Parse(request.BankCode) && !x.BankStatus))
            return Fail(ResultCodes.BankInactive);
        
        await CheckBroker(ct).ConfigureAwait(false);

        await CheckBrokerSaleLimit(order.FkBrokerId, request.OrderId, (int)Products.Charge, ct).ConfigureAwait(false);

        await CheckOffer(order.OfferCode, order.ChargeAmount, (int)Products.Charge, ct);

        await CheckBrokerOfferAccess(order.OfferCode, (int)Products.Charge, 0, 0, 0, ct);
            
        return Success();
    }

    private async Task<ExecResult> CheckBrokerSaleLimit(decimal brokerId, decimal orderId, int productId,
        CancellationToken ct)
    {
        BrokerSaleLimitResponseModel? brokerSaleLimit = await TryRpcAsync<BrokerSaleLimitRequestModel, BrokerSaleLimitResponseModel>(
            "infra.cache.broker-sale-limit",
            new BrokerSaleLimitRequestModel { BrokerId = brokerId },
            ct).ConfigureAwait(false);
        if (brokerSaleLimit is null)
            return Fail(ResultCodes.OrderNotFound);

        if (productId == (int)Products.Charge)
        {
            OrderContext? order = await _store.TryGetAsync(orderId, ct);
            if (order is null)
                return Fail(ResultCodes.OrderNotFound);

            if (order.FkBrokerId != brokerId)
                return Fail(ResultCodes.BrokerIdMismatch);
            if (order.Retry >= brokerSaleLimit.RetryLimit)
                return Fail(ResultCodes.MaxTryExceeded);

            DateTime insDateTime = Service.ParseJalaliDateTime(order.InsDate, order.InsTime);
            if (insDateTime.AddMinutes((double)brokerSaleLimit.TimeLimit) < DateTime.Now)
                return Fail(ResultCodes.TimeExceeded);
        }

        return Success();
    }

    private static ExecResult CheckWll(string telGift, decimal productId, decimal offerCode)
    {
        var telGiftSub = telGift.Substring(1, 4);
        if (StaticWllPrefixes.Items.Any(x => x.PkWllPrefix.ToString() == telGiftSub))
        {
            if (offerCode is 1001 or 1002)
                return Fail(ResultCodes.WllRestriction);
        }

        return Success();
    }

    private async Task<ExecResult> CheckBroker(CancellationToken ct)
    {
        BrokersResponseModel? broker = await TryRpcAsync<BrokersRequestModel, BrokersResponseModel>(
            "infra.cache.brokers",
            new BrokersRequestModel { SapId = 0 /* TODO SapId Should be provided by Redis */ },
            ct).ConfigureAwait(false);

        if (broker is null)
            return Fail(ResultCodes.BrokerNotFound);
        if (broker.Status == 0)
            return Fail(ResultCodes.BrokerInactive);

        return Success();
    }

    private ExecResult CheckTelNumAndChannel(decimal telNum, decimal telGift, string payloadId, decimal channelId)
    {
        if (string.IsNullOrWhiteSpace(payloadId) || _resolver.Resolve(payloadId) is null || telNum <= 0 || telGift <= 0)
            return Fail(ResultCodes.BadInput);

        if (telNum.ToString()[..3] == "932" || telNum.ToString()[..5] == "98932" || telGift.ToString()[..3] == "932" ||
            telGift.ToString()[..5] == "98932")
            return Fail(ResultCodes.TaliyaError);

        if (telNum.ToString().Length < 2)
            return Fail(ResultCodes.TelNumEmpty);

        if (StaticChannels.Items.All(x => x.ChannelId != channelId))
            return Fail(ResultCodes.ChannelNotFound);

        return Success();
    }

    private async Task<ExecResult> CheckOffer(decimal offerCode, decimal amount, decimal productId, CancellationToken ct)
    {
        OfferResponseModel? offer = await TryRpcAsync<OfferRequestModel, OfferResponseModel>(
            "infra.cache.offers",
            new OfferRequestModel { OfferCode = offerCode },
            ct).ConfigureAwait(false);

        if (offer is null || offer.Status != 1)
            return Fail(ResultCodes.OfferInactive);
        if (productId != offer.Type)
            return Fail(ResultCodes.OfferProductMismatch);
        if (amount != offer.Price)
            return Fail(ResultCodes.OfferAmountMismatch);

        DynamicConditionsResponseModel? coefCharge = await TryRpcAsync<DynamicConditionsRequestModel, DynamicConditionsResponseModel>(
            "infra.cache.dynamic-conditions",
            new DynamicConditionsRequestModel { Biztype = "125", KeyStr = "COEF_CHARGE" },
            ct).ConfigureAwait(false);
        DynamicConditionsResponseModel? minCharge = await TryRpcAsync<DynamicConditionsRequestModel, DynamicConditionsResponseModel>(
            "infra.cache.dynamic-conditions",
            new DynamicConditionsRequestModel { Biztype = "125", KeyStr = "MIN_CHARGE" },
            ct).ConfigureAwait(false);
        DynamicConditionsResponseModel? maxCharge = await TryRpcAsync<DynamicConditionsRequestModel, DynamicConditionsResponseModel>(
            "infra.cache.dynamic-conditions",
            new DynamicConditionsRequestModel { Biztype = "125", KeyStr = "MAX_CHARGE" },
            ct).ConfigureAwait(false);

        if (amount < decimal.Parse(minCharge.ValueStr))
            return Fail(ResultCodes.MinAmountCharge);
        if (amount > decimal.Parse(maxCharge.ValueStr) || amount % decimal.Parse(coefCharge.ValueStr) != 0)
            return Fail(ResultCodes.InvalidChargeAmount);

        return Success();
    }

    private async Task<ExecResult> CheckBrokerOfferAccess(decimal offerCode, decimal productId, decimal data, decimal voice, decimal sms, CancellationToken ct)
    {
        BrokersResponseModel? broker = await TryRpcAsync<BrokersRequestModel, BrokersResponseModel>(
            "infra.cache.brokers",
            new BrokersRequestModel { SapId = 0 /* TODO SapId Should be provided by Redis */ },
            ct).ConfigureAwait(false);

        if (broker is null)
            return Fail(ResultCodes.BrokerNotFound);
        if (broker.Status == 0)
            return Fail(ResultCodes.BrokerInactive);

        OfferResponseModel? offer = await TryRpcAsync<OfferRequestModel, OfferResponseModel>(
            "infra.cache.offers",
            new OfferRequestModel { OfferCode = offerCode },
            ct).ConfigureAwait(false);
        if (offer is null || offer.Status != 1)
            return Fail(ResultCodes.OfferInactive);

        BrokerOfferAccessResponseModel? brokerOfferAccess = await TryRpcAsync<BrokerOfferAccessRequestModel, BrokerOfferAccessResponseModel>(
            "infra.cache.broker-offer-access",
            new BrokerOfferAccessRequestModel { BrokerId = broker.BrokerId, OfferId = offer.OfferId },
            ct).ConfigureAwait(false);

        if (brokerOfferAccess is null)
        {
            return productId switch
            {
                (int)Products.Charge => Fail(ResultCodes.BrokerHasNoChargeAccess),
                (int)Products.Package => Fail(ResultCodes.BrokerHasNoPackageAccess),
                _ => Fail(ResultCodes.BrokerHasNoChargeAccess)
            };
        }

        if ((offer.Type == (int)Products.Charge && !new[] { 1, 3, 5, 7 }.Contains((int)broker.SaleAccess)) ||
            (offer.Type == (int)Products.Package && !new[] { 2, 3, 6, 7 }.Contains((int)broker.SaleAccess)) ||
            (offer.Type == (int)Products.Anarestan && !new[] { 4, 5, 6, 7 }.Contains((int)broker.SaleAccess)))
            return Fail(ResultCodes.BrokerHasNoProductAccess);

        if (broker.SaleType != 1 && offer.RelationId == 33)
            return Fail(ResultCodes.BrokerHasNoPackagePermission);

        // if (type == 2)
        // {
        //     if (offerCategory == 12)
        //     {
        //         var diyDataRpc = await _rpcClient
        //             .RequestAsync<DiyDataAccessRequestModel, DiyDataAccessResponseModel>(
        //                 subject: "infra.cache.diy-data-access",
        //                 request: new DiyDataAccessRequestModel
        //                 {
        //                     BrokerId = (long)brokerId,
        //                     OfferId = (long)offerId,
        //                     AttributeType = 1,
        //                     AttributeVal = data
        //                 },
        //                 cancellationToken: cancellationToken)
        //             .ConfigureAwait(false);
        //
        //         if (!diyDataRpc.Success || diyDataRpc.Data is null)
        //             return Error(-1099, "Broker does not have DIY data access.");
        //
        //         var diyVoiceRpc = await _rpcClient
        //             .RequestAsync<DiyDataAccessRequestModel, DiyDataAccessResponseModel>(
        //                 subject: "infra.cache.diy-data-access",
        //                 request: new DiyDataAccessRequestModel
        //                 {
        //                     BrokerId = (long)brokerId,
        //                     OfferId = (long)offerId,
        //                     AttributeType = 2,
        //                     AttributeVal = voice
        //                 },
        //                 cancellationToken: cancellationToken)
        //             .ConfigureAwait(false);
        //
        //         if (!diyVoiceRpc.Success || diyVoiceRpc.Data is null)
        //             return Error(-1099, "Broker does not have DIY voice access.");
        //
        //         var diySmsRpc = await _rpcClient
        //             .RequestAsync<DiyDataAccessRequestModel, DiyDataAccessResponseModel>(
        //                 subject: "infra.cache.diy-data-access",
        //                 request: new DiyDataAccessRequestModel
        //                 {
        //                     BrokerId = (long)brokerId,
        //                     OfferId = (long)offerId,
        //                     AttributeType = 3,
        //                     AttributeVal = sms
        //                 },
        //                 cancellationToken: cancellationToken)
        //             .ConfigureAwait(false);
        //
        //         if (!diySmsRpc.Success || diySmsRpc.Data is null)
        //             return Error(-1099, "Broker does not have DIY SMS access.");
        //     }
        //
        //     if (broker.BrokerType == 2)
        //     {
        //         if (offer.BrokerType != 2 && offer.BrokerType != 3)
        //             return Fail(ResultCodes.BrokerHasNoPackageAccess);
        //     }
        // }

        return Success();
    }

    private async Task<ExecResult> CheckIntegrationEnquiry(decimal telGift, decimal productId, decimal amount, CancellationToken ct)
    {
        List<PrimaryOfferResponseModel>? primaryOffers = await TryRpcAsync<PrimaryOfferRequestModel, List<PrimaryOfferResponseModel>>(
            "infra.cache.primary-offers",
            new PrimaryOfferRequestModel(),
            ct).ConfigureAwait(false);
        if (primaryOffers is null || primaryOffers.Count == 0)
            return Fail(ResultCodes.BadInput);

        IntegrationEnqueryResponseModel? integrationEnqueryResult = await TryRpcAsync<IntegrationEnqueryRequestModel, IntegrationEnqueryResponseModel>(
            "crm.integrationenquery",
            new IntegrationEnqueryRequestModel
            {
                PrimaryIdentity = telGift.ToString(),
                PrimaryOffers = primaryOffers.Select(x => new IntegrationEnquiryRequestPrimaryOffers { Type = x.Type, Offer = x.OfferId }).ToList()
            },
            ct).ConfigureAwait(false);
        if (integrationEnqueryResult is null)
            return Fail(ResultCodes.BadInput);

        switch (integrationEnqueryResult.ResponseType)
        {
            case "-1042":
                return Fail(ResultCodes.ProcessHaltedDueToSpecificCut);
            case "-1012":
                return Fail(ResultCodes.PhoneNumberIsBlacklisted);
            case "-1043":
                return Fail(ResultCodes.ProcessHaltedDueToMissingStatus);
            case "-1070":
                return Fail(ResultCodes.ProcessHaltedDueToPortability);
            case "-1100":
                return Fail(ResultCodes.SimCardNotEligibleForPurchase);
        }

        // for charge sale
        if (integrationEnqueryResult.ResponseType == "0" && productId == (int)Products.Charge && integrationEnqueryResult.ResponseDesc is not null)
        {
            string simType = TokenParser.GetToken(integrationEnqueryResult.ResponseDesc, 2);
            decimal balance = decimal.Parse(TokenParser.GetToken(integrationEnqueryResult.ResponseDesc, 3));
            decimal subscriberStatus = decimal.Parse(TokenParser.GetToken(integrationEnqueryResult.ResponseDesc, 4));

            if (simType == "BC")
                return Fail(ResultCodes.NoChargeForPermanentSim);

            if (amount + balance > 10000000)
                return Fail(ResultCodes.SubscriberOverCredit);

            switch (subscriberStatus)
            {
                case 1:
                    return Fail(ResultCodes.ProcessHaltDueToSubscriberInactivity);
                case 5:
                    return Fail(ResultCodes.ProcessHaltDueToSubscriberEvacuation);
            }
        }

        //for package sale
        if (integrationEnqueryResult.ResponseType == "0" && productId == (int)Products.Package && integrationEnqueryResult.ResponseDesc is not null)
        {
            // TODO validations on integration enquiry for package sale
        }

        return Success();
    }

    private async Task<TResp?> TryRpcAsync<TReq, TResp>(string subject, TReq request, CancellationToken ct)
        where TReq : class
        where TResp : class
    {
        try
        {
            RpcResult<TResp> result = await _rpc.RequestAsync<TReq, TResp>(subject: subject, request: request,
                options: new RpcCallOptions { Timeout = TimeSpan.FromSeconds(3) }, cancellationToken: ct).ConfigureAwait(false);

            return result.Success ? result.Data : null;
        }
        catch
        {
            return null;
        }
    }

    private static ExecResult Fail(ResultCode rc)
        => new()
        {
            ExecStatus = false,
            ResultCode = rc.Code,
            ResultMessage = rc.Message
        };

    private static ExecResult Success()
        => new()
        {
            ExecStatus = true,
            ResultCode = ResultCodes.Success.Code,
            ResultMessage = ResultCodes.Success.Message
        };
    
    
}