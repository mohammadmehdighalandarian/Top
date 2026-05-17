using Polly;
using TopinLite.Biz.PackageSaleHandler.PackageOrder;

namespace TopinLite.Biz.PackageSaleHandler.Validation;

public class PackageCommonValidation
{
    private readonly IRpcClient _rpcClient;
    private readonly ILogger<PackageCommonValidation> _logger;
    private readonly IPackageOrderStore _packageOrderStore;

    public PackageCommonValidation(IRpcClient rpcClient, ILogger<PackageCommonValidation> logger, IPackageOrderStore packageOrderStore)
    {
        _rpcClient = rpcClient;
        _logger = logger;
        _packageOrderStore = packageOrderStore;
    }

    public async Task<ExecResult> ValidateBusinessAsync(PackageRequestModel request, CancellationToken cancellationToken)
    {
        string tellNumStr = request.Request.TelNum.ToString();
        string tellGiftStr = request.Request.TelGift.ToString();

        var checkTellNum = CheckNullValue(tellNumStr, "TEL_NUM");
        if (!checkTellNum.ExecStatus)
            return checkTellNum;

        var checkTellGift = CheckNullValue(tellGiftStr, "TEL_GIFT");
        if (!checkTellGift.ExecStatus)
            return checkTellGift;

        var checkTaliaTellnum = CheckTalia(tellNumStr);
        if (!checkTaliaTellnum.ExecStatus)
            return checkTaliaTellnum;

        var checkTaliaTellGift = CheckTalia(tellGiftStr);
        if (!checkTaliaTellGift.ExecStatus)
            return checkTaliaTellGift;

        var checkWll = await CheckWllAsync(tellGiftStr, 2, request.Offer.OfferId, cancellationToken);
        if (!checkWll.ExecStatus)
            return checkWll;


        var CheckChannelId = await CheckChannelIdAsync(request.Request.ChannelId, cancellationToken);
        if (!CheckChannelId.ExecStatus)
            return CheckChannelId;

        var brokerStatus = await CheckBrokerStatus(request.Request.SapId, request.Request.SaleType, cancellationToken);
        if (!brokerStatus.ExecStatus)
            return brokerStatus;

        var offerStatus = await CheckOfferStatusAsync(request.Offer.OfferCode, 2, request.Request.Amount, request.Request.Data, request.Request.Voice, request.Request.Sms, cancellationToken);
        if (!offerStatus.ExecStatus)
            return new() { ExecStatus = offerStatus.ExecStatus, ResultCode = offerStatus.ResultCode, ResultMessage = offerStatus.ResultMessage };

        var brokerAccess = await CheckBrokerAccessAsync(request.Broker.BrokerId, request.Offer.OfferId, 2, request.Request.Data, request.Request.Voice, request.Request.Sms, cancellationToken);
        if (!brokerAccess.ExecStatus)
            return brokerAccess;


        return Success();
    }

    public async Task<ExecResult> ValidateCallSaleAnarestanAsync(PackageRequestModel request, CancellationToken cancellationToken)
    {
        string tellNumStr = request.Request.TelNum.ToString();
        string tellGiftStr = request.Request.TelGift.ToString();

        var checkTellNum = CheckNullValue(tellNumStr, "TEL_NUM");
        if (!checkTellNum.ExecStatus)
            return checkTellNum;

        var checkTellGift = CheckNullValue(tellGiftStr, "TEL_GIFT");
        if (!checkTellGift.ExecStatus)
            return checkTellGift;

        var checkTellNumAndGiftNumMatch = CheckTellNumAndGiftNumMatch(tellNumStr, tellGiftStr);
        if (!checkTellNumAndGiftNumMatch.ExecStatus)
            return checkTellNumAndGiftNumMatch;

        var result = await CheckBrokerStatus(request.Broker.SapId, request.Request.SaleType, cancellationToken);
        if (!result.ExecStatus)
            return result;


        ExecResult<decimal> offerStatus = await CheckOfferStatusAsync(request.Offer.OfferCode, 2, request.Request.Amount, request.Request.Data, request.Request.Voice, request.Request.Sms, cancellationToken);
        if (!offerStatus.ExecStatus)
            return new() { ExecStatus = offerStatus.ExecStatus, ResultCode = offerStatus.ResultCode, ResultMessage = offerStatus.ResultMessage };

        ExecResult brokerAccess = await CheckBrokerAccessAsync(request.Broker.BrokerId, request.Offer.OfferId, 2, request.Request.Data, request.Request.Voice, request.Request.Sms, cancellationToken).ConfigureAwait(false);
        if (!brokerAccess.ExecStatus)
            return brokerAccess;

        return Success();
    }

