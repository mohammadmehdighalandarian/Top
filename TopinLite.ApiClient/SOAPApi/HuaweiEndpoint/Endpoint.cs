
using TopinLite.Domain.HuaweiApiModel.CRMResponses.IntegrationEnquiry;
using TopinLite.Domain.HuaweiApiModel.CRMResponses.QuerySubscriber;
using TopinLite.Domain.HuawiMicroGateway;

namespace TopinLite.Infra.ApiClient.SOAPApi.HuaweiEndpoint
{
    public interface IEndpoint
    {
        Task<GeneralHuawiResponse> ChangeSubscribersOfferingAdd(ChangeSubscribersOfferingAddTcpRequest model);

        Task<GeneralHuawiResponse> QuerySimType(QuerySimTypeTcpRequest model);

        Task<GeneralHuawiResponse> QueryCustomerInfoByTel(QueryCustomerInfoByTelTcpRequest model);

        Task<GeneralHuawiResponse<QuerySubscriberResponse>> QuerySubscriber(QuerySubscriberTcpRequest model);

        Task<GeneralHuawiResponse> QueryBalance(QueryBalanceTcpRequest model);

        Task<GeneralHuawiResponse> RechargeByBroker(RechargeByBrokerTcpRequest model);

        Task<GeneralHuawiResponse> CheckOfferEligibility(CheckOfferEligibilityTcpRequest model);

        Task<GeneralHuawiResponse> QuerySubscriberCZ2(QuerySubscriberCZ2TcpRequest model);

        Task<GeneralHuawiResponse> IntegrationEnquiry(IntegrationEnquiryTcpRequest model);

        Task<GeneralHuawiResponse> DeleteOffering(DeleteOfferingTcpRequest model);

        Task<GeneralHuawiResponse> QueryRelationOffering(QueryRelationOfferingTcpRequest model);

        Task<GeneralHuawiResponse<List<LocalOfferModel>>> GetOfferingList(GetOfferingListTcpRequest model);
    }

    public class Endpoint : IEndpoint
    {
        private readonly ISoapClient _Client;

        public Endpoint(ISoapClient client)
        {
            _Client = client;
        }

        public async Task<GeneralHuawiResponse> ChangeSubscribersOfferingAdd(ChangeSubscribersOfferingAddTcpRequest model)
        {
            TopinLite.Domain.HuaweiApiModel.CRMResponses.ChangeSubscriberOffering.EnvelopeChangeSubscriberOfferingResponse SoapResponse =
                await _Client.ChangeSubscriberOfferingAdd(model);

            if (SoapResponse.Body.changeSubscriberOfferingRspMsg.resultHeader.resultCode.ToString() != "0")
            {
                return new GeneralHuawiResponse
                {
                    ResponseType = SoapResponse.Body.changeSubscriberOfferingRspMsg.resultHeader.resultCode.ToString(),
                    ResponseDesc = SoapResponse.Body.changeSubscriberOfferingRspMsg.resultHeader.resultDesc
                };
            }

            if (SoapResponse.Body.changeSubscriberOfferingRspMsg.changeSubscriberOfferingResponse == null)
            {
                return new GeneralHuawiResponse
                {
                    ResponseType = "-2",
                    ResponseDesc = "queryOfferingsResponse is null!"
                };
            }

            return new GeneralHuawiResponse
            {
                ResponseType = SoapResponse.Body.changeSubscriberOfferingRspMsg.resultHeader.resultCode.ToString(),
                ResponseDesc = SoapResponse.Body.changeSubscriberOfferingRspMsg.changeSubscriberOfferingResponse.orderId.ToString(),
            };
        }

