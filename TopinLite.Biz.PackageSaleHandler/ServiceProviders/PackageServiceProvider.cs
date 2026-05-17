using System.Text.Json;
using TopinLite.Biz.PackageSaleHandler.PackageOrder;
using TopinLite.Biz.PackageSaleHandler.ServiceExtentions;
using TopinLite.Domain.Commons;
using TopinLite.infra.PostgreSQL.Abstractions;
using TopinLite.infra.PostgreSQL.Models;
using TopinLite.Services.Commons;

namespace TopinLite.Biz.PackageSaleHandler.ServiceProviders;

public interface IPackageServiceProvider
{
    Task<ExecResult<RequestOrderResponseMessageModel>> RequestOrderAsync(RequestOrderRequestMessageModel request, CancellationToken cancellationToken);
    Task<ExecResult<PackageConfirmOrderResponse>> ConfirmOrderAsync(PackageConfirmOrderRequest request, CancellationToken cancellationToken);
}

public class PackageServiceProvider : IPackageServiceProvider
{
    private readonly IRpcClient _rpcClient;
    private readonly ILogger<PackageServiceProvider> _logger;
    private readonly IProviderIdGenerator _providerIdGenerator;
    private readonly ICommonValidation _commonValidation;
    private readonly IPackageOrderStore _orderStore;
    private readonly IAesEncryption _aesEncryption;
    private readonly ITopupSalesRepository _topupSaleRepository;

    public PackageServiceProvider(IRpcClient rpcClient, ILogger<PackageServiceProvider> logger, IProviderIdGenerator providerIdGenerator, ICommonValidation commonValidation, IPackageOrderStore orderStore, IAesEncryption aesEncryption, ITopupSalesRepository topupSaleRepository)
    {
        _rpcClient = rpcClient;
        _logger = logger;
        _providerIdGenerator = providerIdGenerator;
        _commonValidation = commonValidation;
        _orderStore = orderStore;
        _aesEncryption = aesEncryption;
        _topupSaleRepository = topupSaleRepository;
    }

    public async Task<ExecResult<RequestOrderResponseMessageModel>> RequestOrderAsync(RequestOrderRequestMessageModel request, CancellationToken cancellationToken)
    {
        if (!TryParseRequest(request, out var req, out ExecResult parseError))
            return Fail(parseError.ResultCode, parseError.ResultMessage);


        try
        {
            ExecResult stopCheckResult = await CheckProviderStoppedAsync(req.SapId).ConfigureAwait(false);
            if (!stopCheckResult.ExecStatus)
                return Fail(stopCheckResult.ResultCode, stopCheckResult.ResultMessage);

            RpcResult<OfferResponseModel> offerResult = await GetOfferAsync(req.OfferId).ConfigureAwait(false);
            if (!offerResult.Success || offerResult.Data is null)
            {
                return Fail(-1033, "Offer was not found in cache.");
            }

            RpcResult<BrokersResponseModel> brokerResult = await GetBrokerAsync(req.SapId).ConfigureAwait(false);
            if (!brokerResult.Success || brokerResult.Data is null)
            {
                return Fail(-1001, "Broker was not found in cache.");
            }

            decimal providerId = _providerIdGenerator.GeneratePackageProviderId();

            PackageRequestModel context = new(req, offerResult.Data, brokerResult.Data);

            ExecResult categoryValidation = await ValidateByCategoryAsync(context, cancellationToken).ConfigureAwait(false);
            if (!categoryValidation.ExecStatus)
                return Fail(categoryValidation.ResultCode, categoryValidation.ResultMessage);


            decimal reserveStatus = await GetGlobalReserveStatusAsync().ConfigureAwait(false);


            decimal amountWithTax = req.Amount * 1.1m;

            await _orderStore.SavePackageSaleAsync(new PackageOrderContext()
            {
                ProviderId = providerId,
                TelNum = req.TelNum,
                TelGift = req.TelGift,
                Amount = amountWithTax,
                OfferId = req.OfferId,
                OfferCode = context.Offer.OfferCode,
                BrokerId = context.Broker.BrokerId,
                ChannelId = req.ChannelId,
                SapId = req.SapId,
                AccountId = context.AccountId,
                CampOrder = Convert.ToDecimal(context.CampOrder),
                ReserveStatus = reserveStatus,
                CreatedAtUtc = DateTime.UtcNow,
                Category = offerResult.Data.Category,
                SaleType = req.SaleType,
                Data = req.Data,
                Sms = req.Sms,
                Voice = req.Voice,
            }, cancellationToken).ConfigureAwait(false);


            _logger.LogInformation($"CALL_SALE_PROVIDER(package) validated. ProviderId={providerId}, TelNum={req.TelNum}, TelGift={req.TelGift}, OfferId={req.OfferId}, SapId={req.SapId}, ChannelId={req.ChannelId}, Category={context.Offer.Category}, AmountWithTax={amountWithTax}, AccountId={context.AccountId}, AccountName={context.AccountName}");

            return Ok(new RequestOrderResponseMessageModel
            {
                OrderId = providerId
            }, providerId);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled error in package CALL_SALE_PROVIDER simulation");
            return Fail(7, "Technical error");
        }
    }


