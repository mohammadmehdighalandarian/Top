namespace TopinLite.Domain.HuaweiApiModel.CRMResponses.IntegrationEnquiry
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public partial class EnvelopeIntegrationEnquiryResponse
    {
        private EnvelopeBody bodyField;

        /// <remarks/>
        public EnvelopeBody Body
        {
            get
            {
                return bodyField;
            }
            set
            {
                bodyField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public partial class EnvelopeBody
    {
        private IntegrationEnquiryResultMsg integrationEnquiryResultMsgField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgrmsg")]
        public IntegrationEnquiryResultMsg IntegrationEnquiryResultMsg
        {
            get
            {
                return integrationEnquiryResultMsgField;
            }
            set
            {
                integrationEnquiryResultMsgField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgrmsg")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgrmsg", IsNullable = false)]
    public partial class IntegrationEnquiryResultMsg
    {
        private ResultHeader resultHeaderField;

        private IntegrationEnquiryResult integrationEnquiryResultField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public ResultHeader ResultHeader
        {
            get
            {
                return resultHeaderField;
            }
            set
            {
                resultHeaderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public IntegrationEnquiryResult IntegrationEnquiryResult
        {
            get
            {
                return integrationEnquiryResultField;
            }
            set
            {
                integrationEnquiryResultField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
    public partial class ResultHeader
    {
        private string commandIdField;

        private decimal versionField;

        private int transactionIdField;

        private string sequenceIdField;

        private decimal resultCodeField;

        private string resultDescField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bme/cbsinterface/common")]
        public string CommandId
        {
            get
            {
                return commandIdField;
            }
            set
            {
                commandIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bme/cbsinterface/common")]
        public decimal Version
        {
            get
            {
                return versionField;
            }
            set
            {
                versionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bme/cbsinterface/common")]
        public int TransactionId
        {
            get
            {
                return transactionIdField;
            }
            set
            {
                transactionIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bme/cbsinterface/common")]
        public string SequenceId
        {
            get
            {
                return sequenceIdField;
            }
            set
            {
                sequenceIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bme/cbsinterface/common")]
        public decimal ResultCode
        {
            get
            {
                return resultCodeField;
            }
            set
            {
                resultCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bme/cbsinterface/common")]
        public string ResultDesc
        {
            get
            {
                return resultDescField;
            }
            set
            {
                resultDescField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr")]
    public partial class IntegrationEnquiryResult
    {

        private BalanceRecordType[] balanceRecordListField;

        private IntegrationEnquiryResultSubscriberState subscriberStateField;

        private IntegrationEnquiryResultBillingCycleDate billingCycleDateField;

        private IntegrationEnquiryResultSubscriberInfo subscriberInfoField;

        private IntegrationEnquiryResultCumulativeItem[] cumulativeItemListField;

        private IntegrationEnquiryResultUserGroup[] userGroupListField;

        private IntegrationEnquiryResultSubAttachedInfo subAttachedInfoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("BalanceRecord", IsNullable = false)]
        public BalanceRecordType[] BalanceRecordList
        {
            get
            {
                return balanceRecordListField;
            }
            set
            {
                balanceRecordListField = value;
            }
        }

        /// <remarks/>
        public IntegrationEnquiryResultSubscriberState SubscriberState
        {
            get
            {
                return subscriberStateField;
            }
            set
            {
                subscriberStateField = value;
            }
        }

        /// <remarks/>
        public IntegrationEnquiryResultBillingCycleDate BillingCycleDate
        {
            get
            {
                return billingCycleDateField;
            }
            set
            {
                billingCycleDateField = value;
            }
        }

        /// <remarks/>
        public IntegrationEnquiryResultSubscriberInfo SubscriberInfo
        {
            get
            {
                return subscriberInfoField;
            }
            set
            {
                subscriberInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("CumulativeItem", IsNullable = false)]
        public IntegrationEnquiryResultCumulativeItem[] CumulativeItemList
        {
            get
            {
                return cumulativeItemListField;
            }
            set
            {
                cumulativeItemListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("UserGroup", IsNullable = false)]
        public IntegrationEnquiryResultUserGroup[] UserGroupList
        {
            get
            {
                return userGroupListField;
            }
            set
            {
                userGroupListField = value;
            }
        }

        /// <remarks/>
        public IntegrationEnquiryResultSubAttachedInfo SubAttachedInfo
        {
            get
            {
                return subAttachedInfoField;
            }
            set
            {
                subAttachedInfoField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr")]
    public partial class BalanceRecordListBalanceRecord
    {
        private string balanceDescField;

        private long balanceField;

        private int minMeasureIdField;

        private int unitTypeField;

        private int accountTypeField;

        private long expireTimeField;

        private int accountCreditField;

        private long accountKeyField;

        private int productIDField;

        private bool productIDFieldSpecified;

        /// <remarks/>
        public string BalanceDesc
        {
            get
            {
                return balanceDescField;
            }
            set
            {
                balanceDescField = value;
            }
        }

        /// <remarks/>
        public long Balance
        {
            get
            {
                return balanceField;
            }
            set
            {
                balanceField = value;
            }
        }

        /// <remarks/>
        public int MinMeasureId
        {
            get
            {
                return minMeasureIdField;
            }
            set
            {
                minMeasureIdField = value;
            }
        }

        /// <remarks/>
        public int UnitType
        {
            get
            {
                return unitTypeField;
            }
            set
            {
                unitTypeField = value;
            }
        }

        /// <remarks/>
        public int AccountType
        {
            get
            {
                return accountTypeField;
            }
            set
            {
                accountTypeField = value;
            }
        }

        /// <remarks/>
        public long ExpireTime
        {
            get
            {
                return expireTimeField;
            }
            set
            {
                expireTimeField = value;
            }
        }

        /// <remarks/>
        public int AccountCredit
        {
            get
            {
                return accountCreditField;
            }
            set
            {
                accountCreditField = value;
            }
        }

        /// <remarks/>
        public long AccountKey
        {
            get
            {
                return accountKeyField;
            }
            set
            {
                accountKeyField = value;
            }
        }

        /// <remarks/>
        public int ProductID
        {
            get
            {
                return productIDField;
            }
            set
            {
                productIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool ProductIDSpecified
        {
            get
            {
                return productIDFieldSpecified;
            }
            set
            {
                productIDFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr", IsNullable = false)]
    public partial class SubscriberState
    {
        private int activeCACField;

        private int dPEndDateField;

        private int dPFlagField;

        private int fraudStateField;

        private int lifeCycleStateField;

        private int lockedFlagField;

        private int lossFlagField;

        private int pOSUserStateField;

        /// <remarks/>
        public int ActiveCAC
        {
            get
            {
                return activeCACField;
            }
            set
            {
                activeCACField = value;
            }
        }

        /// <remarks/>
        public int DPEndDate
        {
            get
            {
                return dPEndDateField;
            }
            set
            {
                dPEndDateField = value;
            }
        }

        /// <remarks/>
        public int DPFlag
        {
            get
            {
                return dPFlagField;
            }
            set
            {
                dPFlagField = value;
            }
        }

        /// <remarks/>
        public int FraudState
        {
            get
            {
                return fraudStateField;
            }
            set
            {
                fraudStateField = value;
            }
        }

        /// <remarks/>
        public int LifeCycleState
        {
            get
            {
                return lifeCycleStateField;
            }
            set
            {
                lifeCycleStateField = value;
            }
        }

        /// <remarks/>
        public int LockedFlag
        {
            get
            {
                return lockedFlagField;
            }
            set
            {
                lockedFlagField = value;
            }
        }

        /// <remarks/>
        public int LossFlag
        {
            get
            {
                return lossFlagField;
            }
            set
            {
                lossFlagField = value;
            }
        }

        /// <remarks/>
        public int POSUserState
        {
            get
            {
                return pOSUserStateField;
            }
            set
            {
                pOSUserStateField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr", IsNullable = false)]
    public partial class BillingCycleDate
    {
        private int billCycleEndDateField;

        private int billCycleOpenDateField;

        private int billCycleTypeField;

        /// <remarks/>
        public int BillCycleEndDate
        {
            get
            {
                return billCycleEndDateField;
            }
            set
            {
                billCycleEndDateField = value;
            }
        }

        /// <remarks/>
        public int BillCycleOpenDate
        {
            get
            {
                return billCycleOpenDateField;
            }
            set
            {
                billCycleOpenDateField = value;
            }
        }

        /// <remarks/>
        public int BillCycleType
        {
            get
            {
                return billCycleTypeField;
            }
            set
            {
                billCycleTypeField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr", IsNullable = false)]
    public partial class SubscriberInfo
    {
        private SubscriberInfoSubscriber subscriberField;

        private SubscriberInfoProduct[] productField;

        private SubscriberInfoService[] serviceField;

        /// <remarks/>
        public SubscriberInfoSubscriber Subscriber
        {
            get
            {
                return subscriberField;
            }
            set
            {
                subscriberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("Product")]
        public SubscriberInfoProduct[] Product
        {
            get
            {
                return productField;
            }
            set
            {
                productField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("Service")]
        public SubscriberInfoService[] Service
        {
            get
            {
                return serviceField;
            }
            set
            {
                serviceField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr")]
    public partial class SubscriberInfoSubscriber
    {
        private long codeField;

        private byte brandIdField;

        private long registrationTimeField;

        private byte langField;

        private byte sMSLangField;

        private byte uSSDLangField;

        private byte paidModeField;

        private byte initialCreditField;

        private byte belToAreaIDField;

        private decimal mainProductIDField;

        private SubscriberInfoSubscriberSimpleProperty[] simplePropertyField;

        private long lastRechargeTimeField;

        private long lastCallTimeField;

        private long iMSIField;

        /// <remarks/>
        public long Code
        {
            get
            {
                return codeField;
            }
            set
            {
                codeField = value;
            }
        }

        /// <remarks/>
        public byte BrandId
        {
            get
            {
                return brandIdField;
            }
            set
            {
                brandIdField = value;
            }
        }

        /// <remarks/>
        public long RegistrationTime
        {
            get
            {
                return registrationTimeField;
            }
            set
            {
                registrationTimeField = value;
            }
        }

        /// <remarks/>
        public byte Lang
        {
            get
            {
                return langField;
            }
            set
            {
                langField = value;
            }
        }

        /// <remarks/>
        public byte SMSLang
        {
            get
            {
                return sMSLangField;
            }
            set
            {
                sMSLangField = value;
            }
        }

        /// <remarks/>
        public byte USSDLang
        {
            get
            {
                return uSSDLangField;
            }
            set
            {
                uSSDLangField = value;
            }
        }

        /// <remarks/>
        public byte PaidMode
        {
            get
            {
                return paidModeField;
            }
            set
            {
                paidModeField = value;
            }
        }

        /// <remarks/>
        public byte InitialCredit
        {
            get
            {
                return initialCreditField;
            }
            set
            {
                initialCreditField = value;
            }
        }

        /// <remarks/>
        public byte BelToAreaID
        {
            get
            {
                return belToAreaIDField;
            }
            set
            {
                belToAreaIDField = value;
            }
        }

        /// <remarks/>
        public decimal MainProductID
        {
            get
            {
                return mainProductIDField;
            }
            set
            {
                mainProductIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("SimpleProperty")]
        public SubscriberInfoSubscriberSimpleProperty[] SimpleProperty
        {
            get
            {
                return simplePropertyField;
            }
            set
            {
                simplePropertyField = value;
            }
        }

        /// <remarks/>
        public long LastRechargeTime
        {
            get
            {
                return lastRechargeTimeField;
            }
            set
            {
                lastRechargeTimeField = value;
            }
        }

        /// <remarks/>
        public long LastCallTime
        {
            get
            {
                return lastCallTimeField;
            }
            set
            {
                lastCallTimeField = value;
            }
        }

        /// <remarks/>
        public long IMSI
        {
            get
            {
                return iMSIField;
            }
            set
            {
                iMSIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr")]
    public partial class SubscriberInfoSubscriberSimpleProperty
    {
        private string idField;

        private string valueField;

        /// <remarks/>
        public string Id
        {
            get
            {
                return idField;
            }
            set
            {
                idField = value;
            }
        }

        /// <remarks/>
        public string Value
        {
            get
            {
                return valueField;
            }
            set
            {
                valueField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr")]
    public partial class SubscriberInfoProduct
    {
        private int idField;

        private string nameField;

        private string productOrderKeyField;

        private string effectiveDateField;

        private string expiredDateField;

        private byte statusField;

        private long curCycleStartTimeField;

        private bool curCycleStartTimeFieldSpecified;

        private long curCycleEndTimeField;

        private bool curCycleEndTimeFieldSpecified;

        private byte billStatusField;

        /// <remarks/>
        public int Id
        {
            get
            {
                return idField;
            }
            set
            {
                idField = value;
            }
        }

        /// <remarks/>
        public string Name
        {
            get
            {
                return nameField;
            }
            set
            {
                nameField = value;
            }
        }

        /// <remarks/>
        public string ProductOrderKey
        {
            get
            {
                return productOrderKeyField;
            }
            set
            {
                productOrderKeyField = value;
            }
        }

        /// <remarks/>
        public string EffectiveDate
        {
            get
            {
                return effectiveDateField;
            }
            set
            {
                effectiveDateField = value;
            }
        }

        /// <remarks/>
        public string ExpiredDate
        {
            get
            {
                return expiredDateField;
            }
            set
            {
                expiredDateField = value;
            }
        }

        /// <remarks/>
        public byte Status
        {
            get
            {
                return statusField;
            }
            set
            {
                statusField = value;
            }
        }

        /// <remarks/>
        public long CurCycleStartTime
        {
            get
            {
                return curCycleStartTimeField;
            }
            set
            {
                curCycleStartTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool CurCycleStartTimeSpecified
        {
            get
            {
                return curCycleStartTimeFieldSpecified;
            }
            set
            {
                curCycleStartTimeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public long CurCycleEndTime
        {
            get
            {
                return curCycleEndTimeField;
            }
            set
            {
                curCycleEndTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool CurCycleEndTimeSpecified
        {
            get
            {
                return curCycleEndTimeFieldSpecified;
            }
            set
            {
                curCycleEndTimeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public byte BillStatus
        {
            get
            {
                return billStatusField;
            }
            set
            {
                billStatusField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr")]
    public partial class SubscriberInfoService
    {
        private int idField;

        private int statusField;

        private long registrationTimeField;

        /// <remarks/>
        public int Id
        {
            get
            {
                return idField;
            }
            set
            {
                idField = value;
            }
        }

        /// <remarks/>
        public int Status
        {
            get
            {
                return statusField;
            }
            set
            {
                statusField = value;
            }
        }

        /// <remarks/>
        public long RegistrationTime
        {
            get
            {
                return registrationTimeField;
            }
            set
            {
                registrationTimeField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr")]
    public partial class CumulativeItemListCumulativeItem
    {
        private long cumulateBeginTimeField;

        private long cumulateEndTimeField;

        private int cumulateIDField;

        private long cumulativeAmtField;

        /// <remarks/>
        public long CumulateBeginTime
        {
            get
            {
                return cumulateBeginTimeField;
            }
            set
            {
                cumulateBeginTimeField = value;
            }
        }

        /// <remarks/>
        public long CumulateEndTime
        {
            get
            {
                return cumulateEndTimeField;
            }
            set
            {
                cumulateEndTimeField = value;
            }
        }

        /// <remarks/>
        public int CumulateID
        {
            get
            {
                return cumulateIDField;
            }
            set
            {
                cumulateIDField = value;
            }
        }

        /// <remarks/>
        public long CumulativeAmt
        {
            get
            {
                return cumulativeAmtField;
            }
            set
            {
                cumulativeAmtField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr", IsNullable = false)]
    public partial class SubAttachedInfo
    {
        private byte chgMainProductTimesField;

        private byte chgMainPackageTimesField;

        private long lastRechargeTimeField;

        private long callBeginTimeField;

        private long serviceStartField;

        private long serviceStopField;

        private byte uSSDNotifyFlagField;

        private long mSISDNField;

        private byte fraudTimesField;

        private byte homeZoneNo1Field;

        private byte homeZoneNo2Field;

        private byte homeZoneNo3Field;

        private byte homeZoneNo4Field;

        private byte homeZoneNo5Field;

        /// <remarks/>
        public byte ChgMainProductTimes
        {
            get
            {
                return chgMainProductTimesField;
            }
            set
            {
                chgMainProductTimesField = value;
            }
        }

        /// <remarks/>
        public byte ChgMainPackageTimes
        {
            get
            {
                return chgMainPackageTimesField;
            }
            set
            {
                chgMainPackageTimesField = value;
            }
        }

        /// <remarks/>
        public long LastRechargeTime
        {
            get
            {
                return lastRechargeTimeField;
            }
            set
            {
                lastRechargeTimeField = value;
            }
        }

        /// <remarks/>
        public long CallBeginTime
        {
            get
            {
                return callBeginTimeField;
            }
            set
            {
                callBeginTimeField = value;
            }
        }

        /// <remarks/>
        public long ServiceStart
        {
            get
            {
                return serviceStartField;
            }
            set
            {
                serviceStartField = value;
            }
        }

        /// <remarks/>
        public long ServiceStop
        {
            get
            {
                return serviceStopField;
            }
            set
            {
                serviceStopField = value;
            }
        }

        /// <remarks/>
        public byte USSDNotifyFlag
        {
            get
            {
                return uSSDNotifyFlagField;
            }
            set
            {
                uSSDNotifyFlagField = value;
            }
        }

        /// <remarks/>
        public long MSISDN
        {
            get
            {
                return mSISDNField;
            }
            set
            {
                mSISDNField = value;
            }
        }

        /// <remarks/>
        public byte FraudTimes
        {
            get
            {
                return fraudTimesField;
            }
            set
            {
                fraudTimesField = value;
            }
        }

        /// <remarks/>
        public byte HomeZoneNo1
        {
            get
            {
                return homeZoneNo1Field;
            }
            set
            {
                homeZoneNo1Field = value;
            }
        }

        /// <remarks/>
        public byte HomeZoneNo2
        {
            get
            {
                return homeZoneNo2Field;
            }
            set
            {
                homeZoneNo2Field = value;
            }
        }

        /// <remarks/>
        public byte HomeZoneNo3
        {
            get
            {
                return homeZoneNo3Field;
            }
            set
            {
                homeZoneNo3Field = value;
            }
        }

        /// <remarks/>
        public byte HomeZoneNo4
        {
            get
            {
                return homeZoneNo4Field;
            }
            set
            {
                homeZoneNo4Field = value;
            }
        }

        /// <remarks/>
        public byte HomeZoneNo5
        {
            get
            {
                return homeZoneNo5Field;
            }
            set
            {
                homeZoneNo5Field = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr", IsNullable = false)]
    public partial class BalanceRecordList
    {
        private BalanceRecordListBalanceRecord[] balanceRecordField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("BalanceRecord")]
        public BalanceRecordListBalanceRecord[] BalanceRecord
        {
            get
            {
                return balanceRecordField;
            }
            set
            {
                balanceRecordField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr", IsNullable = false)]
    public partial class CumulativeItemList
    {
        private CumulativeItemListCumulativeItem[] cumulativeItemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("CumulativeItem")]
        public CumulativeItemListCumulativeItem[] CumulativeItem
        {
            get
            {
                return cumulativeItemField;
            }
            set
            {
                cumulativeItemField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr")]
    public partial class BalanceRecordType
    {

        private string balanceDescField;

        private long balanceField;

        private int? minMeasureIdField;

        private bool minMeasureIdFieldSpecified;

        private int? unitTypeField;

        private bool unitTypeFieldSpecified;

        private string accountTypeField;

        private string expireTimeField;

        private long? accountCreditField;

        private bool accountCreditFieldSpecified;

        private string accountKeyField;

        private string productIDField;

        private string productOrderKeyField;

        private long initialAmountField;

        private bool initialAmountFieldSpecified;

        private long usageAmountField;

        private bool usageAmountFieldSpecified;

        /// <remarks/>
        public string BalanceDesc
        {
            get
            {
                return balanceDescField;
            }
            set
            {
                balanceDescField = value;
            }
        }

        /// <remarks/>
        public long Balance
        {
            get
            {
                return balanceField;
            }
            set
            {
                balanceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public int? MinMeasureId
        {
            get
            {
                return minMeasureIdField;
            }
            set
            {
                minMeasureIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool MinMeasureIdSpecified
        {
            get
            {
                return minMeasureIdFieldSpecified;
            }
            set
            {
                minMeasureIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public int? UnitType
        {
            get
            {
                return unitTypeField;
            }
            set
            {
                unitTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool UnitTypeSpecified
        {
            get
            {
                return unitTypeFieldSpecified;
            }
            set
            {
                unitTypeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string AccountType
        {
            get
            {
                return accountTypeField;
            }
            set
            {
                accountTypeField = value;
            }
        }

        /// <remarks/>
        public string ExpireTime
        {
            get
            {
                return expireTimeField;
            }
            set
            {
                expireTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public long? AccountCredit
        {
            get
            {
                return accountCreditField;
            }
            set
            {
                accountCreditField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool AccountCreditSpecified
        {
            get
            {
                return accountCreditFieldSpecified;
            }
            set
            {
                accountCreditFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public string AccountKey
        {
            get
            {
                return accountKeyField;
            }
            set
            {
                accountKeyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public string ProductID
        {
            get
            {
                return productIDField;
            }
            set
            {
                productIDField = value;
            }
        }

        /// <remarks/>
        public string ProductOrderKey
        {
            get
            {
                return productOrderKeyField;
            }
            set
            {
                productOrderKeyField = value;
            }
        }

        /// <remarks/>
        public long InitialAmount
        {
            get
            {
                return initialAmountField;
            }
            set
            {
                initialAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool InitialAmountSpecified
        {
            get
            {
                return initialAmountFieldSpecified;
            }
            set
            {
                initialAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        public long UsageAmount
        {
            get
            {
                return usageAmountField;
            }
            set
            {
                usageAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool UsageAmountSpecified
        {
            get
            {
                return usageAmountFieldSpecified;
            }
            set
            {
                usageAmountFieldSpecified = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr")]
    public partial class IntegrationEnquiryResultSubscriberState
    {

        private string firstActiveDateField;

        private string activeCACField;

        private string activeStopField;

        private string suspendStopField;

        private string disableStopField;

        private int? lifeCycleStateField;

        private bool lifeCycleStateFieldSpecified;

        private int? dPFlagField;

        private int? fraudStateField;

        private int? lossFlagField;

        private bool lossFlagFieldSpecified;

        private int? pOSUserStateField;

        private bool pOSUserStateFieldSpecified;

        private int? lockedFlagField;

        private string dPEndDateField;

        private int? pPSPortoutField;

        private bool pPSPortoutFieldSpecified;

        /// <remarks/>
        public string FirstActiveDate
        {
            get
            {
                return firstActiveDateField;
            }
            set
            {
                firstActiveDateField = value;
            }
        }

        /// <remarks/>
        public string ActiveCAC
        {
            get
            {
                return activeCACField;
            }
            set
            {
                activeCACField = value;
            }
        }

        /// <remarks/>
        public string ActiveStop
        {
            get
            {
                return activeStopField;
            }
            set
            {
                activeStopField = value;
            }
        }

        /// <remarks/>
        public string SuspendStop
        {
            get
            {
                return suspendStopField;
            }
            set
            {
                suspendStopField = value;
            }
        }

        /// <remarks/>
        public string DisableStop
        {
            get
            {
                return disableStopField;
            }
            set
            {
                disableStopField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public int? LifeCycleState
        {
            get
            {
                return lifeCycleStateField;
            }
            set
            {
                lifeCycleStateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool LifeCycleStateSpecified
        {
            get
            {
                return lifeCycleStateFieldSpecified;
            }
            set
            {
                lifeCycleStateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public int? DPFlag
        {
            get
            {
                return dPFlagField;
            }
            set
            {
                dPFlagField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public int? FraudState
        {
            get
            {
                return fraudStateField;
            }
            set
            {
                fraudStateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public int? LossFlag
        {
            get
            {
                return lossFlagField;
            }
            set
            {
                lossFlagField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool LossFlagSpecified
        {
            get
            {
                return lossFlagFieldSpecified;
            }
            set
            {
                lossFlagFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public int? POSUserState
        {
            get
            {
                return pOSUserStateField;
            }
            set
            {
                pOSUserStateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool POSUserStateSpecified
        {
            get
            {
                return pOSUserStateFieldSpecified;
            }
            set
            {
                pOSUserStateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public int? LockedFlag
        {
            get
            {
                return lockedFlagField;
            }
            set
            {
                lockedFlagField = value;
            }
        }

        /// <remarks/>
        public string DPEndDate
        {
            get
            {
                return dPEndDateField;
            }
            set
            {
                dPEndDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public int? PPSPortout
        {
            get
            {
                return pPSPortoutField;
            }
            set
            {
                pPSPortoutField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool PPSPortoutSpecified
        {
            get
            {
                return pPSPortoutFieldSpecified;
            }
            set
            {
                pPSPortoutFieldSpecified = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr")]
    public partial class IntegrationEnquiryResultBillingCycleDate
    {

        private string billCycleOpenDateField;

        private string billCycleEndDateField;

        private int? billCycleTypeField;

        private bool billCycleTypeFieldSpecified;

        /// <remarks/>
        public string BillCycleOpenDate
        {
            get
            {
                return billCycleOpenDateField;
            }
            set
            {
                billCycleOpenDateField = value;
            }
        }

        /// <remarks/>
        public string BillCycleEndDate
        {
            get
            {
                return billCycleEndDateField;
            }
            set
            {
                billCycleEndDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public int? BillCycleType
        {
            get
            {
                return billCycleTypeField;
            }
            set
            {
                billCycleTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool BillCycleTypeSpecified
        {
            get
            {
                return billCycleTypeFieldSpecified;
            }
            set
            {
                billCycleTypeFieldSpecified = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr")]
    public partial class IntegrationEnquiryResultSubscriberInfo
    {

        private IntegrationEnquiryResultSubscriberInfoSubscriber subscriberField;

        private IntegrationEnquiryResultSubscriberInfoProduct[] productField;

        private Service[] serviceField;

        /// <remarks/>
        public IntegrationEnquiryResultSubscriberInfoSubscriber Subscriber
        {
            get
            {
                return subscriberField;
            }
            set
            {
                subscriberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("Product")]
        public IntegrationEnquiryResultSubscriberInfoProduct[] Product
        {
            get
            {
                return productField;
            }
            set
            {
                productField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("Service")]
        public Service[] Service
        {
            get
            {
                return serviceField;
            }
            set
            {
                serviceField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr")]
    public partial class IntegrationEnquiryResultCumulativeItem
    {

        private int? cumulateIDField;

        private string cumulateBeginTimeField;

        private string cumulateEndTimeField;

        private long? cumulativeAmtField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public int? CumulateID
        {
            get
            {
                return cumulateIDField;
            }
            set
            {
                cumulateIDField = value;
            }
        }

        /// <remarks/>
        public string CumulateBeginTime
        {
            get
            {
                return cumulateBeginTimeField;
            }
            set
            {
                cumulateBeginTimeField = value;
            }
        }

        /// <remarks/>
        public string CumulateEndTime
        {
            get
            {
                return cumulateEndTimeField;
            }
            set
            {
                cumulateEndTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public long? CumulativeAmt
        {
            get
            {
                return cumulativeAmtField;
            }
            set
            {
                cumulativeAmtField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr")]
    public partial class IntegrationEnquiryResultUserGroup
    {

        private string groupIDField;

        private string groupNameField;

        /// <remarks/>
        public string GroupID
        {
            get
            {
                return groupIDField;
            }
            set
            {
                groupIDField = value;
            }
        }

        /// <remarks/>
        public string GroupName
        {
            get
            {
                return groupNameField;
            }
            set
            {
                groupNameField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr")]
    public partial class IntegrationEnquiryResultSubAttachedInfo
    {

        private int? chgMainProductTimesField;

        private bool chgMainProductTimesFieldSpecified;

        private int? chgMainPackageTimesField;

        private bool chgMainPackageTimesFieldSpecified;

        private string lastRechargeTimeField;

        private string callBeginTimeField;

        private string serviceStartField;

        private string serviceStopField;

        private string uSSDNotifyFlagField;

        private string mSISDNField;

        private int fraudTimesField;

        private bool fraudTimesFieldSpecified;

        private string homeZoneNo1Field;

        private string homeZoneNo2Field;

        private string homeZoneNo3Field;

        private string homeZoneNo4Field;

        private string homeZoneNo5Field;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public int? ChgMainProductTimes
        {
            get
            {
                return chgMainProductTimesField;
            }
            set
            {
                chgMainProductTimesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool ChgMainProductTimesSpecified
        {
            get
            {
                return chgMainProductTimesFieldSpecified;
            }
            set
            {
                chgMainProductTimesFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public int? ChgMainPackageTimes
        {
            get
            {
                return chgMainPackageTimesField;
            }
            set
            {
                chgMainPackageTimesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool ChgMainPackageTimesSpecified
        {
            get
            {
                return chgMainPackageTimesFieldSpecified;
            }
            set
            {
                chgMainPackageTimesFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string LastRechargeTime
        {
            get
            {
                return lastRechargeTimeField;
            }
            set
            {
                lastRechargeTimeField = value;
            }
        }

        /// <remarks/>
        public string CallBeginTime
        {
            get
            {
                return callBeginTimeField;
            }
            set
            {
                callBeginTimeField = value;
            }
        }

        /// <remarks/>
        public string ServiceStart
        {
            get
            {
                return serviceStartField;
            }
            set
            {
                serviceStartField = value;
            }
        }

        /// <remarks/>
        public string ServiceStop
        {
            get
            {
                return serviceStopField;
            }
            set
            {
                serviceStopField = value;
            }
        }

        /// <remarks/>
        public string USSDNotifyFlag
        {
            get
            {
                return uSSDNotifyFlagField;
            }
            set
            {
                uSSDNotifyFlagField = value;
            }
        }

        /// <remarks/>
        public string MSISDN
        {
            get
            {
                return mSISDNField;
            }
            set
            {
                mSISDNField = value;
            }
        }

        /// <remarks/>
        public int FraudTimes
        {
            get
            {
                return fraudTimesField;
            }
            set
            {
                fraudTimesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool FraudTimesSpecified
        {
            get
            {
                return fraudTimesFieldSpecified;
            }
            set
            {
                fraudTimesFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string HomeZoneNo1
        {
            get
            {
                return homeZoneNo1Field;
            }
            set
            {
                homeZoneNo1Field = value;
            }
        }

        /// <remarks/>
        public string HomeZoneNo2
        {
            get
            {
                return homeZoneNo2Field;
            }
            set
            {
                homeZoneNo2Field = value;
            }
        }

        /// <remarks/>
        public string HomeZoneNo3
        {
            get
            {
                return homeZoneNo3Field;
            }
            set
            {
                homeZoneNo3Field = value;
            }
        }

        /// <remarks/>
        public string HomeZoneNo4
        {
            get
            {
                return homeZoneNo4Field;
            }
            set
            {
                homeZoneNo4Field = value;
            }
        }

        /// <remarks/>
        public string HomeZoneNo5
        {
            get
            {
                return homeZoneNo5Field;
            }
            set
            {
                homeZoneNo5Field = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr")]
    public partial class IntegrationEnquiryResultSubscriberInfoSubscriber : Subscriber
    {
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr")]
    public partial class IntegrationEnquiryResultSubscriberInfoProduct : Product
    {

        private string productOrderKeyField;

        private string effectiveDateField;

        private string expiredDateField;

        private int? statusField;

        private string curCycleStartTimeField;

        private string curCycleEndTimeField;

        private int billStatusField;

        private bool billStatusFieldSpecified;

        private BindSubscriberNo[] bindSubscriberNoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public string ProductOrderKey
        {
            get
            {
                return productOrderKeyField;
            }
            set
            {
                productOrderKeyField = value;
            }
        }

        /// <remarks/>
        public string EffectiveDate
        {
            get
            {
                return effectiveDateField;
            }
            set
            {
                effectiveDateField = value;
            }
        }

        /// <remarks/>
        public string ExpiredDate
        {
            get
            {
                return expiredDateField;
            }
            set
            {
                expiredDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public int? Status
        {
            get
            {
                return statusField;
            }
            set
            {
                statusField = value;
            }
        }

        /// <remarks/>
        public string CurCycleStartTime
        {
            get
            {
                return curCycleStartTimeField;
            }
            set
            {
                curCycleStartTimeField = value;
            }
        }

        /// <remarks/>
        public string CurCycleEndTime
        {
            get
            {
                return curCycleEndTimeField;
            }
            set
            {
                curCycleEndTimeField = value;
            }
        }

        /// <remarks/>
        public int BillStatus
        {
            get
            {
                return billStatusField;
            }
            set
            {
                billStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool BillStatusSpecified
        {
            get
            {
                return billStatusFieldSpecified;
            }
            set
            {
                billStatusFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("BindSubscriberNo")]
        public BindSubscriberNo[] BindSubscriberNo
        {
            get
            {
                return bindSubscriberNoField;
            }
            set
            {
                bindSubscriberNoField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr")]
    public partial class BindSubscriberNo
    {

        private string subscriberNoField;

        private int subscriberNoTypeField;

        private string iMSIField;

        private string applyTimeField;

        private string expireTimeField;

        /// <remarks/>
        public string SubscriberNo
        {
            get
            {
                return subscriberNoField;
            }
            set
            {
                subscriberNoField = value;
            }
        }

        /// <remarks/>
        public int SubscriberNoType
        {
            get
            {
                return subscriberNoTypeField;
            }
            set
            {
                subscriberNoTypeField = value;
            }
        }

        /// <remarks/>
        public string IMSI
        {
            get
            {
                return iMSIField;
            }
            set
            {
                iMSIField = value;
            }
        }

        /// <remarks/>
        public string ApplyTime
        {
            get
            {
                return applyTimeField;
            }
            set
            {
                applyTimeField = value;
            }
        }

        /// <remarks/>
        public string ExpireTime
        {
            get
            {
                return expireTimeField;
            }
            set
            {
                expireTimeField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr")]
    public partial class Product
    {

        private string idField;

        private string nameField;

        private SimpleProperty[] simplePropertyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public string Id
        {
            get
            {
                return idField;
            }
            set
            {
                idField = value;
            }
        }

        /// <remarks/>
        public string Name
        {
            get
            {
                return nameField;
            }
            set
            {
                nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("SimpleProperty")]
        public SimpleProperty[] SimpleProperty
        {
            get
            {
                return simplePropertyField;
            }
            set
            {
                simplePropertyField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr")]
    public partial class SimpleProperty
    {

        private string idField;

        private string valueField;

        private int typeField;

        private bool typeFieldSpecified;

        /// <remarks/>
        public string Id
        {
            get
            {
                return idField;
            }
            set
            {
                idField = value;
            }
        }

        /// <remarks/>
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        /// <remarks/>
        public int Type
        {
            get
            {
                return typeField;
            }
            set
            {
                typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TypeSpecified
        {
            get
            {
                return typeFieldSpecified;
            }
            set
            {
                typeFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr")]
    public partial class Service
    {

        private string idField;

        private string statusField;

        private string registrationTimeField;

        private SimpleProperty[] simplePropertyField;

        /// <remarks/>
        public string Id
        {
            get
            {
                return idField;
            }
            set
            {
                idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public string Status
        {
            get
            {
                return statusField;
            }
            set
            {
                statusField = value;
            }
        }

        /// <remarks/>
        public string RegistrationTime
        {
            get
            {
                return registrationTimeField;
            }
            set
            {
                registrationTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("SimpleProperty")]
        public SimpleProperty[] SimpleProperty
        {
            get
            {
                return simplePropertyField;
            }
            set
            {
                simplePropertyField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr")]
    public partial class Subscriber : SubscriberBasic
    {

        private string iMSIField;

        /// <remarks/>
        public string IMSI
        {
            get
            {
                return iMSIField;
            }
            set
            {
                iMSIField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlInclude(typeof(Subscriber))]
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr")]
    public partial class SubscriberBasic
    {

        private string codeField;

        private string brandIdField;

        private string registrationTimeField;

        private string langField;

        private string sMSLangField;

        private string uSSDLangField;

        private Paymode? paidModeField;

        private int? initialCreditField;

        private bool initialCreditFieldSpecified;

        private string belToAreaIDField;

        private string mainProductIDField;

        private SimpleProperty[] simplePropertyField;

        private string lastRechargeTimeField;

        private string lastCallTimeField;

        private int sMSAmountField;

        private bool sMSAmountFieldSpecified;

        /// <remarks/>
        public string Code
        {
            get
            {
                return codeField;
            }
            set
            {
                codeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public string BrandId
        {
            get
            {
                return brandIdField;
            }
            set
            {
                brandIdField = value;
            }
        }

        /// <remarks/>
        public string RegistrationTime
        {
            get
            {
                return registrationTimeField;
            }
            set
            {
                registrationTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public string Lang
        {
            get
            {
                return langField;
            }
            set
            {
                langField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public string SMSLang
        {
            get
            {
                return sMSLangField;
            }
            set
            {
                sMSLangField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public string USSDLang
        {
            get
            {
                return uSSDLangField;
            }
            set
            {
                uSSDLangField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public Paymode? PaidMode
        {
            get
            {
                return paidModeField;
            }
            set
            {
                paidModeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public int? InitialCredit
        {
            get
            {
                return initialCreditField;
            }
            set
            {
                initialCreditField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool InitialCreditSpecified
        {
            get
            {
                return initialCreditFieldSpecified;
            }
            set
            {
                initialCreditFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string BelToAreaID
        {
            get
            {
                return belToAreaIDField;
            }
            set
            {
                belToAreaIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public string MainProductID
        {
            get
            {
                return mainProductIDField;
            }
            set
            {
                mainProductIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("SimpleProperty")]
        public SimpleProperty[] SimpleProperty
        {
            get
            {
                return simplePropertyField;
            }
            set
            {
                simplePropertyField = value;
            }
        }

        /// <remarks/>
        public string LastRechargeTime
        {
            get
            {
                return lastRechargeTimeField;
            }
            set
            {
                lastRechargeTimeField = value;
            }
        }

        /// <remarks/>
        public string LastCallTime
        {
            get
            {
                return lastCallTimeField;
            }
            set
            {
                lastCallTimeField = value;
            }
        }

        /// <remarks/>
        public int SMSAmount
        {
            get
            {
                return sMSAmountField;
            }
            set
            {
                sMSAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool SMSAmountSpecified
        {
            get
            {
                return sMSAmountFieldSpecified;
            }
            set
            {
                sMSAmountFieldSpecified = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bme/cbsinterface/cbs/businessmgr")]
    public enum Paymode
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("2")]
        Item2,
    }

}