        public async Task<GeneralHuawiResponse> QueryBalance(QueryBalanceTcpRequest model)
        {
            TopinLite.Domain.HuaweiApiModel.CRMResponses.QueryBalance.EnvelopeQueryBalanceResponse SoapResponse =
                await _Client.QueryBalance(model.PrimaryIdentity, model.Mss);

            if (SoapResponse.Body.QueryBalanceResultMsg.ResultHeader.ResultCode.ToString() != "0")
            {
                return new GeneralHuawiResponse
                {
                    ResponseType = SoapResponse.Body.QueryBalanceResultMsg.ResultHeader.ResultCode.ToString(),
                    ResponseDesc = SoapResponse.Body.QueryBalanceResultMsg.ResultHeader.ResultDesc
                };
            }

            if (SoapResponse.Body.QueryBalanceResultMsg.QueryBalanceResult == null)
            {
                return new GeneralHuawiResponse
                {
                    ResponseType = "-2",
                    ResponseDesc = "QueryBalance Response is null!"
                };
            }

            try
            {
                string _ResponseDesc = string.Empty;

                _ResponseDesc = SoapResponse.Body.QueryBalanceResultMsg.QueryBalanceResult.SingleOrDefault().BalanceResult.Where(x => x.BalanceType.StartsWith("C_MAIN_ACCOUNT")).SingleOrDefault().TotalAmount.ToString();

                //_ResponseDesc = SoapResponse.Body.QueryBalanceResultMsg.QueryBalanceResult
                //       .SingleOrDefault().AcctList.BalanceResult
                //       .Where(x => x.BalanceType.StartsWith("C_MAIN_ACCOUNT")).SingleOrDefault().TotalAmount.ToString();

                return new GeneralHuawiResponse
                {
                    ResponseDesc = _ResponseDesc,
                    ResponseType = SoapResponse.Body.QueryBalanceResultMsg.ResultHeader.ResultCode.ToString()
                };
            }
            catch (Exception)
            {
                return new GeneralHuawiResponse
                {
                    ResponseDesc = "باقی مانده شارژ مشترک پیدا نشد.",
                    ResponseType = "-2"
                };
            }
        }

        public async Task<GeneralHuawiResponse> RechargeByBroker(RechargeByBrokerTcpRequest model)
        {
            TopinLite.Domain.HuaweiApiModel.CRMResponses.Recharge.EnvelopeRechargeResponse SoapResponse =
                 await _Client.RechargeByBroker(model.PrimaryIdentity, model.Amount, model.Mss, model.BrokerId, model.RechargeChannelID, model.TradeType, model.BeId);
            TopinLite.Domain.HuaweiApiModel.CRMResponses.Recharge.RechargeResultMsg SoapBody = SoapResponse.Body.RechargeResultMsg;

            if (SoapBody.ResultHeader.ResultCode.ToString() != "0")
                return new GeneralHuawiResponse
                {
                    ResponseType = SoapBody.ResultHeader.ResultCode.ToString(),
                    ResponseDesc = SoapBody.ResultHeader.ResultDesc
                };

            if (SoapBody.RechargeResult == null)
                return new GeneralHuawiResponse
                {
                    ResponseType = "-2",
                    ResponseDesc = "RechargeResult is null!!!"
                };

            if (SoapBody.RechargeResult.BalanceChgInfo == null)
                return new GeneralHuawiResponse
                {
                    ResponseType = "0",
                    ResponseDesc = $"0;{SoapBody.RechargeResult.RechargeSerialNo}"
                };
            return new GeneralHuawiResponse
            {
                ResponseType = "0",
                ResponseDesc = SoapBody.RechargeResult.BalanceChgInfo[0].NewBalanceAmt.ToString() + ";" + SoapBody.RechargeResult.RechargeSerialNo
            };
        }

        public async Task<GeneralHuawiResponse> QuerySimType(QuerySimTypeTcpRequest model)
        {
            TopinLite.Domain.HuaweiApiModel.CRMResponses.IntegrationEnquiry.EnvelopeIntegrationEnquiryResponse SoapResponse =
                await _Client.IntegrationEnquiry(model.SubscriberNo, "0", model.Mss);

            string _ResultCode = SoapResponse.Body.IntegrationEnquiryResultMsg.ResultHeader.ResultCode.ToString();
            string _ResultDesc = SoapResponse.Body.IntegrationEnquiryResultMsg.ResultHeader.ResultDesc.ToString();

            if (_ResultCode != "405000000" && _ResultCode != "0")
            {
                return new GeneralHuawiResponse { ResponseDesc = _ResultDesc, ResponseType = _ResultCode };
            }

            string ResponseDesc = SoapResponse.Body.IntegrationEnquiryResultMsg.IntegrationEnquiryResult.BalanceRecordList.Any(x => x.AccountType == "2000") ? "PREPAID" : "BC";

            string ResponseType = SoapResponse.Body.IntegrationEnquiryResultMsg.ResultHeader.ResultCode.ToString() == "405000000" ? "0" : SoapResponse.Body.IntegrationEnquiryResultMsg.ResultHeader.ResultCode.ToString();

            return new GeneralHuawiResponse { ResponseDesc = ResponseDesc, ResponseType = ResponseType };
        }