    public async Task<ExecResult<PackageConfirmOrderResponse>> ConfirmOrderAsync(PackageConfirmOrderRequest request,
                                                                                 CancellationToken cancellationToken)
    {
        var startTime = DateTime.Now;
        var maskedCard = Mask(request.CardNo);
        string telNum = null;
        decimal providerId = request.OrderId;
        string inputReq = "Error on convert.";

        if (request.OrderId <= 0)
            return FailConfirm(ResultCodes.OrderNotFound);

        PackageOrderContext sale = null;
        try
        {
            sale = await _orderStore
                .GetPackageSaleAsync(request.OrderId, cancellationToken)
                .ConfigureAwait(false);

            if (sale is null)
            {
                _logger.LogWarning("ConfirmOrder: Order not found. OrderId={OrderId}", request.OrderId);
                return FailConfirm(ResultCodes.OrderNotFound);
            }

            inputReq = Build(
                providerId: request.OrderId,
                bankId: request.BankCode,
                maskedCardNo: maskedCard,
                idCardType: request.CardType,
                rrn: request.RRN,
                sapId: sale.SapId,
                saleType: sale.SaleType);


            RpcResult<OfferResponseModel> offerResult = await GetOfferAsync(sale.OfferId)
                .ConfigureAwait(false);
            if (!offerResult.Success || offerResult.Data is null)
            {
                _logger.LogWarning("ConfirmOrder: Offer not found. OfferId={OfferId}", sale.OfferId);
                return FailConfirm(ResultCodes.OfferNotFound);
            }

            RpcResult<BrokersResponseModel> brokerResult = await GetBrokerAsync(sale.SapId)
                .ConfigureAwait(false);
            if (!brokerResult.Success || brokerResult.Data is null)
            {
                _logger.LogWarning("ConfirmOrder: Broker not found. SapId={SapId}", sale.SapId);
                return FailConfirm(ResultCodes.BrokerNotFound);
            }

            telNum = sale.TelNum.ToString(CultureInfo.InvariantCulture);
            string telGiftStr = sale.TelGift.ToString(CultureInfo.InvariantCulture);
            int category = (int)offerResult.Data.Category;
            int relationType = (int)offerResult.Data.RelationId;
            decimal offerCode = offerResult.Data.OfferCode;
            //TODO Check that have we OfferName??
            string packageDesc = offerResult.Data.OfferName;
            string brokerDesc = brokerResult.Data.BrokerDesc;


            if (sale.ReserveStatus == 2)
            {
                _logger.LogWarning("ConfirmOrder: Activation already in progress. OrderId={OrderId}", request.OrderId);
                return FailConfirm(ResultCodes.PackageActivationInProgress);
            }

            if (sale.ReserveStatus == 1)
            {
                _logger.LogWarning("ConfirmOrder: Order already completed. OrderId={OrderId}", request.OrderId);
                return FailConfirm(ResultCodes.SuccessInPast);
            }


            var req = new ParsedRequest
            {
                TelNum = sale.TelNum,
                TelGift = sale.TelGift,
                Amount = sale.Amount,
                OfferId = sale.OfferId,
                SapId = sale.SapId,
                ChannelId = sale.ChannelId,
                SaleType = sale.SaleType,
                Data = sale.Data,
                Voice = sale.Voice,
                Sms = sale.Sms,
            };

            PackageRequestModel context = new(req, offerResult.Data, brokerResult.Data);
            ExecResult categoryValidation = await ValidateByCategoryAsync(context, cancellationToken)
                .ConfigureAwait(false);

            if (!categoryValidation.ExecStatus)
            {
                _logger.LogWarning("ConfirmOrder: Category validation failed. OrderId={OrderId}, Code={Code}, Message={Message}",
                    request.OrderId, categoryValidation.ResultCode, categoryValidation.ResultMessage);

                return new()
                {
                    ExecStatus = false,
                    ResultCode = categoryValidation.ResultCode,
                    ResultMessage = categoryValidation.ResultMessage,
                    Data = new PackageConfirmOrderResponse()
                };
            }


            string encCardNo;
            try
            {
                encCardNo = _aesEncryption.Encrypt(request.CardNo ?? string.Empty, telNum);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "ConfirmOrder: AES encryption failed. OrderId={OrderId}", request.OrderId);
                return FailConfirm(ResultCodes.SystemError);
            }



            // Lock sale row — UPDATE PAY_STATUS = 2 (skip for relationType 17)
            // Mirrors Oracle: UPDATE TBL_PACKAGE_SALES SET PAY_STATUS = 2
            decimal responseType = 0;
            string responseDesc = null;
            decimal crmMessageSeq = 0;
            string orderId = null;
            var curDate = DateTime.Now;
            var (saleDate, saleTime) = PersianDateHelper.ToDateAndTime(curDate);

            if (relationType != 17)
            {
                var lockSale = PackageSaleMapper.ToLockSale(sale, providerId, saleDate, saleTime);
                await _topupSaleRepository
                    .InsertPackageSaleAsync(lockSale, cancellationToken)
                    .ConfigureAwait(false);

                sale.ReserveStatus = 2;
                await _orderStore
                    .SavePackageSaleAsync(sale, cancellationToken)
                    .ConfigureAwait(false);

                var stdRpc = await _rpcClient
                                    .RequestAsync<ChangeSubscribersOfferingAddRequestModel,
                                                  ChangeSubscribersOfferingAddResponseModel>(
                                        subject: "crm.changesubscribersofferingadd",
                                        request: new ChangeSubscribersOfferingAddRequestModel
                                        {
                                            PrimaryIdentity = telGiftStr,
                                            OfferingId = offerCode.ToString(CultureInfo.InvariantCulture),
                                            BrokerId = sale.SapId.ToString(CultureInfo.InvariantCulture),
                                            Mss = $"Topup/Topin{Guid.NewGuid():N}",
                                            SponserMsisdn = telNum,
                                            CycleDiscount = "0",
                                            Data = "0",
                                            Voice = "0",
                                            SMS = "0"
                                        },
                                        cancellationToken: cancellationToken)
                                    .ConfigureAwait(false);

                responseType = Convert.ToDecimal(stdRpc.Data?.ResponseType);
                responseDesc = stdRpc.Data?.ResponseDesc;
                //crmMessageSeq = stdRpc.Data?.CrmMessageSequence ?? 0;

                if (responseType == 0 && responseDesc is not null)
                {
                    try
                    {
                        orderId = responseDesc[..Math.Min(300, responseDesc.Length)];
                    }
                    catch
                    {
                        orderId = null;
                    }
                }
            }
            else
            {
                responseType = 0;
            }

            // SUCCESS PATH
            if (responseType == 0)
            {
                curDate = DateTime.Now;
                (saleDate, saleTime) = PersianDateHelper.ToDateAndTime(curDate);

                // PostgreSQL: insert success record
                // Mirrors Oracle: UPDATE TBL_PACKAGE_SALES SET PAY_STATUS = 1
                var successSale = PackageSaleMapper.ToSuccessSale(
                    sale: sale,
                    providerId: providerId,
                    bankId: request.BankCode,
                    encCardNo: encCardNo,
                    CardType: request.CardType,
                    rrn: request.RRN,
                    orderId: orderId,
                    saleDate: saleDate,
                    saleTime: saleTime,
                    accountId: sale.AccountId,
                    crmMessageSeq: crmMessageSeq,
                    responseDesc: ResultCodes.Success);

                await _topupSaleRepository
                .InsertPackageSaleAsync(successSale, cancellationToken)
                .ConfigureAwait(false);

                sale.ReserveStatus = 1;
                await _orderStore
                    .SavePackageSaleAsync(sale, cancellationToken)
                    .ConfigureAwait(false);


                var smsText = sale.ReserveStatus != 3
                    ? $"مشترک گرامي\n{packageDesc} براي شماره تلفن {telGiftStr} با موفقيت فعال شد."
                    : $"مشترک گرامي\n{packageDesc} براي شماره تلفن {telGiftStr} با موفقيت رزرو شد.";


                if (telNum != telGiftStr && category != 61)
                {
                    //TODO Implement the SMS SERVICE
                    //await _smsService.SendAsync(telNum, smsText, providerId, cancellationToken)
                    //    .ConfigureAwait(false);

                    // Mirrors Oracle: FNC_COM_DYNAMIC_CONDITION(996,'BUY_FOR_OTHER_OFFERS_SMS')

                    RpcResult<DynamicConditionsResponseModel> buyForOtherSmsCondition =
                    await _rpcClient.RequestAsync<DynamicConditionsRequestModel, DynamicConditionsResponseModel>(
                        subject: "biz.packagesale",
                        request: new DynamicConditionsRequestModel
                        {
                            Biztype = "996",
                            KeyStr = "BUY_FOR_OTHER_OFFERS_SMS",
                        },
                        cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                    if (buyForOtherSmsCondition.Success && buyForOtherSmsCondition.Data?.ValueStr == "1")
                    {
                        //TODO GET MESSAGE ASYNC
                        var giftSms = "";
                        //await GetMessageAsync(100016, "PACKAGE_SALE_SMS", cancellationToken)
                        //.ConfigureAwait(false);

                        giftSms = giftSms.Replace("%TEL_NUM%", telNum)
                                         .Replace("%PACKAGE_DESC%", packageDesc);

                        //TODO SEND MESSAGE
                        //await _smsService.SendAsync(telGiftStr, giftSms, providerId, cancellationToken)
                        //    .ConfigureAwait(false);
                    }
                }

                RpcResult<DynamicConditionsResponseModel> offerSmsInformCondition =
                    await _rpcClient.RequestAsync<DynamicConditionsRequestModel, DynamicConditionsResponseModel>(
                        subject: "biz.packagesale",
                        request: new DynamicConditionsRequestModel
                        {
                            Biztype = "113",
                            KeyStr = "OFFER_SMS_INFORM",
                        },
                        cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                if (offerSmsInformCondition.Success && offerSmsInformCondition.Data?.ValueStr == "1")
                {
                    //TODO GET MESSAGE ASYNC
                    var offerSms = "";
                    //await GetMessageAsync(100023, "PACKAGE_SALE_SMS", cancellationToken)
                    //    .ConfigureAwait(false);

                    offerSms = offerSms
                        .Replace("%PKG_DESC%", packageDesc)
                        .Replace("%BROKER_DESC%", brokerDesc)
                        .Replace("%PKG_AMOUNT%", sale.Amount.ToString())
                        .Replace("%DATE%", saleDate)
                        .Replace("%TIME%", saleTime);

                    //TODO SEND MESSAGE
                    //await _smsService.SendAsync(telGiftStr, offerSms, providerId, cancellationToken)
                    //    .ConfigureAwait(false);
                }


                //TODO NEED I Check THIS??
                //UPDATE broker sale amount

                //await UpsertBrokerSaleAmountAsync(
                //    sale.BrokerId, sale.OfferId,
                //    saleDate, sale.Amount, accountId, cancellationToken)
                //    .ConfigureAwait(false);

                //TODO NEED I Check THIS??
                //Post-sale Hamrahi inform (fire and forget)

                //_ = HandlePostSaleInformAsync(
                //    telNum, telGiftStr, relationType, offerCode,
                //    saleDate, saleTime, providerId, cancellationToken);

                _logger.LogInformation(
                    "ConfirmOrder succeeded. OrderId={OrderId}, Category={Category}, RelationType={RelationType}, OfferCode={OfferCode}",
                    request.OrderId, category, relationType, offerCode);

                return OkConfirm(sale.AccountId, remain, (int)offerCode, orderId, responseDesc);
            }

            // FAILURE PATH
            // Mirrors Oracle: ELSE branch after IF P_RESPONSE_TYPE = 0

            //  PostgreSQL: insert failed record
            var failedSale = PackageSaleMapper.ToFailedSale(
                sale: sale,
                providerId: providerId,
                responseType: responseType,
                responseDesc: responseDesc,
                saleDate: saleDate,
                saleTime: saleTime);

            await _topupSaleRepository
                .InsertPackageSaleAsync(failedSale, cancellationToken)
                .ConfigureAwait(false);

            //  FIX: Redis: update state to failed (was missing)
            sale.ReserveStatus = -1;
            await _orderStore
                .SavePackageSaleAsync(sale, cancellationToken)
                .ConfigureAwait(false);


            // Mirrors Oracle: IF UPPER(P_RESPONSE_DESC) LIKE '%ORA%'
            if (responseDesc?.ToUpperInvariant().Contains("ORA") == true)
            {
                _logger.LogError(
                    "ConfirmOrder: Oracle/technical error in response. OrderId={OrderId}, Desc={Desc}",
                    request.OrderId, responseDesc);
                return FailConfirm(ResultCodes.SystemError);
            }

            // Mirrors Oracle: FNC_COM_DYNAMIC_VALUE('SUBSCRIBER_OFFERING_ADD', P_RESPONSE_TYPE)
            RpcResult<DynamicConditionsResponseModel> dynamicMappedCode =
                await _rpcClient.RequestAsync<DynamicConditionsRequestModel, DynamicConditionsResponseModel>(
                subject: "biz.packagesale.dynamicconditions",
                request: new DynamicConditionsRequestModel
                {
                    Biztype = "SUBSCRIBER_OFFERING_ADD",
                    KeyStr = responseType.ToString(),
                }).ConfigureAwait(false);

            if (dynamicMappedCode.Success && dynamicMappedCode.Data is not null && !string.IsNullOrEmpty(dynamicMappedCode.Data.ValueStr) && decimal.TryParse(dynamicMappedCode.Data.ValueStr, out decimal mappedCode))
            {
                //Need To Implement this? this is for campain

                // FIX: implement the commented-out dynamic mapping block
                // Mirrors Oracle: P_RESPONSE_TYPE := V_RESPONSE_TYPE
                responseType = mappedCode;

                // Mirrors Oracle: FNC_COM_DYNAMIC_CONDITION('TOPUP_DIRECT_ERROR','PACKAGE',V_RESPONSE_TYPE)
                RpcResult<DynamicConditionsResponseModel> directErrorCondition =
                    await _rpcClient
                        .RequestAsync<DynamicConditionsRequestModel, DynamicConditionsResponseModel>(
                            subject: "biz.packagesale",
                            request: new DynamicConditionsRequestModel
                            {
                                Biztype = "TOPUP_DIRECT_ERROR",
                                KeyStr = "PACKAGE",
                            },
                            cancellationToken: cancellationToken)
                        .ConfigureAwait(false);

                bool keepRaw = directErrorCondition.Success &&
                               directErrorCondition.Data?.ValueStr == "1";

                responseDesc = keepRaw
                    ? responseDesc?[..Math.Min(4000, responseDesc?.Length ?? 0)]
                    : null; // TODO: GetMessageAsync(responseType, "TOPUP_ERROR")
            }
            else
            {
                if (responseType == 60108030015)
                {
                    responseDesc = "خطا در فعالسازي:" +
                                   (responseDesc ?? string.Empty)
                                   .Replace("خطا", string.Empty)
                                   .Replace(":", string.Empty);
                    responseType = -1036;
                }
                else
                {
                    var baseMsg = ResultCodes.ActivePackageFail;
                    var combined = baseMsg + responseDesc;
                    responseDesc = combined[..Math.Min(4000, combined.Length)];
                    responseType = -1036;
                }
            }

            _logger.LogWarning("ConfirmOrder failed. OrderId={OrderId}, MappedCode={MappedCode}, Desc={Desc}",request.OrderId, responseType, responseDesc);


            return new()
            {
                ExecStatus = false,
                ResultCode = Convert.ToDecimal(responseType),
                ResultMessage = responseDesc,
                Data = new PackageConfirmOrderResponse()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ConfirmOrder unhandled exception. OrderId={OrderId}", request.OrderId);

            try
            {
                var curDate = DateTime.Now;
                var (saleDate, saleTime) = PersianDateHelper.ToDateAndTime(curDate);

                if (sale is not null)
                {
                    // FIX: use already-fetched sale instead of re-fetching from Redis
                    var errorSale = PackageSaleMapper.ToFailedSale(
                        sale: sale,
                        providerId: providerId,
                        responseType: 7,
                        responseDesc: ex.Message,
                        saleDate: saleDate,
                        saleTime: saleTime);

                    // PostgreSQL: insert error record
                    await _topupSaleRepository
                        .InsertPackageSaleAsync(errorSale, cancellationToken)
                        .ConfigureAwait(false);

                    // Redis: update state to failed
                    sale.ReserveStatus = -1;
                    await _orderStore
                        .SavePackageSaleAsync(sale, cancellationToken)
                        .ConfigureAwait(false);
                }
            }
            catch { /* best effort */ }

            return FailConfirm(ResultCodes.SystemError);
        }
    }

    public static string Build(decimal providerId, decimal bankId, string maskedCardNo,
                               decimal idCardType, decimal rrn, decimal sapId, int? saleType)
    {
        try
        {
            return JsonSerializer.Serialize(new
            {
                providerId,
                bankId,
                idCardNo = maskedCardNo,
                idCardType,
                rrn,
                sapID = sapId,
                saleType
            });
        }
        catch { return "Error on convert."; }
    }
    private static ExecResult<PackageConfirmOrderResponse> OkConfirm(string accountName, string remain, int offerCode, string? crmOrderId, string? resultRaw)
        => new()
        {
            ExecStatus = true,
            ResultCode = 0,
            ResultMessage = "Success",
            Data = new PackageConfirmOrderResponse
            {
                AccountName = accountName,
                Remain = remain,
                OfferCode = offerCode,
                LoyaltyYear = 0,
                TradeType = "1",
                RechargeSerialNo = crmOrderId ?? string.Empty,
                ResultRaw = resultRaw ?? "OK"
            }
        };

    private async Task<decimal> GetGlobalReserveStatusAsync()
    {
        try
        {
            RpcResult<DynamicConditionsResponseModel> dynamicResult = await _rpcClient.RequestAsync<DynamicConditionsRequestModel, DynamicConditionsResponseModel>(
                subject: "biz.packagesale.dynamicconditions",
                request: new DynamicConditionsRequestModel { Biztype = "0", KeyStr = "GLOBAL_RESERVE_STATUS" }).ConfigureAwait(false);

            if (dynamicResult.Success && dynamicResult.Data is not null &&
                decimal.TryParse(dynamicResult.Data.ValueStr, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal parsed))
            {
                return parsed;
            }
        }
        catch
        {
        }

        return 0;
    }

    private async Task<ExecResult> ValidateExecByCategoryAsync(
    PackageRequestModel context,
    CancellationToken cancellationToken)
    {
        int category = (int)context.Offer.Category;

        return category switch
        {
            4 => Success(), // Anarestan — skipped for now
            3 => Success(), // ByFreeze  — not used in this project
            _ => await _commonValidation.ValidateExecSalePackageAsync(context, cancellationToken).ConfigureAwait(false)
        };
    }

    private async Task<ExecResult> ValidateByCategoryAsync(PackageRequestModel context, CancellationToken cancellationToken)
    {
        int category = (int)context.Offer.Category;

        return category switch
        {
            //3 => await _commonValidation.ValidateCallSaleByFreezeAsync(context).ConfigureAwait(false),
            4 => await _commonValidation.ValidateCallSaleAnarestanAsync(context, cancellationToken).ConfigureAwait(false),
            _ => await _commonValidation.ValidateBusinessAsync(context, cancellationToken).ConfigureAwait(false)
        };
    }

    private async Task<ExecResult> CheckProviderStoppedAsync(int sapId)
    {
        try
        {
            RpcResult<DynamicConditionsResponseModel>? stopConditionTask =
                await _rpcClient.RequestAsync<DynamicConditionsRequestModel, DynamicConditionsResponseModel>(
                    subject: "biz.packagesale",
                    request: new DynamicConditionsRequestModel
                    {
                        Biztype = "999",
                        KeyStr = "PKG_PACKAGE_SALE.CALL_SALE_PROVIDER_STOPED"
                    })
                .ConfigureAwait(false);

            RpcResult<DynamicConditionsResponseModel>? allowedBrokerTask =
                await _rpcClient.RequestAsync<DynamicConditionsRequestModel, DynamicConditionsResponseModel>(
                    subject: "biz.packagesale",
                    request: new DynamicConditionsRequestModel
                    {
                        Biztype = "997",
                        KeyStr = "PKG_PACKAGE_SALE.CALL_SALE_PROVIDER_BROKERS",
                    })
                .ConfigureAwait(false);

            string? stopValue = stopConditionTask?.Data?.ValueStr;
            string? allowedBroker = allowedBrokerTask?.Data?.ValueStr;

            if (string.Equals(stopValue, "1") && !string.Equals(allowedBroker?.Trim(), sapId.ToString() ?? "1"))
            {
                return new()
                {
                    ExecStatus = false,
                    ResultCode = -1166,
                    ResultMessage = "Package sale provider is disabled for this broker."
                };
            }

            return new() { ExecStatus = true, ResultCode = 0, ResultMessage = "OK" };
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Dynamic condition lookup failed. Continue with default flow for SAP {SapId}", sapId);
            return new() { ExecStatus = true, ResultCode = 0, ResultMessage = "OK" };
        }
    }

    private static string Mask(string cardNo)
    {
        if (string.IsNullOrEmpty(cardNo))
            return cardNo ?? string.Empty;
        if (cardNo.Length < 8)
            return new string('*', cardNo.Length);
        return "****" + cardNo[4..^4] + "****";
    }
    
    private Task<RpcResult<OfferResponseModel>> GetOfferAsync(decimal offerCode)
        => _rpcClient.RequestAsync<OfferRequestModel, OfferResponseModel>(
            subject: "biz.packagesale",
            request: new OfferRequestModel { OfferCode = offerCode });

    private Task<RpcResult<BrokersResponseModel>> GetBrokerAsync(int sapId)
        => _rpcClient.RequestAsync<BrokersRequestModel, BrokersResponseModel>(
            subject: "biz.packagesale",
            request: new BrokersRequestModel { SapId = sapId });

    private static bool TryParseRequest(RequestOrderRequestModel model, out ParsedRequest req, out ExecResult validationError)
    {
        req = default;

        if (model.TelNum <= 0 || model.TelGift <= 0)
        {
            validationError = new() { ExecStatus = false, ResultCode = -2, ResultMessage = "TelNum/TelGift is required." };
            return false;
        }

        if (!decimal.TryParse(model.Amount, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal amount) || amount <= 0)
        {
            validationError = new() { ExecStatus = false, ResultCode = -3, ResultMessage = "Amount is invalid." };
            return false;
        }

        if (!int.TryParse(model.ProductId, NumberStyles.Integer, CultureInfo.InvariantCulture, out int offerId) || offerId <= 0)
        {
            validationError = new() { ExecStatus = false, ResultCode = -4, ResultMessage = "ProductId (OfferId) is invalid." };
            return false;
        }

        if (!int.TryParse(model.BrokerId, NumberStyles.Integer, CultureInfo.InvariantCulture, out int sapId) || sapId <= 0)
        {
            validationError = new() { ExecStatus = false, ResultCode = -5, ResultMessage = "BrokerId (SapId) is invalid." };
            return false;
        }

        if (!int.TryParse(model.ChannelId, NumberStyles.Integer, CultureInfo.InvariantCulture, out int channelId) || channelId <= 0)
        {
            validationError = new() { ExecStatus = false, ResultCode = -6, ResultMessage = "ChannelId is invalid." };
            return false;
        }

        int? saleType = int.TryParse(model.PayloadId, NumberStyles.Integer, CultureInfo.InvariantCulture, out int st) ? st : null;

        req = new ParsedRequest()
        {
            TelNum = model.TelNum,
            TelGift = model.TelGift,
            Amount = amount,
            OfferId = offerId,
            SapId = sapId,
            ChannelId = channelId,
            SaleType = saleType,
            Data = Convert.ToDecimal(model.Gprs),
            Voice = Convert.ToDecimal(model.Voice),
            Sms = Convert.ToDecimal(model.Sms)
        };

        validationError = new() { ExecStatus = true, ResultCode = 0, ResultMessage = "OK" };
        return true;
    }
    
    private static ExecResult<PackageConfirmOrderResponse> FailConfirm(ResultCode resultCode)
        => new()
        {
            ExecStatus = false,
            ResultCode = resultCode.Code,
            ResultMessage = resultCode.Message,
            Data = new PackageConfirmOrderResponse()
        };

    private static ExecResult<RequestOrderResponseMessageModel> Ok(RequestOrderResponseMessageModel data, decimal code)
        => new()
        {
            ExecStatus = true,
            ResultCode = code,
            ResultMessage = code.ToString(CultureInfo.InvariantCulture),
            Data = data
        };

    private static ExecResult<RequestOrderResponseMessageModel> Fail(decimal code, string message)
        => new()
        {
            ExecStatus = false,
            ResultCode = code,
            ResultMessage = message,
            Data = new RequestOrderResponseMessageModel()
        };


}
