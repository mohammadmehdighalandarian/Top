
using System.Collections.Concurrent;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml.Serialization;

using TopinLite.Infra.ApiClient.SOAPApi.HuaweiEndpoint.Statics;
using TopinLite.Domain.HuaweiApiModel.CRMResponses;
using TopinLite.Domain.HuawiMicroGateway;

namespace TopinLite.Infra.ApiClient.SOAPApi.HuaweiEndpoint
{
    public class SoapClient : ISoapClient
    {
        private readonly HttpClient httpClient;
        private static readonly XmlSerializerFactory SerializerFactory = new();
        private static readonly ConcurrentDictionary<Type, XmlSerializer> SerializerCache = new();
        private static readonly ConcurrentDictionary<Type, object> SerializerLocks = new();

        public SoapClient(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("ESB");
        }

        private static XmlSerializer GetSerializer(Type type)
        {
            return SerializerCache.GetOrAdd(type, t => SerializerFactory.CreateSerializer(t));
        }

        private static object GetSerializerLock(Type type)
        {
            return SerializerLocks.GetOrAdd(type, _ => new object());
        }

        private static T? DeserializeXml<T>(string payload)
        {
            Type serializerType = typeof(T);
            XmlSerializer serializer = GetSerializer(serializerType);

            lock (GetSerializerLock(serializerType))
            {
                using StringReader xmlReader = new(payload);
                return (T?)serializer.Deserialize(xmlReader);
            }
        }

        public async Task<TopinLite.Domain.HuaweiApiModel.CRMResponses.QueryBalance.EnvelopeQueryBalanceResponse> QueryBalance(string PrimaryIdentity, string Mss)
        {
            if (string.IsNullOrEmpty(Mss))
                Mss = "Topup/Topin" + Guid.NewGuid().ToString();
            using (HttpRequestMessage request = new(HttpMethod.Post, SoapServiceUrls.ArUrl))
            {
                request.Content = new StringContent(RequestStrings.QueryBalanceSoapRequest(PrimaryIdentity, Mss));
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
                request.Content.Headers.Add("SOAPAction", "QueryBalance");
                HttpResponseMessage response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false); ;
                response.EnsureSuccessStatusCode();
                string createdContent = await response.Content.ReadAsStringAsync();
                return DeserializeXml<TopinLite.Domain.HuaweiApiModel.CRMResponses.QueryBalance.EnvelopeQueryBalanceResponse>(createdContent);
            }
        }

        public async Task<TopinLite.Domain.HuaweiApiModel.CRMResponses.IntegrationEnquiry.EnvelopeIntegrationEnquiryResponse> IntegrationEnquiry(string SubscriberNo, string QueryType, string Mss)
        {
            if (string.IsNullOrEmpty(Mss))
                Mss = "Topup/Topin" + Guid.NewGuid().ToString();
            using (HttpRequestMessage request = new(HttpMethod.Post, SoapServiceUrls.BusinessMgrUrl))
            {
                request.Content = new StringContent(RequestStrings.IntegrationEnquirySoapRequest(SubscriberNo, QueryType, Mss));
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
                request.Content.Headers.Add("SOAPAction", "IntegrationEnquiry");
                HttpResponseMessage response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
                response.EnsureSuccessStatusCode();
                string createdContent = await response.Content.ReadAsStringAsync();

                try
                {
                    return DeserializeXml<TopinLite.Domain.HuaweiApiModel.CRMResponses.IntegrationEnquiry.EnvelopeIntegrationEnquiryResponse>(createdContent);
                }
                catch (Exception)
                {
                    Console.WriteLine($"IntegrationEnquiry error on {DateTime.Now} Response content:{createdContent}");
                    return DeserializeXml<TopinLite.Domain.HuaweiApiModel.CRMResponses.IntegrationEnquiry.EnvelopeIntegrationEnquiryResponse>(createdContent);
                }
            }
        }