        public async Task<GeneralHuawiResponse> QueryCustomerInfoByTel(QueryCustomerInfoByTelTcpRequest model)
        {
            TopinLite.Domain.HuaweiApiModel.CRMResponses.QueryCustomerInfo.EnvelopeQueryCustomerInfoReponse SoapResponse = await _Client.QueryCustomerInfoByTel(model.PrimaryIdentity, model.Mss);

            string resultCode = SoapResponse.Body.queryCustomerInfoRspMsg.resultHeader.resultCode.ToString();
            string resultDesc = SoapResponse.Body.queryCustomerInfoRspMsg.resultHeader.resultDesc;

            if (resultCode != "0")
                return new GeneralHuawiResponse { ResponseType = resultCode, ResponseDesc = resultDesc };
            if (SoapResponse.Body.queryCustomerInfoRspMsg.queryCustomerInfoResponse == null)
                return new GeneralHuawiResponse { ResponseType = "-2", ResponseDesc = "queryCustomerInfoResponse is null!" };

            TopinLite.Domain.HuaweiApiModel.CRMResponses.QueryCustomerInfo.IndividualDetailType individualDetail = SoapResponse.Body.queryCustomerInfoRspMsg.queryCustomerInfoResponse.customer.Item as TopinLite.Domain.HuaweiApiModel.CRMResponses.QueryCustomerInfo.IndividualDetailType;
            TopinLite.Domain.HuaweiApiModel.CRMResponses.QueryCustomerInfo.OrganizationDetailType organizationDetail = SoapResponse.Body.queryCustomerInfoRspMsg.queryCustomerInfoResponse.customer.Item as TopinLite.Domain.HuaweiApiModel.CRMResponses.QueryCustomerInfo.OrganizationDetailType;

            if (!SoapResponse.Body.queryCustomerInfoRspMsg.queryCustomerInfoResponse.customer.custBasicInfo.custCode.Equals("1"))
                resultDesc = "2;";
            else
                resultDesc = "1;";

            //TopinLite.Domain.HuaweiApiModel.CRMResponses.QueryCustomerInfo.individualInfoIndividualBaseInfo individualInfo = SoapResponse.Body.queryCustomerInfoRspMsg.queryCustomerInfoResponse.customer.individualInfo.individualBaseInfo;

            //TopinLite.Domain.HuaweiApiModel.CRMResponses.QueryCustomerInfo.individualInfo individualInfo2 = SoapResponse.Body.queryCustomerInfoRspMsg.queryCustomerInfoResponse.customer.individualInfo;

            //TopinLite.Domain.HuaweiApiModel.CRMResponses.QueryCustomerInfo.customer.organizationDetailType organizationDetail = SoapResponse.Body.queryCustomerInfoRspMsg.queryCustomerInfoResponse.customer.orgInfo;

            if (individualDetail != null)
            {
                resultDesc += individualDetail.individualBaseInfo.firstName + ";" + individualDetail.individualBaseInfo.middleName + ";" +
                    individualDetail.individualBaseInfo.lastName + ";" + individualDetail.individualBaseInfo.gender + ";" +
                   individualDetail.individualBaseInfo.birthday + ";" + individualDetail.individualBaseInfo.nationality + ";";

                if (individualDetail.certificationInfo != null)
                {
                    var ss = individualDetail.certificationInfo.Select(x => x).SingleOrDefault();
                    resultDesc += ss.idType + ";" + ss.idNumber + ";";
                }
                else
                    resultDesc += ";;";

                resultDesc += SoapResponse.Body.queryCustomerInfoRspMsg.queryCustomerInfoResponse.customer.customerId;
            }
            else if (organizationDetail != null)
            {
                resultDesc += organizationDetail.organizationBaseInfo.orgId + ";" + organizationDetail.organizationBaseInfo.orgIdSpecified + ";" +
                    organizationDetail.organizationBaseInfo.orgShortName + ";" + organizationDetail.organizationBaseInfo.orgName + ";";

                if (organizationDetail.certificationInfo != null)
                {
                    var ss = organizationDetail.certificationInfo.Select(x => x).SingleOrDefault();
                    resultDesc += ss.idType + ";" + ss.idNumber + ";" + SoapResponse.Body.queryCustomerInfoRspMsg.queryCustomerInfoResponse.customer.customerId;
                }
                else
                    resultDesc += ";" + ";" + SoapResponse.Body.queryCustomerInfoRspMsg.queryCustomerInfoResponse.customer.customerId;
            }

            return new GeneralHuawiResponse { ResponseType = "0", ResponseDesc = resultDesc };
        }