    public async Task<ExecResult> ValidateExecBusinessAsync(PackageConfirmOrderValidationRequest request, CancellationToken cancellationToken)
    {

        if (request.ProviderId <= 0)
        {
            _logger.LogWarning("ValidatePackage: ProviderId is null/invalid. OrderId={OrderId}", request.ProviderId);
            return Error(ResultCodes.UniqueOrderNotFound.Code, ResultCodes.UniqueOrderNotFound.Message);
        }
        if (request.PackageRequest is null)
            return Error(ResultCodes.UniqueOrderNotFound.Code, ResultCodes.UniqueOrderNotFound.Message);

        if (request.PackageConfirm is null)
            return Error(ResultCodes.UniqueOrderNotFound.Code, ResultCodes.UniqueOrderNotFound.Message);


        if ((string.IsNullOrEmpty(request.PackageConfirm.CardNo.ToString()) && !string.IsNullOrEmpty(request.PackageConfirm.CardType.ToString())) ||
            (string.IsNullOrEmpty(request.PackageConfirm.CardType.ToString()) && !string.IsNullOrEmpty(request.PackageConfirm.CardNo.ToString())))
            return Error(ResultCodes.CardTypeCardNoRestriction.Code, ResultCodes.CardTypeCardNoRestriction.Message);

        if (!string.IsNullOrEmpty(request.PackageConfirm.CardNo.ToString()) && !string.IsNullOrEmpty(request.PackageConfirm.CardType.ToString()))
            if (StaticCardTypes.Items.All(x => x.CardTypeId != decimal.Parse(request.PackageConfirm.CardType.ToString())))
                return Error(ResultCodes.BadInput.Code, ResultCodes.BadInput.Message);

        ExecResult<ProviderIdCheckResponseModel> providerCheck = await CheckProviderIdAsync(
            new ProviderIdCheckRequestModel
            {
                ProviderId = request.ProviderId,
                Type = 2
            },
            cancellationToken).ConfigureAwait(false);

        if (!providerCheck.ExecStatus)
        {
            _logger.LogWarning("ValidatePackage: Provider ID check failed. Code={Code}", providerCheck.ResultCode);
            return Error(providerCheck.ResultCode, providerCheck.ResultMessage);
        }


        if (StaticBanks.Items.All(x => x.BankId != decimal.Parse(request.PackageConfirm.BankCode.ToString())))
            return Error(ResultCodes.WrongBankCode.Code, ResultCodes.WrongBankCode.Message);
        if (StaticBanks.Items.Any(x => x.BankId == decimal.Parse(request.PackageConfirm.BankCode.ToString()) && !x.BankStatus))
            return Error(ResultCodes.BankInactive.Code, ResultCodes.BankInactive.Message);


        ExecResult saleConditionCheck = CheckSaleCondition(
        type: 2,
        providerId: context.Request.ProviderId,
        sapId: context.Request.SapId,
        sale: context.Sale,
        broker: context.Broker);

        if (!saleConditionCheck.ExecStatus)
        {
            _logger.LogWarning(
                "ValidatePackage: Gate 5 failed — Sale condition failed. Code={Code}",
                saleConditionCheck.ResultCode);
            return saleConditionCheck;
        }


        ExecResult salePackageCheck = await ValidateExecSalePackageAsync(request.PackageRequest, cancellationToken)
            .ConfigureAwait(false);

        if (!salePackageCheck.ExecStatus)
            return salePackageCheck;

        return Success();
    }

