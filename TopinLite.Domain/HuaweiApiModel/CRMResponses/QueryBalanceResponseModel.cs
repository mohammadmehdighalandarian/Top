

namespace TopinLite.Domain.HuaweiApiModel.CRMResponses.QueryBalance
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>

    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public partial class EnvelopeQueryBalanceResponse
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
        private QueryBalanceResultMsg queryBalanceResultMsgField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bme/cbsinterface/arservices")]
        public QueryBalanceResultMsg QueryBalanceResultMsg
        {
            get
            {
                return queryBalanceResultMsgField;
            }
            set
            {
                queryBalanceResultMsgField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/arservices")]
    public partial class QueryBalanceResultMsg
    {

        private ResultHeader resultHeaderField;

        private QueryBalanceResultAcctList[] queryBalanceResultField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
        [System.Xml.Serialization.XmlArray(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItem("AcctList", IsNullable = false)]
        public QueryBalanceResultAcctList[] QueryBalanceResult
        {
            get
            {
                return queryBalanceResultField;
            }
            set
            {
                queryBalanceResultField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/arservices")]
    public partial class QueryBalanceResultAcctList
    {

        private string acctKeyField;

        private AcctBalance[] balanceResultField;

        private QueryBalanceResultAcctListOutStandingList[] outStandingListField;

        private QueryBalanceResultAcctListAccountCredit[] accountCreditField;

        /// <remarks/>
        public string AcctKey
        {
            get
            {
                return acctKeyField;
            }
            set
            {
                acctKeyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("BalanceResult")]
        public AcctBalance[] BalanceResult
        {
            get
            {
                return balanceResultField;
            }
            set
            {
                balanceResultField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("OutStandingList")]
        public QueryBalanceResultAcctListOutStandingList[] OutStandingList
        {
            get
            {
                return outStandingListField;
            }
            set
            {
                outStandingListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("AccountCredit")]
        public QueryBalanceResultAcctListAccountCredit[] AccountCredit
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
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://cbs.huawei.com/ar/wsservice/arcommon")]
    public partial class AcctBalance
    {

        private string balanceTypeField;

        private string balanceTypeNameField;

        private long totalAmountField;

        private string depositFlagField;

        private string refundFlagField;

        private string currencyIDField;

        private AcctBalanceBalanceDetail[] balanceDetailField;

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
        public long TotalAmount
        {
            get
            {
                return totalAmountField;
            }
            set
            {
                totalAmountField = value;
            }
        }

        /// <remarks/>
        public string DepositFlag
        {
            get
            {
                return depositFlagField;
            }
            set
            {
                depositFlagField = value;
            }
        }

        /// <remarks/>
        public string RefundFlag
        {
            get
            {
                return refundFlagField;
            }
            set
            {
                refundFlagField = value;
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
        [System.Xml.Serialization.XmlElement("BalanceDetail")]
        public AcctBalanceBalanceDetail[] BalanceDetail
        {
            get
            {
                return balanceDetailField;
            }
            set
            {
                balanceDetailField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/arservices")]
    public partial class QueryBalanceResultAcctListOutStandingList
    {

        private string billCycleIDField;

        private string billCycleBeginTimeField;

        private string billCycleEndTimeField;

        private string dueDateField;

        private QueryBalanceResultAcctListOutStandingListOutStandingDetail[] outStandingDetailField;

        /// <remarks/>
        public string BillCycleID
        {
            get
            {
                return billCycleIDField;
            }
            set
            {
                billCycleIDField = value;
            }
        }

        /// <remarks/>
        public string BillCycleBeginTime
        {
            get
            {
                return billCycleBeginTimeField;
            }
            set
            {
                billCycleBeginTimeField = value;
            }
        }

        /// <remarks/>
        public string BillCycleEndTime
        {
            get
            {
                return billCycleEndTimeField;
            }
            set
            {
                billCycleEndTimeField = value;
            }
        }

        /// <remarks/>
        public string DueDate
        {
            get
            {
                return dueDateField;
            }
            set
            {
                dueDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("OutStandingDetail")]
        public QueryBalanceResultAcctListOutStandingListOutStandingDetail[] OutStandingDetail
        {
            get
            {
                return outStandingDetailField;
            }
            set
            {
                outStandingDetailField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/arservices")]
    public partial class QueryBalanceResultAcctListAccountCredit
    {

        private string creditLimitTypeField;

        private string creditLimitTypeNameField;

        private long totalCreditAmountField;

        private long totalUsageAmountField;

        private long totalRemainAmountField;

        private string currencyIDField;

        private QueryBalanceResultAcctListAccountCreditCreditAmountInfo[] creditAmountInfoField;

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
        public long TotalCreditAmount
        {
            get
            {
                return totalCreditAmountField;
            }
            set
            {
                totalCreditAmountField = value;
            }
        }

        /// <remarks/>
        public long TotalUsageAmount
        {
            get
            {
                return totalUsageAmountField;
            }
            set
            {
                totalUsageAmountField = value;
            }
        }

        /// <remarks/>
        public long TotalRemainAmount
        {
            get
            {
                return totalRemainAmountField;
            }
            set
            {
                totalRemainAmountField = value;
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
        [System.Xml.Serialization.XmlElement("CreditAmountInfo")]
        public QueryBalanceResultAcctListAccountCreditCreditAmountInfo[] CreditAmountInfo
        {
            get
            {
                return creditAmountInfoField;
            }
            set
            {
                creditAmountInfoField = value;
            }
        }
       

    }








    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://cbs.huawei.com/ar/wsservice/arcommon")]
    public partial class AcctBalanceBalanceDetail
    {

        private long balanceInstanceIDField;

        private long amountField;

        private long initialAmountField;

        private long usedAmountField;

        private bool usedAmountFieldSpecified;

        private string effectiveTimeField;

        private string expireTimeField;

        /// <remarks/>
        public long BalanceInstanceID
        {
            get
            {
                return balanceInstanceIDField;
            }
            set
            {
                balanceInstanceIDField = value;
            }
        }

        /// <remarks/>
        public long Amount
        {
            get
            {
                return amountField;
            }
            set
            {
                amountField = value;
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
        public long UsedAmount
        {
            get
            {
                return usedAmountField;
            }
            set
            {
                usedAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool UsedAmountSpecified
        {
            get
            {
                return usedAmountFieldSpecified;
            }
            set
            {
                usedAmountFieldSpecified = value;
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
    public partial class QueryBalanceResultAcctListOutStandingListOutStandingDetail
    {

        private long outStandingAmountField;

        private string currencyIDField;

        /// <remarks/>
        public long OutStandingAmount
        {
            get
            {
                return outStandingAmountField;
            }
            set
            {
                outStandingAmountField = value;
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
    public partial class QueryBalanceResultAcctListAccountCreditCreditAmountInfo
    {

        private long creditInstIDField;

        private string limitClassField;

        private long amountField;

        private string effectiveTimeField;

        private string expireTimeField;

        /// <remarks/>
        public long CreditInstID
        {
            get
            {
                return creditInstIDField;
            }
            set
            {
                creditInstIDField = value;
            }
        }

        /// <remarks/>
        public string LimitClass
        {
            get
            {
                return limitClassField;
            }
            set
            {
                limitClassField = value;
            }
        }

        /// <remarks/>
        public long Amount
        {
            get
            {
                return amountField;
            }
            set
            {
                amountField = value;
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
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bme/cbsinterface/cbscommon")]
    public partial class ResultHeader
    {

        private string versionField;

        private string resultCodeField;

        private string msgLanguageCodeField;

        private string resultDescField;

        private ResultHeaderAdditionalProperty[] additionalPropertyField;

        /// <remarks/>
        public string Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }

        /// <remarks/>
        public string ResultCode
        {
            get
            {
                return this.resultCodeField;
            }
            set
            {
                this.resultCodeField = value;
            }
        }

        /// <remarks/>
        public string MsgLanguageCode
        {
            get
            {
                return this.msgLanguageCodeField;
            }
            set
            {
                this.msgLanguageCodeField = value;
            }
        }

        /// <remarks/>
        public string ResultDesc
        {
            get
            {
                return this.resultDescField;
            }
            set
            {
                this.resultDescField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("AdditionalProperty")]
        public ResultHeaderAdditionalProperty[] AdditionalProperty
        {
            get
            {
                return this.additionalPropertyField;
            }
            set
            {
                this.additionalPropertyField = value;
            }
        }
    }



    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bme/cbsinterface/cbscommon")]
    public partial class ResultHeaderAdditionalProperty
    {

        private string codeField;

        private string valueField;

        /// <remarks/>
        public string Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
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
    }







}