        public async Task<GeneralHuawiResponse<QuerySubscriberResponse>> QuerySubscriber(QuerySubscriberTcpRequest model)
        {
            TopinLite.Domain.HuaweiApiModel.CRMResponses.QuerySubscriber.EnvelopeQuerySubscriberResponse SoapResponse =
                await _Client.QuerySubscriber(model.PrimaryIdentity, model.Mss, "N", "N", "N", "N");

            if (SoapResponse.Body.Fault is not null)
            {
                return new GeneralHuawiResponse<QuerySubscriberResponse> { ResponseDesc = SoapResponse.Body.Fault.Reason.Text, ResponseType = SoapResponse.Body.Fault.Code.Value };
            }

            TopinLite.Domain.HuaweiApiModel.CRMResponses.QuerySubscriber.querySubscriberRspMsg ResultData = SoapResponse.Body.querySubscriberRspMsg;

            if (ResultData.resultHeader.resultCode != "0")
            {
                return new GeneralHuawiResponse<QuerySubscriberResponse> { ResponseDesc = ResultData.resultHeader.resultDesc, ResponseType = ResultData.resultHeader.resultCode };
            }

            if (ResultData.querySubscriberResponse == null)
            {
                return new GeneralHuawiResponse<QuerySubscriberResponse> { ResponseType = "-2", ResponseDesc = "querySubscriberResponse is null!" };
            }

            //TopinLite.Domain.HuaweiApiModel.CRMResponses.QuerySubscriber.subInfo SingleData = ResultData.querySubscriberResponse.SingleOrDefault().subInfo;
            var SingleData = ResultData.querySubscriberResponse.subscriberInfo.SingleOrDefault().subInfo;

            string responseDesc = SingleData.subsId + ";" + SingleData.subsName + ";" + SingleData.offeringId + ";" + SingleData.paymentType + ";" + SingleData.imsi + ";" + SingleData.iccid + ";" +
                     SingleData.portFlag + ";" + SingleData.activeDate + ";" + SingleData.status + ";" + SingleData.statusTime + ";" + SingleData.statusDetail + ";" + SingleData.salesChannelType;

            return new GeneralHuawiResponse<QuerySubscriberResponse> { ResponseType = "0", ResponseDesc = responseDesc, Data = SoapResponse.Body.querySubscriberRspMsg.querySubscriberResponse };
        }

        public async Task<GeneralHuawiResponse> QuerySubscriberCZ2(QuerySubscriberCZ2TcpRequest model)
        {
            TopinLite.Domain.HuaweiApiModel.CRMResponses.QuerySubscriberCZ2.EnvelopeQuerySubscriberCZ2Response SoapResponse = await _Client.QuerySubscriberCz2(model.PrimaryIdentity, model.Mss);

            if (SoapResponse.Body.querySubscriberCZ2RspMsg.querySubscriberCZ2Response == null)
                return new GeneralHuawiResponse
                {
                    ResponseType = "-2",
                    ResponseDesc = "querySubscriberCZ2Response is null!"
                };

            if (SoapResponse.Body.querySubscriberCZ2RspMsg.resultHeader.resultCode.ToString() != "0")
                return new GeneralHuawiResponse
                {
                    ResponseType = SoapResponse.Body.querySubscriberCZ2RspMsg.resultHeader.resultCode.ToString(),
                    ResponseDesc = SoapResponse.Body.querySubscriberCZ2RspMsg.resultHeader.resultDesc
                };

            string RResultDesc = SoapResponse.Body.querySubscriberCZ2RspMsg.resultHeader.resultDesc;
            string RResultType = SoapResponse.Body.querySubscriberCZ2RspMsg.resultHeader.resultCode.ToString();

            if (SoapResponse.Body.querySubscriberCZ2RspMsg.querySubscriberCZ2Response
                .AdditionalProperty.Count(o => o.value != null && o.value.ToUpper() == model.ProviderId.ToUpper()) != 0)
                RResultDesc = "1";
            else
                RResultDesc = "2";

            return new GeneralHuawiResponse { ResponseDesc = RResultDesc, ResponseType = RResultType };
        }