        public async Task<TopinLite.Domain.HuaweiApiModel.CRMResponses.Recharge.EnvelopeRechargeResponse> RechargeByBroker(string PrimaryIdentity, long Amount, string Mss, string BrokerId, string RechargeChannelId, string TradeType, string BeId)
        {
            if (string.IsNullOrEmpty(Mss))
                Mss = "Topup/Topin" + Guid.NewGuid().ToString();
            using (HttpRequestMessage request = new(HttpMethod.Post, SoapServiceUrls.ArUrl))
            {
                request.Content = new StringContent(RequestStrings.RechargeByBrokerSoapRequest(PrimaryIdentity, Amount.ToString(), Mss, BrokerId, RechargeChannelId, TradeType, BeId));
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
                request.Content.Headers.Add("SOAPAction", "Recharge");
                HttpResponseMessage response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                string createdContent = await response.Content.ReadAsStringAsync();

                try
                {
                    return DeserializeXml<Domain.HuaweiApiModel.CRMResponses.Recharge.EnvelopeRechargeResponse>(createdContent);
                }
                catch (Exception)
                {
                    Console.WriteLine($"RechargeByBroker error on {DateTime.Now} Response content:{createdContent}");
                    return DeserializeXml<Domain.HuaweiApiModel.CRMResponses.Recharge.EnvelopeRechargeResponse>(createdContent);
                }
            }
        }

        public async Task<TopinLite.Domain.HuaweiApiModel.CRMResponses.QuerySubscriber.EnvelopeQuerySubscriberResponse> QuerySubscriber(string PrimaryIdentity, string Mss, string IncludeOfferFlag, string IncludeHistoryFlag, string IncludeProdFlag, string IncludeContractFlag)
        {
            if (string.IsNullOrEmpty(Mss))
                Mss = "Topup/Topin" + Guid.NewGuid().ToString();
            using (HttpRequestMessage request = new(HttpMethod.Post, SoapServiceUrls.CmUrl))
            {
                var temp = RequestStrings.QuerySubscriber(PrimaryIdentity, Mss, IncludeOfferFlag, IncludeHistoryFlag, IncludeProdFlag, IncludeContractFlag);
                request.Content = new StringContent(RequestStrings.QuerySubscriber(PrimaryIdentity, Mss, IncludeOfferFlag, IncludeHistoryFlag, IncludeProdFlag, IncludeContractFlag));
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
                request.Content.Headers.Add("SOAPAction", "QuerySubscriber");
                HttpResponseMessage response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
                response.EnsureSuccessStatusCode();
                string createdContent = await response.Content.ReadAsStringAsync();

                return DeserializeXml<TopinLite.Domain.HuaweiApiModel.CRMResponses.QuerySubscriber.EnvelopeQuerySubscriberResponse>(createdContent);
            }
        }

