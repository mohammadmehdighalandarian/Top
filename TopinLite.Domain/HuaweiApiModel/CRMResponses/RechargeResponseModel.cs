namespace TopinLite.Domain.HuaweiApiModel.CRMResponses.Recharge
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public partial class EnvelopeRechargeResponse
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
        private RechargeResultMsg rechargeResultMsgField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bme/cbsinterface/arservices")]
        public RechargeResultMsg RechargeResultMsg
        {
            get
            {
                return rechargeResultMsgField;
            }
            set
            {
                rechargeResultMsgField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/arservices")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://www.huawei.com/bme/cbsinterface/arservices", IsNullable = false)]
    public partial class RechargeResultMsg
    {
        private ResultHeader resultHeaderField;

        private RechargeResult rechargeResultField;

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
        public RechargeResult RechargeResult
        {
            get
            {
                return rechargeResultField;
            }
            set
            {
                rechargeResultField = value;
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
        private decimal versionField;

        private decimal resultCodeField;

        private ushort msgLanguageCodeField;

        private string resultDescField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bme/cbsinterface/cbscommon")]
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
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bme/cbsinterface/cbscommon")]
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
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bme/cbsinterface/cbscommon")]
        public ushort MsgLanguageCode
        {
            get
            {
                return msgLanguageCodeField;
            }
            set
            {
                msgLanguageCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bme/cbsinterface/cbscommon")]
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
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bme/cbsinterface/arservices")]
    public partial class RechargeResult
    {

        private string rechargeSerialNoField;

        private BalanceChgInfo[] balanceChgInfoField;

        private LoanChgInfo loanChgInfoField;

        private RechargeResultRechargeBonus rechargeBonusField;

        private RechargeResultLifeCycleChgInfo lifeCycleChgInfoField;

        private CreditChgInfo[] creditChgInfoField;

        /// <remarks/>
        public string RechargeSerialNo
        {
            get
            {
                return rechargeSerialNoField;
            }
            set
            {
                rechargeSerialNoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("BalanceChgInfo")]
        public BalanceChgInfo[] BalanceChgInfo
        {
            get
            {
                return balanceChgInfoField;
            }
            set
            {
                balanceChgInfoField = value;
            }
        }

        /// <remarks/>
        public LoanChgInfo LoanChgInfo
        {
            get
            {
                return loanChgInfoField;
            }
            set
            {
                loanChgInfoField = value;
            }
        }

        /// <remarks/>
        public RechargeResultRechargeBonus RechargeBonus
        {
            get
            {
                return rechargeBonusField;
            }
            set
            {
                rechargeBonusField = value;
            }
        }

        /// <remarks/>
        public RechargeResultLifeCycleChgInfo LifeCycleChgInfo
        {
            get
            {
                return lifeCycleChgInfoField;
            }
            set
            {
                lifeCycleChgInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("CreditChgInfo")]
        public CreditChgInfo[] CreditChgInfo
        {
            get
            {
                return creditChgInfoField;
            }
            set
            {
                creditChgInfoField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/arservices")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://www.huawei.com/bme/cbsinterface/arservices", IsNullable = false)]
    public partial class BalanceChgInfo
    {
        private string balanceTypeField;

        private long balanceIDField;

        private bool balanceIDFieldSpecified;

        private string balanceTypeNameField;

        private long oldBalanceAmtField;

        private long newBalanceAmtField;

        private ushort currencyIDField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://cbs.huawei.com/ar/wsservice/arcommon")]
        public string BalanceType
        {
            get
            {
                return balanceTypeField;
            }
            set
            {
                balanceTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://cbs.huawei.com/ar/wsservice/arcommon")]
        public long BalanceID
        {
            get
            {
                return balanceIDField;
            }
            set
            {
                balanceIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool BalanceIDSpecified
        {
            get
            {
                return balanceIDFieldSpecified;
            }
            set
            {
                balanceIDFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://cbs.huawei.com/ar/wsservice/arcommon")]
        public string BalanceTypeName
        {
            get
            {
                return balanceTypeNameField;
            }
            set
            {
                balanceTypeNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://cbs.huawei.com/ar/wsservice/arcommon")]
        public long OldBalanceAmt
        {
            get
            {
                return oldBalanceAmtField;
            }
            set
            {
                oldBalanceAmtField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://cbs.huawei.com/ar/wsservice/arcommon")]
        public long NewBalanceAmt
        {
            get
            {
                return newBalanceAmtField;
            }
            set
            {
                newBalanceAmtField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://cbs.huawei.com/ar/wsservice/arcommon")]
        public ushort CurrencyID
        {
            get
            {
                return currencyIDField;
            }
            set
            {
                currencyIDField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/arservices")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://www.huawei.com/bme/cbsinterface/arservices", IsNullable = false)]
    public partial class LifeCycleChgInfo
    {
        private LifeCycleChgInfoOldLifeCycleStatus[] oldLifeCycleStatusField;

        private LifeCycleChgInfoNewLifeCycleStatus[] newLifeCycleStatusField;

        private byte addValidityField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("OldLifeCycleStatus")]
        public LifeCycleChgInfoOldLifeCycleStatus[] OldLifeCycleStatus
        {
            get
            {
                return oldLifeCycleStatusField;
            }
            set
            {
                oldLifeCycleStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("NewLifeCycleStatus")]
        public LifeCycleChgInfoNewLifeCycleStatus[] NewLifeCycleStatus
        {
            get
            {
                return newLifeCycleStatusField;
            }
            set
            {
                newLifeCycleStatusField = value;
            }
        }

        /// <remarks/>
        public byte AddValidity
        {
            get
            {
                return addValidityField;
            }
            set
            {
                addValidityField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/arservices")]
    public partial class LifeCycleChgInfoOldLifeCycleStatus
    {
        private string statusNameField;

        private long statusExpireTimeField;

        private byte statusIndexField;

        /// <remarks/>
        public string StatusName
        {
            get
            {
                return statusNameField;
            }
            set
            {
                statusNameField = value;
            }
        }

        /// <remarks/>
        public long StatusExpireTime
        {
            get
            {
                return statusExpireTimeField;
            }
            set
            {
                statusExpireTimeField = value;
            }
        }

        /// <remarks/>
        public byte StatusIndex
        {
            get
            {
                return statusIndexField;
            }
            set
            {
                statusIndexField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/arservices")]
    public partial class LifeCycleChgInfoNewLifeCycleStatus
    {
        private string statusNameField;

        private long statusExpireTimeField;

        private byte statusIndexField;

        /// <remarks/>
        public string StatusName
        {
            get
            {
                return statusNameField;
            }
            set
            {
                statusNameField = value;
            }
        }

        /// <remarks/>
        public long StatusExpireTime
        {
            get
            {
                return statusExpireTimeField;
            }
            set
            {
                statusExpireTimeField = value;
            }
        }

        /// <remarks/>
        public byte StatusIndex
        {
            get
            {
                return statusIndexField;
            }
            set
            {
                statusIndexField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://cbs.huawei.com/ar/wsservice/arcommon")]
    public partial class LoanChgInfo
    {

        private long oldLoanAmtField;

        private long newLoanAmtField;

        private long loanPaymentAmtField;

        private long loanInterestAmtField;

        private string currencyIDField;

        /// <remarks/>
        public long OldLoanAmt
        {
            get
            {
                return oldLoanAmtField;
            }
            set
            {
                oldLoanAmtField = value;
            }
        }

        /// <remarks/>
        public long NewLoanAmt
        {
            get
            {
                return newLoanAmtField;
            }
            set
            {
                newLoanAmtField = value;
            }
        }

        /// <remarks/>
        public long LoanPaymentAmt
        {
            get
            {
                return loanPaymentAmtField;
            }
            set
            {
                loanPaymentAmtField = value;
            }
        }

        /// <remarks/>
        public long LoanInterestAmt
        {
            get
            {
                return loanInterestAmtField;
            }
            set
            {
                loanInterestAmtField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "integer")]
        public string CurrencyID
        {
            get
            {
                return currencyIDField;
            }
            set
            {
                currencyIDField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/arservices")]
    public partial class RechargeResultRechargeBonus
    {

        private RechargeResultRechargeBonusFreeUnitItemList[] freeUnitItemListField;

        private RechargeResultRechargeBonusBalanceList[] balanceListField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("FreeUnitItemList")]
        public RechargeResultRechargeBonusFreeUnitItemList[] FreeUnitItemList
        {
            get
            {
                return freeUnitItemListField;
            }
            set
            {
                freeUnitItemListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("BalanceList")]
        public RechargeResultRechargeBonusBalanceList[] BalanceList
        {
            get
            {
                return balanceListField;
            }
            set
            {
                balanceListField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/arservices")]
    public partial class RechargeResultLifeCycleChgInfo
    {

        private RechargeResultLifeCycleChgInfoOldLifeCycleStatus[] oldLifeCycleStatusField;

        private RechargeResultLifeCycleChgInfoNewLifeCycleStatus[] newLifeCycleStatusField;

        private string addValidityField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("OldLifeCycleStatus")]
        public RechargeResultLifeCycleChgInfoOldLifeCycleStatus[] OldLifeCycleStatus
        {
            get
            {
                return oldLifeCycleStatusField;
            }
            set
            {
                oldLifeCycleStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("NewLifeCycleStatus")]
        public RechargeResultLifeCycleChgInfoNewLifeCycleStatus[] NewLifeCycleStatus
        {
            get
            {
                return newLifeCycleStatusField;
            }
            set
            {
                newLifeCycleStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "integer")]
        public string AddValidity
        {
            get
            {
                return addValidityField;
            }
            set
            {
                addValidityField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://cbs.huawei.com/ar/wsservice/arcommon")]
    public partial class CreditChgInfo
    {

        private long creditLimitIDField;

        private bool creditLimitIDFieldSpecified;

        private string creditLimitTypeField;

        private string creditLimitTypeNameField;

        private long oldLeftCreditAmtField;

        private long newLeftCreditAmtField;

        private string measureUnitField;

        /// <remarks/>
        public long CreditLimitID
        {
            get
            {
                return creditLimitIDField;
            }
            set
            {
                creditLimitIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool CreditLimitIDSpecified
        {
            get
            {
                return creditLimitIDFieldSpecified;
            }
            set
            {
                creditLimitIDFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string CreditLimitType
        {
            get
            {
                return creditLimitTypeField;
            }
            set
            {
                creditLimitTypeField = value;
            }
        }

        /// <remarks/>
        public string CreditLimitTypeName
        {
            get
            {
                return creditLimitTypeNameField;
            }
            set
            {
                creditLimitTypeNameField = value;
            }
        }

        /// <remarks/>
        public long OldLeftCreditAmt
        {
            get
            {
                return oldLeftCreditAmtField;
            }
            set
            {
                oldLeftCreditAmtField = value;
            }
        }

        /// <remarks/>
        public long NewLeftCreditAmt
        {
            get
            {
                return newLeftCreditAmtField;
            }
            set
            {
                newLeftCreditAmtField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "integer")]
        public string MeasureUnit
        {
            get
            {
                return measureUnitField;
            }
            set
            {
                measureUnitField = value;
            }
        }
    }

        /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="http://www.huawei.com/bme/cbsinterface/arservices")]
    public partial class RechargeResultLifeCycleChgInfoOldLifeCycleStatus {
        
        private string statusNameField;
        
        private string statusExpireTimeField;
        
        private string statusIndexField;
        
        /// <remarks/>
        public string StatusName {
            get {
                return statusNameField;
            }
            set {
                statusNameField = value;
            }
        }
        
        /// <remarks/>
        public string StatusExpireTime {
            get {
                return statusExpireTimeField;
            }
            set {
                statusExpireTimeField = value;
            }
        }
        
        /// <remarks/>
        public string StatusIndex {
            get {
                return statusIndexField;
            }
            set {
                statusIndexField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/arservices")]
    public partial class RechargeResultLifeCycleChgInfoNewLifeCycleStatus
    {

        private string statusNameField;

        private string statusExpireTimeField;

        private string statusIndexField;

        /// <remarks/>
        public string StatusName
        {
            get
            {
                return statusNameField;
            }
            set
            {
                statusNameField = value;
            }
        }

        /// <remarks/>
        public string StatusExpireTime
        {
            get
            {
                return statusExpireTimeField;
            }
            set
            {
                statusExpireTimeField = value;
            }
        }

        /// <remarks/>
        public string StatusIndex
        {
            get
            {
                return statusIndexField;
            }
            set
            {
                statusIndexField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/arservices")]
    public partial class RechargeResultRechargeBonusFreeUnitItemList
    {

        private string freeUnitIDField;

        private string freeUnitTypeField;

        private string freeUnitTypeNameField;

        private string measureUnitField;

        private string measureUnitNameField;

        private long? bonusAmtField;

        private string effectiveTimeField;

        private string expireTimeField;

        /// <remarks/>
        public string FreeUnitID
        {
            get
            {
                return freeUnitIDField;
            }
            set
            {
                freeUnitIDField = value;
            }
        }

        /// <remarks/>
        public string FreeUnitType
        {
            get
            {
                return freeUnitTypeField;
            }
            set
            {
                freeUnitTypeField = value;
            }
        }

        /// <remarks/>
        public string FreeUnitTypeName
        {
            get
            {
                return freeUnitTypeNameField;
            }
            set
            {
                freeUnitTypeNameField = value;
            }
        }

        /// <remarks/>
        public string MeasureUnit
        {
            get
            {
                return measureUnitField;
            }
            set
            {
                measureUnitField = value;
            }
        }

        /// <remarks/>
        public string MeasureUnitName
        {
            get
            {
                return measureUnitNameField;
            }
            set
            {
                measureUnitNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public long? BonusAmt
        {
            get
            {
                return bonusAmtField;
            }
            set
            {
                bonusAmtField = value;
            }
        }

        /// <remarks/>
        public string EffectiveTime
        {
            get
            {
                return effectiveTimeField;
            }
            set
            {
                effectiveTimeField = value;
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
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/arservices")]
    public partial class RechargeResultRechargeBonusBalanceList
    {

        private string balanceTypeField;

        private string balanceIDField;

        private string balanceTypeNameField;

        private long bonusAmtField;

        private string currencyIDField;

        private string effectiveTimeField;

        private string expireTimeField;

        /// <remarks/>
        public string BalanceType
        {
            get
            {
                return balanceTypeField;
            }
            set
            {
                balanceTypeField = value;
            }
        }

        /// <remarks/>
        public string BalanceID
        {
            get
            {
                return balanceIDField;
            }
            set
            {
                balanceIDField = value;
            }
        }

        /// <remarks/>
        public string BalanceTypeName
        {
            get
            {
                return balanceTypeNameField;
            }
            set
            {
                balanceTypeNameField = value;
            }
        }

        /// <remarks/>
        public long BonusAmt
        {
            get
            {
                return bonusAmtField;
            }
            set
            {
                bonusAmtField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "integer")]
        public string CurrencyID
        {
            get
            {
                return currencyIDField;
            }
            set
            {
                currencyIDField = value;
            }
        }

        /// <remarks/>
        public string EffectiveTime
        {
            get
            {
                return effectiveTimeField;
            }
            set
            {
                effectiveTimeField = value;
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



}