        public async Task<GeneralHuawiResponse> CheckOfferEligibility(CheckOfferEligibilityTcpRequest model)
        {
            TopinLite.Domain.HuaweiApiModel.CRMResponses.CheckOfferEligibility.EnvelopCheckOfferEligibilityResponse SoapResponse =
                 await _Client.CheckOfferEligibility(model.PrimaryIdentity, model.Mss, model.OfferId);

            TopinLite.Domain.HuaweiApiModel.CRMResponses.CheckOfferEligibility.ResultHeader resultHeader =
                SoapResponse?.Body?.CheckOfferingEligibilityRspMsg?.resultHeader;

            if (resultHeader == null)
            {
                return new GeneralHuawiResponse
                {
                    ResponseType = "-2",
                    ResponseDesc = "CheckOfferingEligibility response is null!"
                };
            }

            string responseType = resultHeader.resultCode?.ToString() ?? "-1";
            string responseDesc = resultHeader.resultDesc ?? string.Empty;

            if (responseType != "0")
            {
                return new GeneralHuawiResponse
                {
                    ResponseType = responseType,
                    ResponseDesc = responseDesc
                };
            }

            TopinLite.Domain.HuaweiApiModel.CRMResponses.CheckOfferEligibility.CheckOfferingEligibilityResponse checkOfferingEligibilityResponse =
                SoapResponse.Body.CheckOfferingEligibilityRspMsg.CheckOfferingEligibilityResponse;

            if (checkOfferingEligibilityResponse?.AdditionalProperty != null &&
                checkOfferingEligibilityResponse.AdditionalProperty.Length > 0)
            {
                string additionalProps = string.Join(
                    ";",
                    checkOfferingEligibilityResponse.AdditionalProperty
                        .Where(x => x != null && !string.IsNullOrWhiteSpace(x.code))
                        .Select(x => $"{x.code}={x.value ?? string.Empty}"));

                if (!string.IsNullOrWhiteSpace(additionalProps))
                {
                    responseDesc = string.IsNullOrWhiteSpace(responseDesc)
                        ? additionalProps
                        : $"{responseDesc}|{additionalProps}";
                }
            }

            return new GeneralHuawiResponse
            {
                ResponseType = responseType,
                ResponseDesc = responseDesc
            };
        }

