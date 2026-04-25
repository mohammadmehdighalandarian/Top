using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TopinLite.Domain.HuawiMicroGateway
{

    internal class GetOfferingList
    {
    }
    public class GetOfferingListRequestModel : MassTransitRequestBaseModel { }
    public class GetOfferingListResponseModel : MassTransitResponseBaseModel
    {
        public object ResponseList { get; set; }
    }
    public interface IGetOfferingListRequestModel : IMassTransitRequestBaseModel { }

    public class GetOfferingListTcpRequest
    {
        public string TelNum { get; set; }
        public string Status { get; set; }
        public string OttFlag { get; set; }
        public string OfferId { get; set; }
        public string Category { get; set; }
        public string DivertFlag { get; set; }
        public string MessageSeq { get; set; }
        public string ContractFlag { get; set; }
        public string HistoryFlag { get; set; }
        public string OfferFlag { get; set; }
        public string ProdFlag { get; set; }
    }

    public class LocalOfferModel
    {
        public string OfferId { get; set; }
    }


    public class AllOfferingRecord
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string offeringId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string status { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string statusDetail { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string expDate { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string activeDate { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? hasOttOffer { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? hasVolumeOttOffer { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ottOfferExpirationDate { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ottOfferCycleCount { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ottOfferTotalCount { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ottRenewOfferId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? createOttOffer { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ottOfferStartDate { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ottOfferEndDate { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ottOfferDuration { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? canRenewAgainOffer { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? purchaseSeq { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? offeringInstId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? divertMsisdn { get; set; }
    }
}
