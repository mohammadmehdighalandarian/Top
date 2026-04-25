using System.Text.Json.Serialization;

namespace TopinLite.Domain.HuaweiApiModel.Mg
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class ResultHeader
    {
        [JsonPropertyName("CommandId")]
        public string CommandId { get; set; }

        [JsonPropertyName("Version")]
        public string Version { get; set; }

        [JsonPropertyName("TransactionId")]
        public string TransactionId { get; set; }

        [JsonPropertyName("SequenceId")]
        public string SequenceId { get; set; }

        [JsonPropertyName("ResultCode")]
        public string ResultCode { get; set; }

        [JsonPropertyName("ResultDesc")]
        public string ResultDesc { get; set; }
    }

    public class BalanceRecord
    {
        [JsonPropertyName("BalanceDesc")]
        public string BalanceDesc { get; set; }

        [JsonPropertyName("Balance")]
        public string Balance { get; set; }

        [JsonPropertyName("MinMeasureId")]
        public string MinMeasureId { get; set; }

        [JsonPropertyName("UnitType")]
        public string UnitType { get; set; }

        [JsonPropertyName("AccountType")]
        public string AccountType { get; set; }

        [JsonPropertyName("ExpireTime")]
        public string ExpireTime { get; set; }

        [JsonPropertyName("AccountCredit")]
        public string AccountCredit { get; set; }

        [JsonPropertyName("AccountKey")]
        public string AccountKey { get; set; }

        [JsonPropertyName("ProductID")]
        public string ProductID { get; set; }
    }

    public class BalanceRecordList
    {
        [JsonPropertyName("BalanceRecord")]
        public List<BalanceRecord> BalanceRecord { get; set; }
    }

    public class SubscriberState
    {
        [JsonPropertyName("ActiveCAC")]
        public string ActiveCAC { get; set; }

        [JsonPropertyName("ActiveStop")]
        public string ActiveStop { get; set; }

        [JsonPropertyName("DisableStop")]
        public string DisableStop { get; set; }

        [JsonPropertyName("DPEndDate")]
        public string DPEndDate { get; set; }

        [JsonPropertyName("DPFlag")]
        public string DPFlag { get; set; }

        [JsonPropertyName("FirstActiveDate")]
        public string FirstActiveDate { get; set; }

        [JsonPropertyName("FraudState")]
        public string FraudState { get; set; }

        [JsonPropertyName("LifeCycleState")]
        public string LifeCycleState { get; set; }

        [JsonPropertyName("LockedFlag")]
        public string LockedFlag { get; set; }

        [JsonPropertyName("LossFlag")]
        public string LossFlag { get; set; }

        [JsonPropertyName("POSUserState")]
        public string POSUserState { get; set; }

        [JsonPropertyName("PPSPortout")]
        public string PPSPortout { get; set; }

        [JsonPropertyName("SuspendStop")]
        public string SuspendStop { get; set; }
    }

    public class BillingCycleDate
    {
        [JsonPropertyName("BillCycleEndDate")]
        public string BillCycleEndDate { get; set; }

        [JsonPropertyName("BillCycleOpenDate")]
        public string BillCycleOpenDate { get; set; }

        [JsonPropertyName("BillCycleType")]
        public string BillCycleType { get; set; }
    }

    public class SimpleProperty
    {
        [JsonPropertyName("Id")]
        public string Id { get; set; }

        [JsonPropertyName("Value")]
        public string Value { get; set; }
    }

    public class Subscriber
    {
        [JsonPropertyName("Code")]
        public string Code { get; set; }

        [JsonPropertyName("BrandId")]
        public string BrandId { get; set; }

        [JsonPropertyName("RegistrationTime")]
        public string RegistrationTime { get; set; }

        [JsonPropertyName("Lang")]
        public string Lang { get; set; }

        [JsonPropertyName("SMSLang")]
        public string SMSLang { get; set; }

        [JsonPropertyName("USSDLang")]
        public string USSDLang { get; set; }

        [JsonPropertyName("PaidMode")]
        public string PaidMode { get; set; }

        [JsonPropertyName("InitialCredit")]
        public string InitialCredit { get; set; }

        [JsonPropertyName("BelToAreaID")]
        public string BelToAreaID { get; set; }

        [JsonPropertyName("MainProductID")]
        public string MainProductID { get; set; }

        [JsonPropertyName("SimpleProperty")]
        public List<SimpleProperty> SimpleProperty { get; set; }

        [JsonPropertyName("LastRechargeTime")]
        public string LastRechargeTime { get; set; }

        [JsonPropertyName("LastCallTime")]
        public string LastCallTime { get; set; }

        [JsonPropertyName("IMSI")]
        public string IMSI { get; set; }
    }

    public class Product
    {
        [JsonPropertyName("Id")]
        public string Id { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("ProductOrderKey")]
        public string ProductOrderKey { get; set; }

        [JsonPropertyName("EffectiveDate")]
        public string EffectiveDate { get; set; }

        [JsonPropertyName("ExpiredDate")]
        public string ExpiredDate { get; set; }

        [JsonPropertyName("Status")]
        public string Status { get; set; }

        [JsonPropertyName("CurCycleStartTime")]
        public string CurCycleStartTime { get; set; }

        [JsonPropertyName("CurCycleEndTime")]
        public string CurCycleEndTime { get; set; }

        [JsonPropertyName("BillStatus")]
        public string BillStatus { get; set; }
    }

    public class Service
    {
        [JsonPropertyName("Id")]
        public string Id { get; set; }

        [JsonPropertyName("Status")]
        public string Status { get; set; }

        [JsonPropertyName("RegistrationTime")]
        public string RegistrationTime { get; set; }
    }

    public class SubscriberInfo
    {
        [JsonPropertyName("Subscriber")]
        public Subscriber Subscriber { get; set; }

        [JsonPropertyName("Product")]
        public List<Product> Product { get; set; }

        [JsonPropertyName("Service")]
        public List<Service> Service { get; set; }
    }

    public class CumulativeItem
    {
        [JsonPropertyName("CumulateBeginTime")]
        public string CumulateBeginTime { get; set; }

        [JsonPropertyName("CumulateEndTime")]
        public string CumulateEndTime { get; set; }

        [JsonPropertyName("CumulateID")]
        public string CumulateID { get; set; }

        [JsonPropertyName("CumulativeAmt")]
        public string CumulativeAmt { get; set; }
    }

    public class CumulativeItemList
    {
        [JsonPropertyName("CumulativeItem")]
        public List<CumulativeItem> CumulativeItem { get; set; }
    }

    public class SubAttachedInfo
    {
        [JsonPropertyName("ChgMainProductTimes")]
        public string ChgMainProductTimes { get; set; }

        [JsonPropertyName("ChgMainPackageTimes")]
        public string ChgMainPackageTimes { get; set; }

        [JsonPropertyName("LastRechargeTime")]
        public string LastRechargeTime { get; set; }

        [JsonPropertyName("CallBeginTime")]
        public string CallBeginTime { get; set; }

        [JsonPropertyName("ServiceStart")]
        public string ServiceStart { get; set; }

        [JsonPropertyName("ServiceStop")]
        public string ServiceStop { get; set; }

        [JsonPropertyName("USSDNotifyFlag")]
        public string USSDNotifyFlag { get; set; }

        [JsonPropertyName("MSISDN")]
        public string MSISDN { get; set; }

        [JsonPropertyName("FraudTimes")]
        public string FraudTimes { get; set; }

        [JsonPropertyName("HomeZoneNo1")]
        public string HomeZoneNo1 { get; set; }

        [JsonPropertyName("HomeZoneNo2")]
        public string HomeZoneNo2 { get; set; }

        [JsonPropertyName("HomeZoneNo3")]
        public string HomeZoneNo3 { get; set; }

        [JsonPropertyName("HomeZoneNo4")]
        public string HomeZoneNo4 { get; set; }

        [JsonPropertyName("HomeZoneNo5")]
        public string HomeZoneNo5 { get; set; }
    }

    public class IntegrationEnquiryResult
    {
        [JsonPropertyName("BalanceRecordList")]
        public BalanceRecordList BalanceRecordList { get; set; }

        [JsonPropertyName("SubscriberState")]
        public SubscriberState SubscriberState { get; set; }

        [JsonPropertyName("BillingCycleDate")]
        public BillingCycleDate BillingCycleDate { get; set; }

        [JsonPropertyName("SubscriberInfo")]
        public SubscriberInfo SubscriberInfo { get; set; }

        [JsonPropertyName("CumulativeItemList")]
        public CumulativeItemList CumulativeItemList { get; set; }

        [JsonPropertyName("SubAttachedInfo")]
        public SubAttachedInfo SubAttachedInfo { get; set; }
    }

    public class IntegrationEnquiryResultMsg
    {
        [JsonPropertyName("ResultHeader")]
        public ResultHeader ResultHeader { get; set; }

        [JsonPropertyName("IntegrationEnquiryResult")]
        public IntegrationEnquiryResult IntegrationEnquiryResult { get; set; }
    }

    public class Body
    {
        [JsonPropertyName("IntegrationEnquiryResultMsg")]
        public IntegrationEnquiryResultMsg IntegrationEnquiryResultMsg { get; set; }
    }

    public class Envelope
    {
        [JsonPropertyName("Body")]
        public Body Body { get; set; }
    }

    public class IntegrationEnquiryResponseModel
    {
        [JsonPropertyName("Envelope")]
        public Envelope Envelope { get; set; }
    }
}