        public async Task<GeneralHuawiResponse> IntegrationEnquiry(IntegrationEnquiryTcpRequest model)
        {
            TopinLite.Domain.HuaweiApiModel.CRMResponses.IntegrationEnquiry.EnvelopeIntegrationEnquiryResponse SoapResponse =
                 await _Client.IntegrationEnquiry(model.PrimaryIdentity, "0", model.Mss);

            string ResponseType = SoapResponse.Body.IntegrationEnquiryResultMsg.ResultHeader.ResultCode.ToString();
            string ResponseDesc = string.Empty;

            if (ResponseType == "405000000")
            {
                ResponseType = "0";
            }
            else
            {
                return new GeneralHuawiResponse { ResponseDesc = SoapResponse.Body.IntegrationEnquiryResultMsg.ResultHeader.ResultDesc, ResponseType = ResponseType };
            }

            if (SoapResponse.Body.IntegrationEnquiryResultMsg.IntegrationEnquiryResult.SubscriberState == null)
            {
                return new GeneralHuawiResponse { ResponseDesc = "SubscriberState is null !!", ResponseType = "-1" };
            }

            decimal subscriberPrimaryOffer = decimal.Parse(SoapResponse.Body.IntegrationEnquiryResultMsg.IntegrationEnquiryResult.SubscriberInfo.Subscriber.MainProductID);

            if (model.PrimaryOffers.Count(o => o.Offer.ToString() == subscriberPrimaryOffer.ToString() && o.Type == 2) != 0)
            {
                return new GeneralHuawiResponse { ResponseDesc = "Primary offer has been black listed.", ResponseType = "5" };
            }

            if (model.PrimaryOfferWhiteLists.Any(x => x.offerId == subscriberPrimaryOffer && x.type == 2))
            {
                return new GeneralHuawiResponse { ResponseDesc = "Primary offer has been black listed.", ResponseType = "5" };
            }

            string SimType = "BC";
            long Balance = 0;

            TopinLite.Domain.HuaweiApiModel.CRMResponses.IntegrationEnquiry.BalanceRecordType BalanceItem = SoapResponse.Body.IntegrationEnquiryResultMsg.IntegrationEnquiryResult.BalanceRecordList.Where(x => x.AccountType == "2000").FirstOrDefault();

            if (BalanceItem != null)
            {
                SimType = "PREPAID";
                Balance = BalanceItem.Balance;
            }

            int? SubscriberStatus = 0;
            switch (SimType)
            {
                case "PREPAID":
                    SubscriberStatus = SoapResponse.Body.IntegrationEnquiryResultMsg.IntegrationEnquiryResult.SubscriberState.LifeCycleState;
                    break;

                case "BC":
                    SubscriberStatus = SoapResponse.Body.IntegrationEnquiryResultMsg.IntegrationEnquiryResult.SubscriberState.POSUserState;
                    break;
            }

            if (SoapResponse.Body.IntegrationEnquiryResultMsg.IntegrationEnquiryResult.SubscriberState.DPFlag != 0)  // DPFlag=0 unbarring &&
            {
                ResponseDesc = SoapResponse.Body.IntegrationEnquiryResultMsg.IntegrationEnquiryResult.SubscriberState.DPFlag.ToString();
                ResponseType = "1";
            }
            else if (SoapResponse.Body.IntegrationEnquiryResultMsg.IntegrationEnquiryResult.SubscriberState.FraudState != 0)  //FraudState=0  not  blacklist
            {
                ResponseDesc = SoapResponse.Body.IntegrationEnquiryResultMsg.IntegrationEnquiryResult.SubscriberState.FraudState.ToString();
                ResponseType = "2";
            }
            else if (SoapResponse.Body.IntegrationEnquiryResultMsg.IntegrationEnquiryResult.SubscriberState.LossFlag != 0)// LossFlag=0 not claimed loss
            {
                ResponseDesc = SoapResponse.Body.IntegrationEnquiryResultMsg.IntegrationEnquiryResult.SubscriberState.LossFlag.ToString();
                ResponseType = "3";
            }
            else
            {
                ResponseDesc = "0";
                ResponseType = "0";
            }

            return new GeneralHuawiResponse
            {
                ResponseDesc = $"{ResponseDesc}|{SimType}|{Balance}|{SubscriberStatus}|{subscriberPrimaryOffer}",
                ResponseType = ResponseType,
            };
        }

        public async Task<GeneralHuawiResponse> DeleteOffering(DeleteOfferingTcpRequest model)
        {
            TopinLite.Domain.HuaweiApiModel.CRMResponses.ChangeSubscriberOffering.EnvelopeChangeSubscriberOfferingResponse SoapResponse =
                 await _Client.DeleteOffering(model.PrimaryIdentity, model.OfferingId, model.Mss);

            if (SoapResponse.Body.changeSubscriberOfferingRspMsg.resultHeader.resultCode.ToString() != "0")
            {
                return new GeneralHuawiResponse
                {
                    ResponseType = SoapResponse.Body.changeSubscriberOfferingRspMsg.resultHeader.resultCode.ToString(),
                    ResponseDesc = SoapResponse.Body.changeSubscriberOfferingRspMsg.resultHeader.resultDesc
                };
            }

            if (SoapResponse.Body.changeSubscriberOfferingRspMsg.changeSubscriberOfferingResponse == null)
            {
                return new GeneralHuawiResponse
                {
                    ResponseType = "-2",
                    ResponseDesc = "queryOfferingsResponse is null!"
                };
            }

            return new GeneralHuawiResponse
            {
                ResponseType = SoapResponse.Body.changeSubscriberOfferingRspMsg.resultHeader.resultCode.ToString(),
                ResponseDesc = SoapResponse.Body.changeSubscriberOfferingRspMsg.changeSubscriberOfferingResponse.orderId.ToString(),
            };
        }

