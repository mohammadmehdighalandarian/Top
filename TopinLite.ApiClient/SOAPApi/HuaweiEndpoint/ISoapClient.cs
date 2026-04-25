using TopinLite.Domain.HuaweiApiModel.CRMResponses.QuerySubscriberCZ2;
using TopinLite.Domain.HuawiMicroGateway;


namespace TopinLite.Infra.ApiClient.SOAPApi.HuaweiEndpoint
{
    public interface ISoapClient
    {
        Task<TopinLite.Domain.HuaweiApiModel.CRMResponses.ChangeSubscriberOffering.EnvelopeChangeSubscriberOfferingResponse> ChangeSubscriberOfferingAdd(ChangeSubscribersOfferingAddTcpRequest requestModel);

        Task<TopinLite.Domain.HuaweiApiModel.CRMResponses.CheckOfferEligibility.EnvelopCheckOfferEligibilityResponse> CheckOfferEligibility(string PrimaryIdentity, string Mss, string OfferId);

        Task<TopinLite.Domain.HuaweiApiModel.CRMResponses.IntegrationEnquiry.EnvelopeIntegrationEnquiryResponse> IntegrationEnquiry(string SubscriberNo, string QueryType, string Mss);

        Task<TopinLite.Domain.HuaweiApiModel.CRMResponses.QueryBalance.EnvelopeQueryBalanceResponse> QueryBalance(string PrimaryIdentity, string Mss);

        Task<TopinLite.Domain.HuaweiApiModel.CRMResponses.QueryCustomerInfo.EnvelopeQueryCustomerInfoReponse> QueryCustomerInfoByTel(string PrimaryIdentity, string Mss);

        Task<TopinLite.Domain.HuaweiApiModel.CRMResponses.QuerySubscriber.EnvelopeQuerySubscriberResponse> QuerySubscriber(string PrimaryIdentity, string Mss, string IncludeOfferFlag, string IncludeHistoryFlag, string IncludeProdFlag, string IncludeContractFlag);

        Task<TopinLite.Domain.HuaweiApiModel.CRMResponses.QuerySubscriberCZ2.EnvelopeQuerySubscriberCZ2Response> QuerySubscriberCz2(string PrimaryIdentity, string Mss);

        Task<TopinLite.Domain.HuaweiApiModel.CRMResponses.Recharge.EnvelopeRechargeResponse> RechargeByBroker(string PrimaryIdentity, long Amount, string Mss, string BrokerId, string RechargeChannelId, string TradeType, string BeId);

        Task<TopinLite.Domain.HuaweiApiModel.CRMResponses.ChangeSubscriberOffering.EnvelopeChangeSubscriberOfferingResponse> DeleteOffering(string PrimaryIdentity, string offeringId, string Mss);

        Task<TopinLite.Domain.HuaweiApiModel.CRMResponses.QueryRelationOffering.EnvelopeQueryRelationOfferingResponse> QueryRelationOffering(string PrimaryIdentity, string Mss, string OfferingId, string RelationType);
        Task<EnvelopeQuerySubscriberCZ2Response> GetOfferingList(string PrimaryIdentity, string Mss, string ContractFlag, string HistoryFlag, string OfferFlag, string ProdFlag, string OttFlag, string DivertFlag);
    }
}