    public async Task<ExecResult> ValidateExecSalePackageAsync(PackageRequestModel context, CancellationToken cancellationToken)
    {
        var saleConditionRpc = await _rpcClient
            .RequestAsync<CheckSaleConditionRequestModel, CheckSaleConditionResponseModel>(
                subject: "biz.packagesale",
                request: new CheckSaleConditionRequestModel
                {
                    Type = 2,
                    ProviderId = context.Request.ProviderId,
                    SapId = context.Request.SapId
                },
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (!saleConditionRpc.Success || saleConditionRpc.Data?.ResponseType != 0)
        {
            _logger.LogWarning("ValidatePackage: Sale condition check failed. Code={Code}",
                saleConditionRpc.Data?.ResponseType);
            return Error(saleConditionRpc.Data?.ResponseType ?? ResultCodes.SystemError.Code,
                         saleConditionRpc.Data?.ResponseDesc);
        }

        var brokerStatusRpc = await _rpcClient
            .RequestAsync<CheckBrokerStatusRequestModel, CheckBrokerStatusResponseModel>(
                subject: "biz.packagesale",
                request: new CheckBrokerStatusRequestModel
                {
                    BrokerId = context.Broker.BrokerId,
                    SaleType = context.Request.SaleType
                },
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (!brokerStatusRpc.Success || brokerStatusRpc.Data?.ResponseType != 0)
        {
            _logger.LogWarning("ValidatePackage: Broker status check failed. Code={Code}",
                brokerStatusRpc.Data?.ResponseType);
            return Error(brokerStatusRpc.Data?.ResponseType ?? ResultCodes.SystemError.Code,
                         brokerStatusRpc.Data?.ResponseDesc);
        }

        var diyCheck = await CheckDiyOfferAsync(
            context.Offer.OfferId,
            context.Request.ProviderId,
            cancellationToken).ConfigureAwait(false);

        decimal diyData = diyCheck?.Data ?? 0;
        decimal diyVoice = diyCheck?.Voice ?? 0;
        decimal diySms = diyCheck?.Sms ?? 0;

        var offerStatusRpc = await _rpcClient
            .RequestAsync<CheckOfferStatusRequestModel, CheckOfferStatusResponseModel>(
                subject: "biz.packagesale",
                request: new CheckOfferStatusRequestModel
                {
                    OfferId = context.Offer.OfferId,
                    Type = 2,
                    Amount = context.Request.Amount,
                    Data = diyData,
                    Voice = diyVoice,
                    Sms = diySms
                },
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (!offerStatusRpc.Success || offerStatusRpc.Data?.ResponseType != 0)
        {
            _logger.LogWarning("ValidatePackage: Offer status check failed. Code={Code}",
                offerStatusRpc.Data?.ResponseType);
            return Error(offerStatusRpc.Data?.ResponseType ?? ResultCodes.SystemError.Code,
                         offerStatusRpc.Data?.ResponseDesc);
        }

        ExecResult brokerAccessCheck = await CheckBrokerAccessAsync(
            brokerId: context.Broker.BrokerId,
            offerId: context.Offer.OfferId,
            type: 2,
            data: diyData,
            voice: diyVoice,
            sms: diySms,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        if (!brokerAccessCheck.ExecStatus)
        {
            _logger.LogWarning("ValidatePackage: Broker access check failed. Code={Code}",
                brokerAccessCheck.ResultCode);
            return brokerAccessCheck;
        }

        return Success();

    }

    public async Task<ExecResult<ProviderIdCheckResponseModel>> CheckProviderIdAsync(
        ProviderIdCheckRequestModel request,
        CancellationToken cancellationToken)
    {
        var result = new ProviderIdCheckResponseModel();

        if (request.ProviderId <= 0)
            return new()
            {
                ExecStatus = false,
                ResultCode = -1085,
                ResultMessage = "Provider ID was not found.",
                Data = result
            };

        var saleRpc = await _rpcClient
            .RequestAsync<ProviderIdCheckRequestModel, ProviderIdCheckResponseModel>(
                subject: "biz.providersale.provider-id",
                request: new ProviderSaleLookupRequestModel
                {
                    ProviderId = request.ProviderId,
                    Type = request.Type
                },
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (!saleRpc.Success || saleRpc.Data is null)
            return ProviderError(-1085, "Provider ID was not found.", result);

        ProviderSaleLookupResponseModel sale = saleRpc.Data;
        result.OfferId = sale.OfferId;
        result.BrokerId = sale.BrokerId;
        result.Amount = sale.Amount;

        decimal responseType = await ResolveProviderResponseTypeAsync(
            request.Type,
            request.ProviderId,
            sale,
            cancellationToken).ConfigureAwait(false);

        result.ResponseType = responseType;

        return responseType == 0
            ? new ExecResult<ProviderIdCheckResult>
            {
                ExecStatus = true,
                ResultCode = 0,
                ResultMessage = "OK",
                Data = result
            }
            : ProviderError(responseType, null, result);
    }

    private async Task<decimal> ResolveProviderResponseTypeAsync(
        int type,
        decimal providerId,
        ProviderSaleLookupResponseModel sale,
        CancellationToken cancellationToken)
    {
        return type switch
        {
            1 => await ResolveChargeProviderResponseTypeAsync(providerId, sale.SaleStatus, cancellationToken)
                .ConfigureAwait(false),
            2 => await ResolvePackageProviderResponseTypeAsync(providerId, sale, cancellationToken)
                .ConfigureAwait(false),
            4 => await ResolveServiceProviderResponseTypeAsync(providerId, sale, cancellationToken)
                .ConfigureAwait(false),
            _ => 0
        };
    }

    private async Task<ExecResult> CheckBrokerSaleLimit(decimal brokerId, decimal orderId, int productId, CancellationToken cancellationToken)
    {
        var brokerSaleLimit = await _rpcClient.RequestAsync<BrokerSaleLimitRequestModel, BrokerSaleLimitResponseModel>(
                 subject: "infra.cache.broker-sale-limit",
                 request: new BrokerSaleLimitRequestModel
                 {
                     BrokerId = brokerId
                 },
                 cancellationToken: cancellationToken)
             .ConfigureAwait(false);


        if (brokerSaleLimit is null)
            return Error(ResultCodes.OrderNotFound.Code, ResultCodes.OrderNotFound.Message);

        if (productId == (int)Products.Charge)
        {
            PackageOrderContext? order = await _packageOrderStore.GetPackageSaleAsync(orderId, cancellationToken);
            if (order is null)
                return Error(ResultCodes.OrderNotFound.Code, ResultCodes.OrderNotFound.Message);

            if (order.BrokerId != brokerId)
                return Error(ResultCodes.BrokerIdMismatch.Code, ResultCodes.BrokerIdMismatch.Message);
            if (order..Retry >= brokerSaleLimit.Data.RetryLimit)
                return Error(ResultCodes.MaxTryExceeded.Code, ResultCodes.MaxTryExceeded.Message);

            DateTime insDateTime = Service.ParseJalaliDateTime(order.Value.InsDate, order.Value.InsTime);
            if (insDateTime.AddMinutes((double)brokerSaleLimit.Data.TimeLimit) < DateTime.Now)
                return Error(ResultCodes.TimeExceeded.Code, ResultCodes.TimeExceeded.Message);
        }

        return Success();
    }
    private async Task<decimal> ResolveChargeProviderResponseTypeAsync(
        decimal providerId,
        decimal saleStatus,
        CancellationToken cancellationToken)
    {
        if (saleStatus == 1)
        {
            await UpdateProviderRetryAsync(providerId, 1, cancellationToken).ConfigureAwait(false);
            return 1;
        }

        return saleStatus == 2 ? -1017 : 0;
    }

    private async Task<decimal> ResolvePackageProviderResponseTypeAsync(
        decimal providerId,
        ProviderSaleLookupResponseModel sale,
        CancellationToken cancellationToken)
    {
        if (sale.SaleStatus == 1)
        {
            await UpdateProviderRetryAsync(providerId, 2, cancellationToken).ConfigureAwait(false);
            return 1;
        }

        decimal category = sale.Category ?? 0;

        if (sale.SaleStatus == -1)
        {
            if (category == 4)
                return -1090;

            if (category == 3)
                return -1076;

            return 0;
        }

        if (sale.SaleStatus == 2)
            return -1096;

        if (category == 3)
        {
            int reservedCount = await GetReservedLoyaltyPackageCountAsync(
                sale.TelNum ?? 0,
                sale.OfferId,
                providerId,
                cancellationToken).ConfigureAwait(false);

            if (reservedCount == 0)
                return -1076;
        }

        return 0;
    }

    private async Task<decimal> ResolveServiceProviderResponseTypeAsync(
        decimal providerId,
        ProviderSaleLookupResponseModel sale,
        CancellationToken cancellationToken)
    {
        if (sale.SaleStatus == 1)
        {
            await UpdateProviderRetryAsync(providerId, 4, cancellationToken).ConfigureAwait(false);
            return 1;
        }

        if (sale.SaleStatus == -1)
            return -1076;

        return sale.SaleStatus == 2 ? -1096 : 0;
    }

    private async Task UpdateProviderRetryAsync(decimal providerId, int type, CancellationToken cancellationToken)
    {
        await _rpcClient
            .RequestAsync<ProviderRetryUpdateRequestModel, object>(
                subject: "biz.providersale.retry",
                request: new ProviderRetryUpdateRequestModel
                {
                    ProviderId = providerId,
                    Type = type
                },
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    private async Task<int> GetReservedLoyaltyPackageCountAsync(
        decimal telNum,
        decimal offerId,
        decimal providerId,
        CancellationToken cancellationToken)
    {
        var rpc = await _rpcClient
            .RequestAsync<ReservedLoyaltyPackageRequestModel, ReservedLoyaltyPackageResponseModel>(
                subject: "biz.providersale.reserved-loyalty-package",
                request: new ReservedLoyaltyPackageRequestModel
                {
                    TelNum = telNum,
                    PackageNo = offerId,
                    ProviderId = providerId
                },
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return rpc.Success ? rpc.Data?.Count ?? 0 : 0;
    }

    private static ExecResult<ProviderIdCheckResult> ProviderError(
        decimal code,
        string? message,
        ProviderIdCheckResult result)
        => new()
        {
            ExecStatus = false,
            ResultCode = code,
            ResultMessage = message,
            Data = result
        };




    // ---------------------------------------------------------------------------
    // CheckDiyOfferAsync
    // Mirrors Oracle: PRC_CHECK_DIY_OFFER(P_OFFER_ID, P_PROVIDER_ID, OUT DATA/VOICE/SMS)
    // No error returned — just returns DIY values (0 if not a DIY offer)
    // ---------------------------------------------------------------------------
    private async Task<DiyOfferResult?> CheckDiyOfferAsync(
        decimal offerId,
        decimal providerId,
        CancellationToken cancellationToken)
    {
        try
        {
            var rpc = await _rpcClient
                .RequestAsync<CheckDiyOfferRequestModel, CheckDiyOfferResponseModel>(
                    subject: "biz.packagesale",
                    request: new CheckDiyOfferRequestModel
                    {
                        OfferId = offerId,
                        ProviderId = providerId
                    },
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (!rpc.Success || rpc.Data is null)
                return null;

            return new DiyOfferResult
            {
                Data = rpc.Data.Data,
                Voice = rpc.Data.Voice,
                Sms = rpc.Data.Sms
            };
        }
        catch
        {
            // Mirrors Oracle: PRC_CHECK_DIY_OFFER has no exception handler
            // — if not a DIY offer, values stay 0
            return null;
        }
    }
    private ExecResult CheckNullValue(string input, string parameterName)
    {
        if (input is null)
            return Error(19, $"Parameter '{parameterName}' cannot be null.");

        return Success();
    }

    private ExecResult CheckTellNumAndGiftNumMatch(string telNum, string telGift)
    {
        if (telNum != telGift)
            return Error(-1087, "Tel number and gift number do not match.");

        return Success();
    }

    private async Task<ExecResult> CheckBrokerStatus(decimal sapId, int? saleType, CancellationToken cancellationToken)
    {
        var brokerRpc = await _rpcClient
                        .RequestAsync<BrokersRequestModel, BrokersResponseModel>(
                            subject: "biz.packagesale",
                            request: new BrokersRequestModel
                            {
                                SapId = sapId,
                            },
                            cancellationToken: cancellationToken)
                        .ConfigureAwait(false);

        if (!brokerRpc.Success || brokerRpc.Data is null)
            return Error(-1001, "Broker not found.");


        if (brokerRpc.Data.Status == 0)
            return Error(-1002, "Broker is not active.");

        if (saleType.HasValue && brokerRpc.Data.SaleType != saleType.Value)
            return Error(-1101, "Sale type mismatch.");

        return Success();
    }

    private async Task<ExecResult<decimal>> CheckOfferStatusAsync(decimal offerCode, decimal type, decimal amount, decimal data, decimal voice, decimal sms, CancellationToken cancellationToken)
    {
        var offerRpc = await _rpcClient
            .RequestAsync<OfferRequestModel, OfferResponseModel>(
                subject: "biz.packagesale",
                request: new OfferRequestModel
                {
                    OfferCode = offerCode
                },
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (!offerRpc.Success || offerRpc.Data is null)
        {
            return type switch
            {
                1 => new() { ExecStatus = false, ResultCode = -1021, ResultMessage = "Offer not found." },
                2 => new() { ExecStatus = false, ResultCode = -1033, ResultMessage = "Offer not found." },
                4 => new() { ExecStatus = false, ResultCode = -1165, ResultMessage = "Offer not found." },
                _ => new() { ExecStatus = false, ResultCode = -1021, ResultMessage = "Offer not found." }
            };
        }

        OfferResponseModel offer = offerRpc.Data;
        decimal ruleId = offer.RuleId;
        decimal price = offer.Price;
        decimal category = offer.Category;
        decimal offerType = offer.Type;
        decimal status = offer.Status;

        if (offerType != type)
        {
            return new()
            {
                ExecStatus = false,
                ResultCode = -1095,
                ResultMessage = "Offer type mismatch.",
            };
        }
        switch (type)
        {
            case 1:
                {
                    //TODO Charge
                    break;
                }

            case 2:
                {
                    if (status != 1)
                    {
                        return new()
                        {
                            ExecStatus = false,
                            ResultCode = -1033,
                            ResultMessage = "Offer is not active.",
                        };
                    }

                    if (category == 4)
                    {
                        // Oracle: NULL — no validation needed
                    }
                    else if (category == 12)
                    {
                        var diyPriceRpc = await _rpcClient
                            .RequestAsync<DiyPriceRequestModel, decimal>(
                                subject: "biz.packagesale",
                                request: new DiyPriceRequestModel
                                {
                                    Data = data,
                                    Voice = voice,
                                    Sms = sms
                                },
                                cancellationToken: cancellationToken)
                            .ConfigureAwait(false);

                        if (!diyPriceRpc.Success)
                        {
                            return new()
                            {
                                ExecStatus = false,
                                ResultCode = -1098,
                                ResultMessage = "DIY price data not found.",
                            };
                        }

                        decimal diyPrice = diyPriceRpc.Data;

                        decimal expectedPrice = Math.Floor(diyPrice / 100) * 100 * 1.1m;
                        if (amount != expectedPrice)
                        {
                            return new()
                            {
                                ExecStatus = false,
                                ResultCode = -1034,
                                ResultMessage = "Amount does not match DIY offer price.",
                            };
                        }
                    }
                    else
                    {
                        decimal expectedPrice = Math.Floor(price / 100) * 100 * 1.1m;
                        if (amount != expectedPrice)
                        {
                            return new()
                            {
                                ExecStatus = false,
                                ResultCode = -1034,
                                ResultMessage = "Amount does not match offer price.",
                            };
                        }
                    }
                    break;
                }

            case 4:
                {
                    if (status != 1)
                    {
                        return new()
                        {
                            ExecStatus = false,
                            ResultCode = -1033,
                            ResultMessage = "Offer is not active.",
                        };
                    }

                    if (price == 0 || amount == 0)
                    {
                        return new()
                        {
                            ExecStatus = false,
                            ResultCode = -1034,
                            ResultMessage = "Price or amount is zero.",
                        };
                    }
                    break;
                }
        }

        return new ExecResult<decimal>()
        {
            Data = offerRpc.Data.RuleId,
            ExecStatus = true,
            ResultCode = 0,
            ResultMessage = "OK",
        };
    }

    private async Task<ExecResult> CheckBrokerAccessAsync(decimal brokerId, decimal offerId, int type, decimal data, decimal voice, decimal sms, CancellationToken cancellationToken = default)
    {
        var accessRpc = await _rpcClient
            .RequestAsync<BrokerOfferAccessRequestModel, BrokerOfferAccessResponseModel>(
                subject: "biz.packagesale",
                request: new BrokerOfferAccessRequestModel
                {
                    BrokerId = brokerId,
                    OfferId = offerId,
                },
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (!accessRpc.Success || accessRpc.Data is null)
        {
            return type switch
            {
                1 => Error(-1018, "Broker access not found."),
                2 => Error(-1032, "Broker access not found."),
                4 => Error(-1164, "Broker access not found."),
                _ => Error(-1032, "Broker access not found.")
            };
        }


        var broker = await _rpcClient
                        .RequestAsync<BrokersRequestModel, BrokersResponseModel>(
                            subject: "biz.packagesale",
                            request: new BrokersRequestModel
                            {
                                SapId = brokerId,
                            },
                            cancellationToken: cancellationToken)
                        .ConfigureAwait(false);

        if (!broker.Success || broker.Data is null)
            return Error(-1001, "Broker not found.");


        var offer = await _rpcClient
            .RequestAsync<OfferRequestModel, OfferResponseModel>(
                subject: "biz.packagesale",
                request: new OfferRequestModel
                {
                    OfferCode = offerId
                },
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (!offer.Success || offer.Data is null)
            return Error(-1001, "offer not found.");

        int saleAccess = (int)broker.Data.SaleAccess;
        int saleType = (int)broker.Data.SaleType;
        int brokerType = (int)broker.Data.BrokerType;
        string brokerDesc = broker.Data.BrokerDesc;

        int offerType = (int)offer.Data.Type;
        int offerCategory = (int)offer.Data.Category;
        int offerBrokerType = (int)offer.Data.BrokerType;
        int relationType = (int)offer.Data.RelationId;

        if ((offerType == 1 && !new[] { 1, 3, 5, 7 }.Contains(saleAccess)) ||
            (offerType == 2 && !new[] { 2, 3, 6, 7 }.Contains(saleAccess)) ||
            (offerType == 4 && !new[] { 4, 5, 6, 7 }.Contains(saleAccess)))
        {
            return Error(-1020, "Broker does not have access to this offer type.");
        }

        if (saleType != 1 && relationType == 33)
            return Error(-1116, "Sale type and relation type mismatch.");

        if (type == 2)
        {
            if (offerCategory == 12)
            {
                var diyDataRpc = await _rpcClient
                    .RequestAsync<DiyDataAccessRequestModel, DiyDataAccessResponseModel>(
                        subject: "biz.packagesale",
                        request: new DiyDataAccessRequestModel
                        {
                            BrokerId = (long)brokerId,
                            OfferId = (long)offerId,
                            AttributeType = 1,
                            AttributeVal = data
                        },
                        cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                if (!diyDataRpc.Success || diyDataRpc.Data is null)
                    return Error(-1099, "Broker does not have DIY data access.");

                var diyVoiceRpc = await _rpcClient
                    .RequestAsync<DiyDataAccessRequestModel, DiyDataAccessResponseModel>(
                        subject: "biz.packagesale",
                        request: new DiyDataAccessRequestModel
                        {
                            BrokerId = (long)brokerId,
                            OfferId = (long)offerId,
                            AttributeType = 2,
                            AttributeVal = voice
                        },
                        cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                if (!diyVoiceRpc.Success || diyVoiceRpc.Data is null)
                    return Error(-1099, "Broker does not have DIY voice access.");

                var diySmsRpc = await _rpcClient
                    .RequestAsync<DiyDataAccessRequestModel, DiyDataAccessResponseModel>(
                        subject: "biz.packagesale",
                        request: new DiyDataAccessRequestModel
                        {
                            BrokerId = (long)brokerId,
                            OfferId = (long)offerId,
                            AttributeType = 3,
                            AttributeVal = sms
                        },
                        cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                if (!diySmsRpc.Success || diySmsRpc.Data is null)
                    return Error(-1099, "Broker does not have DIY SMS access.");
            }

            if (brokerType == 2)
            {
                if (offerBrokerType != 2 && offerBrokerType != 3)
                    return Error(-1032, "Offer not available for this broker type.");
            }
        }

        return Success();
    }

    private ExecResult CheckTalia(string telNum)
    {
        if (telNum.StartsWith('0'))
            telNum = telNum.Remove(0, 1);

        if (telNum.StartsWith("932") || telNum.StartsWith("98932"))
            return Error(-1053, "Tel number and gift number do not match.");

        if (telNum.Length < 2)
            return Error(-1114, "Tel number and gift number do not match.");

        return Success();
    }

    private async Task<ExecResult> CheckWllAsync(string telGift, int type, decimal offerId, CancellationToken cancellationToken)
    {
        if (type == 1)
        {

            int prefix = Convert.ToInt32(telGift.Substring(0, 4));

            var wllRpc = await _rpcClient
                .RequestAsync<WllPrefixesRequestModel, WllPrefixesResponseModel>(
                    subject: "biz.packagesale.wll-prefix",
                    request: new WllPrefixesRequestModel
                    {
                        PkWllPrefix = prefix
                    },
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (!wllRpc.Success || wllRpc.Data is null)
                return Success();

            var offerRpc = await _rpcClient
                .RequestAsync<OfferRequestModel, OfferResponseModel>(
                    subject: "biz.packagesale.offer",
                    request: new OfferRequestModel
                    {
                        OfferCode = offerId
                    },
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (!offerRpc.Success || offerRpc.Data is null)
                return Success();

            decimal offerCode = offerRpc.Data.OfferCode;

            if (!new decimal[] { 1001, 1002 }.Contains(offerCode))
                return Error(-1079, "Offer code is not valid for WLL number.");

            return Success();
        }
        if (type == 2)
        {
            int prefix = Convert.ToInt32(telGift.Substring(0, 4));

            var wllRpc = await _rpcClient
                .RequestAsync<WllPrefixesRequestModel, WllPrefixesResponseModel>(
                    subject: "biz.packagesale.wll-prefix",
                    request: new WllPrefixesRequestModel
                    {
                        PkWllPrefix = prefix
                    },
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (!wllRpc.Success || wllRpc.Data is null)
                return Success();

            return Error(-1066, "Gift number is a WLL number.");
        }

        return Success();
    }

    private async Task<ExecResult> CheckChannelIdAsync(decimal? channelId, CancellationToken cancellationToken)
    {
        if ((channelId ?? 0) <= 0)
            return Success();

        var channelRpc = await _rpcClient
            .RequestAsync<ChannelIdRequestModel, ChannelIdResponseModel>(
                subject: "biz.packagesale",
                request: new ChannelIdRequestModel
                {
                    ChannelId = channelId.Value,
                },
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (!channelRpc.Success || channelRpc.Data is null)
            return Error(-1149, "Channel ID not found.");

        return Success();
    }

    private static ExecResult Success() => new() { ExecStatus = true, ResultCode = 0, ResultMessage = "OK" };

    private static ExecResult Error(decimal code, string message) => new() { ExecStatus = false, ResultCode = code, ResultMessage = message };


}