        public async Task<TopinLite.Domain.HuaweiApiModel.CRMResponses.QueryCustomerInfo.EnvelopeQueryCustomerInfoReponse> QueryCustomerInfoByTel(string PrimaryIdentity, string Mss)
        {
            if (string.IsNullOrEmpty(Mss))
                Mss = "Topup/Topin" + Guid.NewGuid().ToString();
            using (HttpRequestMessage request = new(HttpMethod.Post, SoapServiceUrls.CmUrl))
            {
                request.Content = new StringContent(RequestStrings.QueryCustomerInfo(PrimaryIdentity, Mss));
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
                request.Content.Headers.Add("SOAPAction", "QueryCustomerInfo");
                HttpResponseMessage response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false); ;
                response.EnsureSuccessStatusCode();
                string createdContent = await response.Content.ReadAsStringAsync();

                return DeserializeXml<TopinLite.Domain.HuaweiApiModel.CRMResponses.QueryCustomerInfo.EnvelopeQueryCustomerInfoReponse>(createdContent);
            }
        }

        public async Task<TopinLite.Domain.HuaweiApiModel.CRMResponses.QuerySubscriberCZ2.EnvelopeQuerySubscriberCZ2Response> QuerySubscriberCz2(string PrimaryIdentity, string Mss)
        {
            if (string.IsNullOrEmpty(Mss))
                Mss = "Topup/Topin" + Guid.NewGuid().ToString();
            using (HttpRequestMessage request = new(HttpMethod.Post, SoapServiceUrls.CmUrl))
            {
                request.Content = new StringContent(RequestStrings.QuerySubscriberCZ2Request(PrimaryIdentity, Mss));
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
                request.Content.Headers.Add("SOAPAction", "QuerySubscriberCZ2");
                HttpResponseMessage response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
                response.EnsureSuccessStatusCode();
                string createdContent = await response.Content.ReadAsStringAsync();

                return DeserializeXml<TopinLite.Domain.HuaweiApiModel.CRMResponses.QuerySubscriberCZ2.EnvelopeQuerySubscriberCZ2Response>(createdContent);
            }
        }

        public async Task<TopinLite.Domain.HuaweiApiModel.CRMResponses.ChangeSubscriberOffering.EnvelopeChangeSubscriberOfferingResponse> ChangeSubscriberOfferingAdd(ChangeSubscribersOfferingAddTcpRequest requestModel)
        {
            if (string.IsNullOrEmpty(requestModel.Mss))
                requestModel.Mss = "Topup/Topin" + Guid.NewGuid().ToString();

            using (HttpRequestMessage request = new(HttpMethod.Post, SoapServiceUrls.CmUrl))
            {
                request.Content = new StringContent(RequestStrings.ChangeSubscriberOfferingRequest(requestModel));
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
                request.Content.Headers.Add("SOAPAction", "ChangeSubscriberOffering");
                HttpResponseMessage response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                string createdContent = await response.Content.ReadAsStringAsync();

                return DeserializeXml<TopinLite.Domain.HuaweiApiModel.CRMResponses.ChangeSubscriberOffering.EnvelopeChangeSubscriberOfferingResponse>(createdContent);
            }
        }

        public async Task<TopinLite.Domain.HuaweiApiModel.CRMResponses.ChangeSubscriberOffering.EnvelopeChangeSubscriberOfferingResponse> DeleteOffering(string PrimaryIdentity, string offeringId, string Mss)
        {
            if (string.IsNullOrEmpty(Mss))
                Mss = "Topup/Topin" + Guid.NewGuid().ToString();
            using (HttpRequestMessage request = new(HttpMethod.Post, SoapServiceUrls.CmUrl))
            {
                request.Content = new StringContent(RequestStrings.DeleteOfferingRequest(PrimaryIdentity, offeringId, Mss));
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
                request.Content.Headers.Add("SOAPAction", "ChangeSubscriberOffering");
                HttpResponseMessage response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                string createdContent = await response.Content.ReadAsStringAsync();

                return DeserializeXml<TopinLite.Domain.HuaweiApiModel.CRMResponses.ChangeSubscriberOffering.EnvelopeChangeSubscriberOfferingResponse>(createdContent);
            }
        }

        public async Task<TopinLite.Domain.HuaweiApiModel.CRMResponses.CheckOfferEligibility.EnvelopCheckOfferEligibilityResponse> CheckOfferEligibility(string PrimaryIdentity, string Mss, string OfferId)
        {
            if (string.IsNullOrEmpty(Mss))
                Mss = "Topup/Topin" + Guid.NewGuid().ToString();
            using (HttpRequestMessage request = new(HttpMethod.Post, SoapServiceUrls.CmUrl))
            {
                request.Content = new StringContent(RequestStrings.CheckOfferEligibilityRequest(PrimaryIdentity, Mss, OfferId));
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
                request.Content.Headers.Add("SOAPAction", "CheckOfferingEligibility");
                HttpResponseMessage response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                string createdContent = await response.Content.ReadAsStringAsync();

                try
                {
                    int nameSpace = -1;

                    if (createdContent.Contains("<ns1:resultCode>"))
                    {
                        nameSpace = 1;
                    }
                    else if (createdContent.Contains("<ns0:resultCode>"))
                    {
                        nameSpace = 0;
                    }
                    else if (createdContent.Contains("<ns2:resultCode>"))
                    {
                        nameSpace = 0;
                    }

                    string BeginStr = string.Empty;
                    string EndStr = string.Empty;

                    string BeginStr2 = string.Empty;
                    string EndStr2 = string.Empty;

                    switch (nameSpace)
                    {
                        case 1:

                            BeginStr = "<ns1:resultCode>";
                            EndStr = "</ns1:resultCode>";

                            BeginStr2 = "<ns1:resultDesc>";
                            EndStr2 = "</ns1:resultDesc>";

                            break;

                        case 0:
                            BeginStr = "<ns0:resultCode>";
                            EndStr = "</ns0:resultCode>";

                            BeginStr2 = "<ns0:resultDesc>";
                            EndStr2 = "</ns0:resultDesc>";
                            break;

                        case 2:
                            BeginStr = "<ns2:resultCode>";
                            EndStr = "</ns2:resultCode>";

                            BeginStr2 = "<ns2:resultDesc>";
                            EndStr2 = "</ns2:resultDesc>";
                            break;

                        default:
                            break;
                    }

                    int pTo = createdContent.LastIndexOf(EndStr);
                    string Rcode = createdContent.Substring(createdContent.IndexOf(BeginStr) + BeginStr.Length, pTo - (createdContent.IndexOf(BeginStr) + BeginStr.Length));

                    pTo = createdContent.LastIndexOf(EndStr2);

                    string Rdesc = createdContent.Substring(createdContent.IndexOf(BeginStr2) + BeginStr2.Length, pTo - (createdContent.IndexOf(BeginStr2) + BeginStr2.Length));
                    Console.WriteLine($"{Rcode}  :::  {Rdesc}");
                    return new TopinLite.Domain.HuaweiApiModel.CRMResponses.CheckOfferEligibility.EnvelopCheckOfferEligibilityResponse
                    {
                        Body = new TopinLite.Domain.HuaweiApiModel.CRMResponses.CheckOfferEligibility.EnvelopeBody
                        {
                            CheckOfferingEligibilityRspMsg = new TopinLite.Domain.HuaweiApiModel.CRMResponses.CheckOfferEligibility.CheckOfferingEligibilityRspMsg
                            {
                                resultHeader = new TopinLite.Domain.HuaweiApiModel.CRMResponses.CheckOfferEligibility.ResultHeader
                                {
                                    resultCode = Rcode,
                                    resultDesc = Rdesc
                                }
                            }
                        }
                    };
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Client error log: " + ex.Message);

                    return new TopinLite.Domain.HuaweiApiModel.CRMResponses.CheckOfferEligibility.EnvelopCheckOfferEligibilityResponse
                    {
                        Body = new TopinLite.Domain.HuaweiApiModel.CRMResponses.CheckOfferEligibility.EnvelopeBody
                        {
                            CheckOfferingEligibilityRspMsg = new TopinLite.Domain.HuaweiApiModel.CRMResponses.CheckOfferEligibility.CheckOfferingEligibilityRspMsg
                            {
                                resultHeader = new TopinLite.Domain.HuaweiApiModel.CRMResponses.CheckOfferEligibility.ResultHeader
                                {
                                    resultCode = "-1",
                                    resultDesc = "Error on response conversion"
                                }
                            }
                        }
                    };
                }
            }
        }

        public async Task<TopinLite.Domain.HuaweiApiModel.CRMResponses.QueryRelationOffering.EnvelopeQueryRelationOfferingResponse> QueryRelationOffering(string PrimaryIdentity, string Mss, string OfferingId, string RelationType)
        {
            if (string.IsNullOrEmpty(Mss))
                Mss = "Topup/Topin" + Guid.NewGuid().ToString();
            using (HttpRequestMessage request = new(HttpMethod.Post, SoapServiceUrls.CmUrl))
            {
                request.Content = new StringContent(RequestStrings.QueryRelationOfferingRequest(PrimaryIdentity, Mss, RelationType, OfferingId));
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
                request.Content.Headers.Add("SOAPAction", "QueryRelationOffering");
                HttpResponseMessage response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                string createdContent = await response.Content.ReadAsStringAsync();

                createdContent = XmlParserLib.RemoveAllNamespaces(createdContent);

                Console.WriteLine($"QueryRelationOffering soap response: {createdContent}");
                return DeserializeXml<TopinLite.Domain.HuaweiApiModel.CRMResponses.QueryRelationOffering.EnvelopeQueryRelationOfferingResponse>(createdContent);
            }
        }

        public async Task<Domain.HuaweiApiModel.CRMResponses.QuerySubscriberCZ2.EnvelopeQuerySubscriberCZ2Response> GetOfferingList(string PrimaryIdentity, string Mss, string ContractFlag, string HistoryFlag, string OfferFlag, string ProdFlag, string OttFlag, string DivertFlag)
        {
            if (string.IsNullOrEmpty(Mss))
                Mss = "Topup/Topin" + Guid.NewGuid().ToString();
            GeneralHuawiResponse result = new GeneralHuawiResponse();
            using (HttpRequestMessage request = new(HttpMethod.Post, SoapServiceUrls.CmUrl))
            {
                request.Content = new StringContent(RequestStrings.QuerySubscriberCZ2Request(PrimaryIdentity, Mss, OttFlag, ContractFlag, HistoryFlag, OfferFlag, ProdFlag, DivertFlag));
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
                request.Content.Headers.Add("SOAPAction", "QuerySubscriberCZ2");
                HttpResponseMessage response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
                response.EnsureSuccessStatusCode();
                string createdContent = await response.Content.ReadAsStringAsync();

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Domain.HuaweiApiModel.CRMResponses.QuerySubscriberCZ2.EnvelopeQuerySubscriberCZ2Response));
                using (StringReader xmlReader = new StringReader(createdContent))
                {
                    return (Domain.HuaweiApiModel.CRMResponses.QuerySubscriberCZ2.EnvelopeQuerySubscriberCZ2Response)xmlSerializer.Deserialize(xmlReader);
                }
            }
        }
    }
}