namespace TopinLite.Domain.HuaweiApiModel.CRMResponses.QuerySubscriberCZ2
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public partial class EnvelopeQuerySubscriberCZ2Response
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
        private querySubscriberCZ2RspMsg querySubscriberCZ2RspMsgField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bes/crminterface/cmservices")]
        public querySubscriberCZ2RspMsg querySubscriberCZ2RspMsg
        {
            get
            {
                return querySubscriberCZ2RspMsgField;
            }
            set
            {
                querySubscriberCZ2RspMsgField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bes/crminterface/cmservices")]
    public partial class querySubscriberCZ2RspMsg
    {

        private ResultHeader resultHeaderField;

        private querySubscriberCZ2Response querySubscriberCZ2ResponseField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ResultHeader resultHeader
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
        [System.Xml.Serialization.XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public querySubscriberCZ2Response querySubscriberCZ2Response
        {
            get
            {
                return querySubscriberCZ2ResponseField;
            }
            set
            {
                querySubscriberCZ2ResponseField = value;
            }
        }
    }





    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/crmheader")]
    public partial class ResultHeader
    {

        private string versionField;

        private string resultCodeField;

        private string msgLanguageCodeField;

        private string resultDescField;

        private ResultHeaderAdditionalProperty[] additionalPropertyField;

        /// <remarks/>
        public string version
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
        public string resultCode
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
        public string msgLanguageCode
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
        public string resultDesc
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
        [System.Xml.Serialization.XmlElement("additionalProperty")]
        public ResultHeaderAdditionalProperty[] additionalProperty
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
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bes/crminterface/crmheader")]
    public partial class ResultHeaderAdditionalProperty
    {

        private string codeField;

        private string valueField;

        /// <remarks/>
        public string code
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
        public string value
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

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmservices")]
    public partial class querySubscriberCZ2Response
    {

        private Subscriber[] subscriberInfoField;

        private SimpleProperty[] additionalPropertyField;

        private Page pageQueryField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("subscriberInfo")]
        public Subscriber[] subscriberInfo
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
        [System.Xml.Serialization.XmlElement("AdditionalProperty")]
        public SimpleProperty[] AdditionalProperty
        {
            get
            {
                return additionalPropertyField;
            }
            set
            {
                additionalPropertyField = value;
            }
        }

        /// <remarks/>
        public Page pageQuery
        {
            get
            {
                return pageQueryField;
            }
            set
            {
                pageQueryField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class Subscriber
    {

        private SubscriberBasicType subInfoField;

        private OfferingInstDetailInfo[] primaryOfferingListField;

        private OfferingInstDetailInfo[] supplementaryOfferingListField;

        /// <remarks/>
        public SubscriberBasicType subInfo
        {
            get
            {
                return subInfoField;
            }
            set
            {
                subInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("primaryOfferingList")]
        public OfferingInstDetailInfo[] primaryOfferingList
        {
            get
            {
                return primaryOfferingListField;
            }
            set
            {
                primaryOfferingListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("supplementaryOfferingList")]
        public OfferingInstDetailInfo[] supplementaryOfferingList
        {
            get
            {
                return supplementaryOfferingListField;
            }
            set
            {
                supplementaryOfferingListField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/common")]
    public partial class SimpleProperty
    {

        private string codeField;

        private string valueField;

        /// <remarks/>
        public string code
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
        public string value
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

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/common")]
    public partial class Page
    {

        private int pageSizeField;

        private bool pageSizeFieldSpecified;

        private int startNumField;

        private bool startNumFieldSpecified;

        private int totalNumField;

        private bool totalNumFieldSpecified;

        /// <remarks/>
        public int pageSize
        {
            get
            {
                return pageSizeField;
            }
            set
            {
                pageSizeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool pageSizeSpecified
        {
            get
            {
                return pageSizeFieldSpecified;
            }
            set
            {
                pageSizeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public int startNum
        {
            get
            {
                return startNumField;
            }
            set
            {
                startNumField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool startNumSpecified
        {
            get
            {
                return startNumFieldSpecified;
            }
            set
            {
                startNumFieldSpecified = value;
            }
        }

        /// <remarks/>
        public int totalNum
        {
            get
            {
                return totalNumField;
            }
            set
            {
                totalNumField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool totalNumSpecified
        {
            get
            {
                return totalNumFieldSpecified;
            }
            set
            {
                totalNumFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class OfferingInstType
    {

        private decimal offeringInstIdField;

        private string ownerEntityTypeField;

        private decimal ownerEntityIdField;

        private bool ownerEntityIdFieldSpecified;

        private decimal offeringIdField;

        private string ownerPartyRoleTypeField;

        private decimal ownerPartyRoleIdField;

        private string purchaseSeqField;

        private decimal brandField;

        private bool brandFieldSpecified;

        private string primaryFlagField;

        private string bundleFlagField;

        private decimal pOfferingInstIdField;

        private bool pOfferingInstIdFieldSpecified;

        private string offeringTypeField;

        private string offeringSubTypeField;

        private string contractFlagField;

        private string applyObjTypeField;

        private decimal applyObjIdField;

        private decimal applyObjBeIdField;

        private bool applyObjBeIdFieldSpecified;

        private string statusField;

        private string statusDetailField;

        private DateTime statusDateField;

        private bool statusDateFieldSpecified;

        private string effModeField;

        private DateTime effDateField;

        private string expModeField;

        private DateTime expDateField;

        private string activeModeField;

        private DateTime activeDateField;

        private bool activeDateFieldSpecified;

        private DateTime lastActiveDateField;

        private bool lastActiveDateFieldSpecified;

        private DateTime trialBeginDateField;

        private bool trialBeginDateFieldSpecified;

        private DateTime trialEndDateField;

        private bool trialEndDateFieldSpecified;

        private decimal salesIdField;

        private bool salesIdFieldSpecified;

        private string salesChannelIdField;

        private string salesChannelTypeField;

        private decimal beIdField;

        /// <remarks/>
        public decimal offeringInstId
        {
            get
            {
                return offeringInstIdField;
            }
            set
            {
                offeringInstIdField = value;
            }
        }

        /// <remarks/>
        public string ownerEntityType
        {
            get
            {
                return ownerEntityTypeField;
            }
            set
            {
                ownerEntityTypeField = value;
            }
        }

        /// <remarks/>
        public decimal ownerEntityId
        {
            get
            {
                return ownerEntityIdField;
            }
            set
            {
                ownerEntityIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool ownerEntityIdSpecified
        {
            get
            {
                return ownerEntityIdFieldSpecified;
            }
            set
            {
                ownerEntityIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal offeringId
        {
            get
            {
                return offeringIdField;
            }
            set
            {
                offeringIdField = value;
            }
        }

        /// <remarks/>
        public string ownerPartyRoleType
        {
            get
            {
                return ownerPartyRoleTypeField;
            }
            set
            {
                ownerPartyRoleTypeField = value;
            }
        }

        /// <remarks/>
        public decimal ownerPartyRoleId
        {
            get
            {
                return ownerPartyRoleIdField;
            }
            set
            {
                ownerPartyRoleIdField = value;
            }
        }

        /// <remarks/>
        public string purchaseSeq
        {
            get
            {
                return purchaseSeqField;
            }
            set
            {
                purchaseSeqField = value;
            }
        }

        /// <remarks/>
        public decimal brand
        {
            get
            {
                return brandField;
            }
            set
            {
                brandField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool brandSpecified
        {
            get
            {
                return brandFieldSpecified;
            }
            set
            {
                brandFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string primaryFlag
        {
            get
            {
                return primaryFlagField;
            }
            set
            {
                primaryFlagField = value;
            }
        }

        /// <remarks/>
        public string bundleFlag
        {
            get
            {
                return bundleFlagField;
            }
            set
            {
                bundleFlagField = value;
            }
        }

        /// <remarks/>
        public decimal pOfferingInstId
        {
            get
            {
                return pOfferingInstIdField;
            }
            set
            {
                pOfferingInstIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool pOfferingInstIdSpecified
        {
            get
            {
                return pOfferingInstIdFieldSpecified;
            }
            set
            {
                pOfferingInstIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string offeringType
        {
            get
            {
                return offeringTypeField;
            }
            set
            {
                offeringTypeField = value;
            }
        }

        /// <remarks/>
        public string offeringSubType
        {
            get
            {
                return offeringSubTypeField;
            }
            set
            {
                offeringSubTypeField = value;
            }
        }

        /// <remarks/>
        public string contractFlag
        {
            get
            {
                return contractFlagField;
            }
            set
            {
                contractFlagField = value;
            }
        }

        /// <remarks/>
        public string applyObjType
        {
            get
            {
                return applyObjTypeField;
            }
            set
            {
                applyObjTypeField = value;
            }
        }

        /// <remarks/>
        public decimal applyObjId
        {
            get
            {
                return applyObjIdField;
            }
            set
            {
                applyObjIdField = value;
            }
        }

        /// <remarks/>
        public decimal applyObjBeId
        {
            get
            {
                return applyObjBeIdField;
            }
            set
            {
                applyObjBeIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool applyObjBeIdSpecified
        {
            get
            {
                return applyObjBeIdFieldSpecified;
            }
            set
            {
                applyObjBeIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string status
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
        public string statusDetail
        {
            get
            {
                return statusDetailField;
            }
            set
            {
                statusDetailField = value;
            }
        }

        /// <remarks/>
        public DateTime statusDate
        {
            get
            {
                return statusDateField;
            }
            set
            {
                statusDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool statusDateSpecified
        {
            get
            {
                return statusDateFieldSpecified;
            }
            set
            {
                statusDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string effMode
        {
            get
            {
                return effModeField;
            }
            set
            {
                effModeField = value;
            }
        }

        /// <remarks/>
        public DateTime effDate
        {
            get
            {
                return effDateField;
            }
            set
            {
                effDateField = value;
            }
        }

        /// <remarks/>
        public string expMode
        {
            get
            {
                return expModeField;
            }
            set
            {
                expModeField = value;
            }
        }

        /// <remarks/>
        public DateTime expDate
        {
            get
            {
                return expDateField;
            }
            set
            {
                expDateField = value;
            }
        }

        /// <remarks/>
        public string activeMode
        {
            get
            {
                return activeModeField;
            }
            set
            {
                activeModeField = value;
            }
        }

        /// <remarks/>
        public DateTime activeDate
        {
            get
            {
                return activeDateField;
            }
            set
            {
                activeDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool activeDateSpecified
        {
            get
            {
                return activeDateFieldSpecified;
            }
            set
            {
                activeDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public DateTime lastActiveDate
        {
            get
            {
                return lastActiveDateField;
            }
            set
            {
                lastActiveDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool lastActiveDateSpecified
        {
            get
            {
                return lastActiveDateFieldSpecified;
            }
            set
            {
                lastActiveDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public DateTime trialBeginDate
        {
            get
            {
                return trialBeginDateField;
            }
            set
            {
                trialBeginDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool trialBeginDateSpecified
        {
            get
            {
                return trialBeginDateFieldSpecified;
            }
            set
            {
                trialBeginDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public DateTime trialEndDate
        {
            get
            {
                return trialEndDateField;
            }
            set
            {
                trialEndDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool trialEndDateSpecified
        {
            get
            {
                return trialEndDateFieldSpecified;
            }
            set
            {
                trialEndDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal salesId
        {
            get
            {
                return salesIdField;
            }
            set
            {
                salesIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool salesIdSpecified
        {
            get
            {
                return salesIdFieldSpecified;
            }
            set
            {
                salesIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string salesChannelId
        {
            get
            {
                return salesChannelIdField;
            }
            set
            {
                salesChannelIdField = value;
            }
        }

        /// <remarks/>
        public string salesChannelType
        {
            get
            {
                return salesChannelTypeField;
            }
            set
            {
                salesChannelTypeField = value;
            }
        }

        /// <remarks/>
        public decimal beId
        {
            get
            {
                return beIdField;
            }
            set
            {
                beIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class SubscriberBasicType
    {

        private decimal subsIdField;

        private bool subsIdFieldSpecified;

        private string ownerPartyRoleTypeField;

        private decimal ownerPartyRoleIdField;

        private bool ownerPartyRoleIdFieldSpecified;

        private string subsNameField;

        private string subsWrittenLangField;

        private string subsVocieLangField;

        private string subsLevelField;

        private decimal offeringIdField;

        private bool offeringIdFieldSpecified;

        private decimal brandField;

        private bool brandFieldSpecified;

        private string primaryIdentityField;

        private decimal usingCustIdField;

        private bool usingCustIdFieldSpecified;

        private decimal guaranteeField;

        private bool guaranteeFieldSpecified;

        private string networkTypeField;

        private decimal defaultAcctIdField;

        private bool defaultAcctIdFieldSpecified;

        private string paymentTypeField;

        private string imsiField;

        private string iccidField;

        private string subTypeField;

        private string portFlagField;

        private DateTime effDateField;

        private bool effDateFieldSpecified;

        private DateTime expDateField;

        private bool expDateFieldSpecified;

        private DateTime activeDateField;

        private bool activeDateFieldSpecified;

        private string statusField;

        private DateTime statusTimeField;

        private bool statusTimeFieldSpecified;

        private string statusDetailField;

        private string salesChannelTypeField;

        private string salesChannelIdField;

        private decimal salesIdField;

        private bool salesIdFieldSpecified;

        private decimal beIdField;

        private bool beIdFieldSpecified;

        private string remarkField;

        /// <remarks/>
        public decimal subsId
        {
            get
            {
                return subsIdField;
            }
            set
            {
                subsIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool subsIdSpecified
        {
            get
            {
                return subsIdFieldSpecified;
            }
            set
            {
                subsIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string ownerPartyRoleType
        {
            get
            {
                return ownerPartyRoleTypeField;
            }
            set
            {
                ownerPartyRoleTypeField = value;
            }
        }

        /// <remarks/>
        public decimal ownerPartyRoleId
        {
            get
            {
                return ownerPartyRoleIdField;
            }
            set
            {
                ownerPartyRoleIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool ownerPartyRoleIdSpecified
        {
            get
            {
                return ownerPartyRoleIdFieldSpecified;
            }
            set
            {
                ownerPartyRoleIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string subsName
        {
            get
            {
                return subsNameField;
            }
            set
            {
                subsNameField = value;
            }
        }

        /// <remarks/>
        public string subsWrittenLang
        {
            get
            {
                return subsWrittenLangField;
            }
            set
            {
                subsWrittenLangField = value;
            }
        }

        /// <remarks/>
        public string subsVocieLang
        {
            get
            {
                return subsVocieLangField;
            }
            set
            {
                subsVocieLangField = value;
            }
        }

        /// <remarks/>
        public string subsLevel
        {
            get
            {
                return subsLevelField;
            }
            set
            {
                subsLevelField = value;
            }
        }

        /// <remarks/>
        public decimal offeringId
        {
            get
            {
                return offeringIdField;
            }
            set
            {
                offeringIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool offeringIdSpecified
        {
            get
            {
                return offeringIdFieldSpecified;
            }
            set
            {
                offeringIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal brand
        {
            get
            {
                return brandField;
            }
            set
            {
                brandField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool brandSpecified
        {
            get
            {
                return brandFieldSpecified;
            }
            set
            {
                brandFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string primaryIdentity
        {
            get
            {
                return primaryIdentityField;
            }
            set
            {
                primaryIdentityField = value;
            }
        }

        /// <remarks/>
        public decimal usingCustId
        {
            get
            {
                return usingCustIdField;
            }
            set
            {
                usingCustIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool usingCustIdSpecified
        {
            get
            {
                return usingCustIdFieldSpecified;
            }
            set
            {
                usingCustIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal guarantee
        {
            get
            {
                return guaranteeField;
            }
            set
            {
                guaranteeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool guaranteeSpecified
        {
            get
            {
                return guaranteeFieldSpecified;
            }
            set
            {
                guaranteeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string networkType
        {
            get
            {
                return networkTypeField;
            }
            set
            {
                networkTypeField = value;
            }
        }

        /// <remarks/>
        public decimal defaultAcctId
        {
            get
            {
                return defaultAcctIdField;
            }
            set
            {
                defaultAcctIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool defaultAcctIdSpecified
        {
            get
            {
                return defaultAcctIdFieldSpecified;
            }
            set
            {
                defaultAcctIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string paymentType
        {
            get
            {
                return paymentTypeField;
            }
            set
            {
                paymentTypeField = value;
            }
        }

        /// <remarks/>
        public string imsi
        {
            get
            {
                return imsiField;
            }
            set
            {
                imsiField = value;
            }
        }

        /// <remarks/>
        public string iccid
        {
            get
            {
                return iccidField;
            }
            set
            {
                iccidField = value;
            }
        }

        /// <remarks/>
        public string subType
        {
            get
            {
                return subTypeField;
            }
            set
            {
                subTypeField = value;
            }
        }

        /// <remarks/>
        public string portFlag
        {
            get
            {
                return portFlagField;
            }
            set
            {
                portFlagField = value;
            }
        }

        /// <remarks/>
        public DateTime effDate
        {
            get
            {
                return effDateField;
            }
            set
            {
                effDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool effDateSpecified
        {
            get
            {
                return effDateFieldSpecified;
            }
            set
            {
                effDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public DateTime expDate
        {
            get
            {
                return expDateField;
            }
            set
            {
                expDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool expDateSpecified
        {
            get
            {
                return expDateFieldSpecified;
            }
            set
            {
                expDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public DateTime activeDate
        {
            get
            {
                return activeDateField;
            }
            set
            {
                activeDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool activeDateSpecified
        {
            get
            {
                return activeDateFieldSpecified;
            }
            set
            {
                activeDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string status
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
        public DateTime statusTime
        {
            get
            {
                return statusTimeField;
            }
            set
            {
                statusTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool statusTimeSpecified
        {
            get
            {
                return statusTimeFieldSpecified;
            }
            set
            {
                statusTimeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string statusDetail
        {
            get
            {
                return statusDetailField;
            }
            set
            {
                statusDetailField = value;
            }
        }

        /// <remarks/>
        public string salesChannelType
        {
            get
            {
                return salesChannelTypeField;
            }
            set
            {
                salesChannelTypeField = value;
            }
        }

        /// <remarks/>
        public string salesChannelId
        {
            get
            {
                return salesChannelIdField;
            }
            set
            {
                salesChannelIdField = value;
            }
        }

        /// <remarks/>
        public decimal salesId
        {
            get
            {
                return salesIdField;
            }
            set
            {
                salesIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool salesIdSpecified
        {
            get
            {
                return salesIdFieldSpecified;
            }
            set
            {
                salesIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal beId
        {
            get
            {
                return beIdField;
            }
            set
            {
                beIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool beIdSpecified
        {
            get
            {
                return beIdFieldSpecified;
            }
            set
            {
                beIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string remark
        {
            get
            {
                return remarkField;
            }
            set
            {
                remarkField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class OfferingInstDetailInfo
    {

        private OfferingInstType offeringBasicField;

        private OfferingInstAttrType[] offeringPropListField;

        private ProdInstDetailInfo[] productListField;

        private ContractType[] contractListField;

        /// <remarks/>
        public OfferingInstType offeringBasic
        {
            get
            {
                return offeringBasicField;
            }
            set
            {
                offeringBasicField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("offeringPropList")]
        public OfferingInstAttrType[] offeringPropList
        {
            get
            {
                return offeringPropListField;
            }
            set
            {
                offeringPropListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("productList")]
        public ProdInstDetailInfo[] productList
        {
            get
            {
                return productListField;
            }
            set
            {
                productListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("contractList")]
        public ContractType[] contractList
        {
            get
            {
                return contractListField;
            }
            set
            {
                contractListField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class OfferingInstAttrType
    {

        private decimal propInstIdField;

        private decimal offeringInstIdField;

        private decimal propIdField;

        private string propCodeField;

        private string complexFlagField;

        private string propValueField;

        private decimal pPropInstIdField;

        private bool pPropInstIdFieldSpecified;

        private string ownerEntityTypeField;

        private decimal ownerEntityIdField;

        private bool ownerEntityIdFieldSpecified;

        private DateTime effDateField;

        private DateTime expDateField;

        private decimal beIdField;

        /// <remarks/>
        public decimal propInstId
        {
            get
            {
                return propInstIdField;
            }
            set
            {
                propInstIdField = value;
            }
        }

        /// <remarks/>
        public decimal offeringInstId
        {
            get
            {
                return offeringInstIdField;
            }
            set
            {
                offeringInstIdField = value;
            }
        }

        /// <remarks/>
        public decimal propId
        {
            get
            {
                return propIdField;
            }
            set
            {
                propIdField = value;
            }
        }

        /// <remarks/>
        public string propCode
        {
            get
            {
                return propCodeField;
            }
            set
            {
                propCodeField = value;
            }
        }

        /// <remarks/>
        public string complexFlag
        {
            get
            {
                return complexFlagField;
            }
            set
            {
                complexFlagField = value;
            }
        }

        /// <remarks/>
        public string propValue
        {
            get
            {
                return propValueField;
            }
            set
            {
                propValueField = value;
            }
        }

        /// <remarks/>
        public decimal pPropInstId
        {
            get
            {
                return pPropInstIdField;
            }
            set
            {
                pPropInstIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool pPropInstIdSpecified
        {
            get
            {
                return pPropInstIdFieldSpecified;
            }
            set
            {
                pPropInstIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string ownerEntityType
        {
            get
            {
                return ownerEntityTypeField;
            }
            set
            {
                ownerEntityTypeField = value;
            }
        }

        /// <remarks/>
        public decimal ownerEntityId
        {
            get
            {
                return ownerEntityIdField;
            }
            set
            {
                ownerEntityIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool ownerEntityIdSpecified
        {
            get
            {
                return ownerEntityIdFieldSpecified;
            }
            set
            {
                ownerEntityIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public DateTime effDate
        {
            get
            {
                return effDateField;
            }
            set
            {
                effDateField = value;
            }
        }

        /// <remarks/>
        public DateTime expDate
        {
            get
            {
                return expDateField;
            }
            set
            {
                expDateField = value;
            }
        }

        /// <remarks/>
        public decimal beId
        {
            get
            {
                return beIdField;
            }
            set
            {
                beIdField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class ProdInstDetailInfo
    {

        private ProdInstType prodBasicField;

        private ProdInstAttrType[] prodAttrListField;

        /// <remarks/>
        public ProdInstType prodBasic
        {
            get
            {
                return prodBasicField;
            }
            set
            {
                prodBasicField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("prodAttrList")]
        public ProdInstAttrType[] prodAttrList
        {
            get
            {
                return prodAttrListField;
            }
            set
            {
                prodAttrListField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class ContractType
    {

        private decimal contractInstIdField;

        private string ownerEntityTypeField;

        private decimal ownerEntityIdField;

        private decimal offeringInstIdField;

        private string contractIdField;

        private string contractCodeField;

        private string contractTypeField;

        private string periodUnitField;

        private string periodField;

        private string statusField;

        private DateTime statusTimeField;

        private DateTime effDateField;

        private DateTime expDateField;

        private DateTime maxEndDateField;

        private bool maxEndDateFieldSpecified;

        private DateTime windowDateField;

        private bool windowDateFieldSpecified;

        private DateTime notifyDateField;

        private bool notifyDateFieldSpecified;

        private string prolongPeriodUnitField;

        private string prolongPeriodField;

        private string prolongTypeField;

        private string autoProlongFlagField;

        private DateTime reserveDateField;

        private bool reserveDateFieldSpecified;

        private string reserveActionField;

        private decimal prolongNewContractField;

        private bool prolongNewContractFieldSpecified;

        private decimal prolongOldContractField;

        private bool prolongOldContractFieldSpecified;

        private decimal beIdField;

        /// <remarks/>
        public decimal contractInstId
        {
            get
            {
                return contractInstIdField;
            }
            set
            {
                contractInstIdField = value;
            }
        }

        /// <remarks/>
        public string ownerEntityType
        {
            get
            {
                return ownerEntityTypeField;
            }
            set
            {
                ownerEntityTypeField = value;
            }
        }

        /// <remarks/>
        public decimal ownerEntityId
        {
            get
            {
                return ownerEntityIdField;
            }
            set
            {
                ownerEntityIdField = value;
            }
        }

        /// <remarks/>
        public decimal offeringInstId
        {
            get
            {
                return offeringInstIdField;
            }
            set
            {
                offeringInstIdField = value;
            }
        }

        /// <remarks/>
        public string contractId
        {
            get
            {
                return contractIdField;
            }
            set
            {
                contractIdField = value;
            }
        }

        /// <remarks/>
        public string contractCode
        {
            get
            {
                return contractCodeField;
            }
            set
            {
                contractCodeField = value;
            }
        }

        /// <remarks/>
        public string contractType
        {
            get
            {
                return contractTypeField;
            }
            set
            {
                contractTypeField = value;
            }
        }

        /// <remarks/>
        public string periodUnit
        {
            get
            {
                return periodUnitField;
            }
            set
            {
                periodUnitField = value;
            }
        }

        /// <remarks/>
        public string period
        {
            get
            {
                return periodField;
            }
            set
            {
                periodField = value;
            }
        }

        /// <remarks/>
        public string status
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
        public DateTime statusTime
        {
            get
            {
                return statusTimeField;
            }
            set
            {
                statusTimeField = value;
            }
        }

        /// <remarks/>
        public DateTime effDate
        {
            get
            {
                return effDateField;
            }
            set
            {
                effDateField = value;
            }
        }

        /// <remarks/>
        public DateTime expDate
        {
            get
            {
                return expDateField;
            }
            set
            {
                expDateField = value;
            }
        }

        /// <remarks/>
        public DateTime maxEndDate
        {
            get
            {
                return maxEndDateField;
            }
            set
            {
                maxEndDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool maxEndDateSpecified
        {
            get
            {
                return maxEndDateFieldSpecified;
            }
            set
            {
                maxEndDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public DateTime windowDate
        {
            get
            {
                return windowDateField;
            }
            set
            {
                windowDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool windowDateSpecified
        {
            get
            {
                return windowDateFieldSpecified;
            }
            set
            {
                windowDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public DateTime notifyDate
        {
            get
            {
                return notifyDateField;
            }
            set
            {
                notifyDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool notifyDateSpecified
        {
            get
            {
                return notifyDateFieldSpecified;
            }
            set
            {
                notifyDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string prolongPeriodUnit
        {
            get
            {
                return prolongPeriodUnitField;
            }
            set
            {
                prolongPeriodUnitField = value;
            }
        }

        /// <remarks/>
        public string prolongPeriod
        {
            get
            {
                return prolongPeriodField;
            }
            set
            {
                prolongPeriodField = value;
            }
        }

        /// <remarks/>
        public string prolongType
        {
            get
            {
                return prolongTypeField;
            }
            set
            {
                prolongTypeField = value;
            }
        }

        /// <remarks/>
        public string autoProlongFlag
        {
            get
            {
                return autoProlongFlagField;
            }
            set
            {
                autoProlongFlagField = value;
            }
        }

        /// <remarks/>
        public DateTime reserveDate
        {
            get
            {
                return reserveDateField;
            }
            set
            {
                reserveDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool reserveDateSpecified
        {
            get
            {
                return reserveDateFieldSpecified;
            }
            set
            {
                reserveDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string reserveAction
        {
            get
            {
                return reserveActionField;
            }
            set
            {
                reserveActionField = value;
            }
        }

        /// <remarks/>
        public decimal prolongNewContract
        {
            get
            {
                return prolongNewContractField;
            }
            set
            {
                prolongNewContractField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool prolongNewContractSpecified
        {
            get
            {
                return prolongNewContractFieldSpecified;
            }
            set
            {
                prolongNewContractFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal prolongOldContract
        {
            get
            {
                return prolongOldContractField;
            }
            set
            {
                prolongOldContractField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool prolongOldContractSpecified
        {
            get
            {
                return prolongOldContractFieldSpecified;
            }
            set
            {
                prolongOldContractFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal beId
        {
            get
            {
                return beIdField;
            }
            set
            {
                beIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class ProdInstType
    {

        private decimal prodInstIdField;

        private decimal offeringInstIdField;

        private bool offeringInstIdFieldSpecified;

        private string ownerEntityTypeField;

        private decimal ownerEntityIdField;

        private bool ownerEntityIdFieldSpecified;

        private string ownerPartyRoleTypeField;

        private decimal ownerPartyRoleIdField;

        private decimal prodIdField;

        private string prodTypeField;

        private string purchaseSeqField;

        private string primaryFlagField;

        private string compositeFlagField;

        private decimal quantityField;

        private bool quantityFieldSpecified;

        private string statusField;

        private string statusDetailField;

        private DateTime statusDateField;

        private bool statusDateFieldSpecified;

        private DateTime effDateField;

        private DateTime expDateField;

        private decimal beIdField;

        /// <remarks/>
        public decimal prodInstId
        {
            get
            {
                return prodInstIdField;
            }
            set
            {
                prodInstIdField = value;
            }
        }

        /// <remarks/>
        public decimal offeringInstId
        {
            get
            {
                return offeringInstIdField;
            }
            set
            {
                offeringInstIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool offeringInstIdSpecified
        {
            get
            {
                return offeringInstIdFieldSpecified;
            }
            set
            {
                offeringInstIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string ownerEntityType
        {
            get
            {
                return ownerEntityTypeField;
            }
            set
            {
                ownerEntityTypeField = value;
            }
        }

        /// <remarks/>
        public decimal ownerEntityId
        {
            get
            {
                return ownerEntityIdField;
            }
            set
            {
                ownerEntityIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool ownerEntityIdSpecified
        {
            get
            {
                return ownerEntityIdFieldSpecified;
            }
            set
            {
                ownerEntityIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string ownerPartyRoleType
        {
            get
            {
                return ownerPartyRoleTypeField;
            }
            set
            {
                ownerPartyRoleTypeField = value;
            }
        }

        /// <remarks/>
        public decimal ownerPartyRoleId
        {
            get
            {
                return ownerPartyRoleIdField;
            }
            set
            {
                ownerPartyRoleIdField = value;
            }
        }

        /// <remarks/>
        public decimal prodId
        {
            get
            {
                return prodIdField;
            }
            set
            {
                prodIdField = value;
            }
        }

        /// <remarks/>
        public string prodType
        {
            get
            {
                return prodTypeField;
            }
            set
            {
                prodTypeField = value;
            }
        }

        /// <remarks/>
        public string purchaseSeq
        {
            get
            {
                return purchaseSeqField;
            }
            set
            {
                purchaseSeqField = value;
            }
        }

        /// <remarks/>
        public string primaryFlag
        {
            get
            {
                return primaryFlagField;
            }
            set
            {
                primaryFlagField = value;
            }
        }

        /// <remarks/>
        public string compositeFlag
        {
            get
            {
                return compositeFlagField;
            }
            set
            {
                compositeFlagField = value;
            }
        }

        /// <remarks/>
        public decimal quantity
        {
            get
            {
                return quantityField;
            }
            set
            {
                quantityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool quantitySpecified
        {
            get
            {
                return quantityFieldSpecified;
            }
            set
            {
                quantityFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string status
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
        public string statusDetail
        {
            get
            {
                return statusDetailField;
            }
            set
            {
                statusDetailField = value;
            }
        }

        /// <remarks/>
        public DateTime statusDate
        {
            get
            {
                return statusDateField;
            }
            set
            {
                statusDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool statusDateSpecified
        {
            get
            {
                return statusDateFieldSpecified;
            }
            set
            {
                statusDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public DateTime effDate
        {
            get
            {
                return effDateField;
            }
            set
            {
                effDateField = value;
            }
        }

        /// <remarks/>
        public DateTime expDate
        {
            get
            {
                return expDateField;
            }
            set
            {
                expDateField = value;
            }
        }

        /// <remarks/>
        public decimal beId
        {
            get
            {
                return beIdField;
            }
            set
            {
                beIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmtype")]
    public partial class ProdInstAttrType
    {

        private decimal propInstIdField;

        private string ownerEntityTypeField;

        private decimal ownerEntityIdField;

        private bool ownerEntityIdFieldSpecified;

        private decimal prodInstIdField;

        private decimal propIdField;

        private string propCodeField;

        private string complexFlagField;

        private string propValueField;

        private decimal pPropInstIdField;

        private bool pPropInstIdFieldSpecified;

        private DateTime effDateField;

        private DateTime expDateField;

        private decimal beIdField;

        /// <remarks/>
        public decimal propInstId
        {
            get
            {
                return propInstIdField;
            }
            set
            {
                propInstIdField = value;
            }
        }

        /// <remarks/>
        public string ownerEntityType
        {
            get
            {
                return ownerEntityTypeField;
            }
            set
            {
                ownerEntityTypeField = value;
            }
        }

        /// <remarks/>
        public decimal ownerEntityId
        {
            get
            {
                return ownerEntityIdField;
            }
            set
            {
                ownerEntityIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool ownerEntityIdSpecified
        {
            get
            {
                return ownerEntityIdFieldSpecified;
            }
            set
            {
                ownerEntityIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal prodInstId
        {
            get
            {
                return prodInstIdField;
            }
            set
            {
                prodInstIdField = value;
            }
        }

        /// <remarks/>
        public decimal propId
        {
            get
            {
                return propIdField;
            }
            set
            {
                propIdField = value;
            }
        }

        /// <remarks/>
        public string propCode
        {
            get
            {
                return propCodeField;
            }
            set
            {
                propCodeField = value;
            }
        }

        /// <remarks/>
        public string complexFlag
        {
            get
            {
                return complexFlagField;
            }
            set
            {
                complexFlagField = value;
            }
        }

        /// <remarks/>
        public string propValue
        {
            get
            {
                return propValueField;
            }
            set
            {
                propValueField = value;
            }
        }

        /// <remarks/>
        public decimal pPropInstId
        {
            get
            {
                return pPropInstIdField;
            }
            set
            {
                pPropInstIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool pPropInstIdSpecified
        {
            get
            {
                return pPropInstIdFieldSpecified;
            }
            set
            {
                pPropInstIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public DateTime effDate
        {
            get
            {
                return effDateField;
            }
            set
            {
                effDateField = value;
            }
        }

        /// <remarks/>
        public DateTime expDate
        {
            get
            {
                return expDateField;
            }
            set
            {
                expDateField = value;
            }
        }

        /// <remarks/>
        public decimal beId
        {
            get
            {
                return beIdField;
            }
            set
            {
                beIdField = value;
            }
        }
    }
































}