        public async Task<GeneralHuawiResponse> QueryRelationOffering(QueryRelationOfferingTcpRequest model)
        {
            TopinLite.Domain.HuaweiApiModel.CRMResponses.QueryRelationOffering.EnvelopeQueryRelationOfferingResponse soapResponse = await _Client.QueryRelationOffering(model.MSISDN, model.MessageSeq, model.OfferId, model.RelationType);

            if (soapResponse.Body.QueryRelationOfferingRspMsg.resultHeader.resultCode.ToString() != "0")
            {
                return new GeneralHuawiResponse
                {
                    ResponseDesc = soapResponse.Body.QueryRelationOfferingRspMsg.resultHeader.resultDesc,
                    ResponseType = soapResponse.Body.QueryRelationOfferingRspMsg.resultHeader.resultCode.ToString()
                };
            }

            if (soapResponse.Body.QueryRelationOfferingRspMsg.QueryRelationOfferingResponse == null)
            {
                return new GeneralHuawiResponse
                {
                    ResponseType = "-2",
                    ResponseDesc = "queryRelationOfferingResponse is null!"
                };
            }

            return new GeneralHuawiResponse
            {
                ResponseDesc = string.Join('|', soapResponse.Body.QueryRelationOfferingRspMsg.QueryRelationOfferingResponse.OfferingID),
                ResponseType = soapResponse.Body.QueryRelationOfferingRspMsg.resultHeader.resultCode.ToString()
            };
        }

        public async Task<GeneralHuawiResponse<List<LocalOfferModel>>> GetOfferingList(GetOfferingListTcpRequest model)
        {
            Domain.HuaweiApiModel.CRMResponses.QuerySubscriberCZ2.EnvelopeQuerySubscriberCZ2Response response = await _Client.GetOfferingList(model.TelNum, model.MessageSeq, model.ContractFlag, model.HistoryFlag, model.OfferFlag, model.ProdFlag, model.OttFlag, model.DivertFlag);

            string resultCode = response.Body.querySubscriberCZ2RspMsg.resultHeader.resultCode;
            string resultDesc = response.Body.querySubscriberCZ2RspMsg.resultHeader.resultDesc;

            Console.WriteLine($"|||||||||||||||||||||||||resultCode={resultCode}");
            Console.WriteLine($"|||||||||||||||||||||||||resultDesc={resultDesc}");

            if (resultCode.Equals("0"))
            {
                List<AllOfferingRecord> responseJson = new List<AllOfferingRecord>();

                if (response.Body.querySubscriberCZ2RspMsg.querySubscriberCZ2Response.subscriberInfo is null)
                {
                    return new GeneralHuawiResponse<List<LocalOfferModel>>
                    {
                        ResponseDesc = "SubscriberInfo is null",
                        ResponseType = "-1"
                    };
                }

                if (response.Body.querySubscriberCZ2RspMsg.querySubscriberCZ2Response.subscriberInfo.Count() == 0)
                {
                    return new GeneralHuawiResponse<List<LocalOfferModel>>
                    {
                        ResponseDesc = "No records in SubscriberInfo",
                        ResponseType = "-1"
                    };
                }

                List<LocalOfferModel> ResponseData = new List<LocalOfferModel>();

                foreach (var subscriber in response.Body.querySubscriberCZ2RspMsg.querySubscriberCZ2Response.subscriberInfo)
                {
                    if (subscriber.supplementaryOfferingList is not null)
                    {
                        foreach (var innerItem in subscriber.supplementaryOfferingList)
                        {
                            ResponseData.Add(new LocalOfferModel { OfferId = innerItem.offeringBasic.offeringId.ToString() });
                        }
                    }
                }

                return new GeneralHuawiResponse<List<LocalOfferModel>>
                {
                    Data = ResponseData,
                    ResponseDesc = resultDesc,
                    ResponseType = resultCode
                };
            }
            else
            {
                return new GeneralHuawiResponse<List<LocalOfferModel>>
                {
                    ResponseDesc = resultDesc,
                    ResponseType = resultCode
                };
            }
        }
    }
}