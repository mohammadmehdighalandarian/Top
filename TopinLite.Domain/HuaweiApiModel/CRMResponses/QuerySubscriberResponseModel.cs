

namespace TopinLite.Domain.HuaweiApiModel.CRMResponses.QuerySubscriber
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public partial class EnvelopeQuerySubscriberResponse
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

    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public partial class Fault
    {
        private Code codeField;
        private Reason reasonField;


        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bes/crminterface/cmservices")]
        public Code Code
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


        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bes/crminterface/cmservices")]
        public Reason Reason
        {
            get
            {
                return reasonField;
            }
            set
            {
                reasonField = value;
            }
        }
    }

    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public partial class Code
    {
        private string valueField;

        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bes/crminterface/cmservices")]
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


    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public partial class Reason
    {
        private string textField;


        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bes/crminterface/cmservices")]
        public string Text
        {
            get { return textField; }
            set { textField = value; }
        }
    }


    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public partial class EnvelopeBody
    {
        private querySubscriberRspMsg querySubscriberRspMsgField;
        private Fault faultField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bes/crminterface/cmservices")]
        public querySubscriberRspMsg querySubscriberRspMsg
        {
            get
            {
                return querySubscriberRspMsgField;
            }
            set
            {
                querySubscriberRspMsgField = value;
            }
        }

        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bes/crminterface/cmservices")]
        public Fault Fault
        {
            get
            {
                return faultField;
            }
            set
            {
                faultField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bes/crminterface/cmservices")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://www.huawei.com/bes/crminterface/cmservices", IsNullable = false)]
    public partial class querySubscriberRspMsg
    {
        private ResultHeader resultHeaderField;

        private QuerySubscriberResponse querySubscriberResponseField;

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
        public QuerySubscriberResponse querySubscriberResponse
        {
            get
            {
                return querySubscriberResponseField;
            }
            set
            {
                querySubscriberResponseField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmservices")]
    public partial class QuerySubscriberResponse
    {

        private Subscriber[] subscriberInfoField;

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
                return this.subInfoField;
            }
            set
            {
                this.subInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("primaryOfferingList")]
        public OfferingInstDetailInfo[] primaryOfferingList
        {
            get
            {
                return this.primaryOfferingListField;
            }
            set
            {
                this.primaryOfferingListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("supplementaryOfferingList")]
        public OfferingInstDetailInfo[] supplementaryOfferingList
        {
            get
            {
                return this.supplementaryOfferingListField;
            }
            set
            {
                this.supplementaryOfferingListField = value;
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
                return this.pageSizeField;
            }
            set
            {
                this.pageSizeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool pageSizeSpecified
        {
            get
            {
                return this.pageSizeFieldSpecified;
            }
            set
            {
                this.pageSizeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public int startNum
        {
            get
            {
                return this.startNumField;
            }
            set
            {
                this.startNumField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool startNumSpecified
        {
            get
            {
                return this.startNumFieldSpecified;
            }
            set
            {
                this.startNumFieldSpecified = value;
            }
        }

        /// <remarks/>
        public int totalNum
        {
            get
            {
                return this.totalNumField;
            }
            set
            {
                this.totalNumField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool totalNumSpecified
        {
            get
            {
                return this.totalNumFieldSpecified;
            }
            set
            {
                this.totalNumFieldSpecified = value;
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
                return this.subsIdField;
            }
            set
            {
                this.subsIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool subsIdSpecified
        {
            get
            {
                return this.subsIdFieldSpecified;
            }
            set
            {
                this.subsIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string ownerPartyRoleType
        {
            get
            {
                return this.ownerPartyRoleTypeField;
            }
            set
            {
                this.ownerPartyRoleTypeField = value;
            }
        }

        /// <remarks/>
        public decimal ownerPartyRoleId
        {
            get
            {
                return this.ownerPartyRoleIdField;
            }
            set
            {
                this.ownerPartyRoleIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool ownerPartyRoleIdSpecified
        {
            get
            {
                return this.ownerPartyRoleIdFieldSpecified;
            }
            set
            {
                this.ownerPartyRoleIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string subsName
        {
            get
            {
                return this.subsNameField;
            }
            set
            {
                this.subsNameField = value;
            }
        }

        /// <remarks/>
        public string subsWrittenLang
        {
            get
            {
                return this.subsWrittenLangField;
            }
            set
            {
                this.subsWrittenLangField = value;
            }
        }

        /// <remarks/>
        public string subsVocieLang
        {
            get
            {
                return this.subsVocieLangField;
            }
            set
            {
                this.subsVocieLangField = value;
            }
        }

        /// <remarks/>
        public string subsLevel
        {
            get
            {
                return this.subsLevelField;
            }
            set
            {
                this.subsLevelField = value;
            }
        }

        /// <remarks/>
        public decimal offeringId
        {
            get
            {
                return this.offeringIdField;
            }
            set
            {
                this.offeringIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool offeringIdSpecified
        {
            get
            {
                return this.offeringIdFieldSpecified;
            }
            set
            {
                this.offeringIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal brand
        {
            get
            {
                return this.brandField;
            }
            set
            {
                this.brandField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool brandSpecified
        {
            get
            {
                return this.brandFieldSpecified;
            }
            set
            {
                this.brandFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string primaryIdentity
        {
            get
            {
                return this.primaryIdentityField;
            }
            set
            {
                this.primaryIdentityField = value;
            }
        }

        /// <remarks/>
        public decimal usingCustId
        {
            get
            {
                return this.usingCustIdField;
            }
            set
            {
                this.usingCustIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool usingCustIdSpecified
        {
            get
            {
                return this.usingCustIdFieldSpecified;
            }
            set
            {
                this.usingCustIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal guarantee
        {
            get
            {
                return this.guaranteeField;
            }
            set
            {
                this.guaranteeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool guaranteeSpecified
        {
            get
            {
                return this.guaranteeFieldSpecified;
            }
            set
            {
                this.guaranteeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string networkType
        {
            get
            {
                return this.networkTypeField;
            }
            set
            {
                this.networkTypeField = value;
            }
        }

        /// <remarks/>
        public decimal defaultAcctId
        {
            get
            {
                return this.defaultAcctIdField;
            }
            set
            {
                this.defaultAcctIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool defaultAcctIdSpecified
        {
            get
            {
                return this.defaultAcctIdFieldSpecified;
            }
            set
            {
                this.defaultAcctIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string paymentType
        {
            get
            {
                return this.paymentTypeField;
            }
            set
            {
                this.paymentTypeField = value;
            }
        }

        /// <remarks/>
        public string imsi
        {
            get
            {
                return this.imsiField;
            }
            set
            {
                this.imsiField = value;
            }
        }

        /// <remarks/>
        public string iccid
        {
            get
            {
                return this.iccidField;
            }
            set
            {
                this.iccidField = value;
            }
        }

        /// <remarks/>
        public string subType
        {
            get
            {
                return this.subTypeField;
            }
            set
            {
                this.subTypeField = value;
            }
        }

        /// <remarks/>
        public string portFlag
        {
            get
            {
                return this.portFlagField;
            }
            set
            {
                this.portFlagField = value;
            }
        }

        /// <remarks/>
        public DateTime effDate
        {
            get
            {
                return this.effDateField;
            }
            set
            {
                this.effDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool effDateSpecified
        {
            get
            {
                return this.effDateFieldSpecified;
            }
            set
            {
                this.effDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public DateTime expDate
        {
            get
            {
                return this.expDateField;
            }
            set
            {
                this.expDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool expDateSpecified
        {
            get
            {
                return this.expDateFieldSpecified;
            }
            set
            {
                this.expDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public DateTime activeDate
        {
            get
            {
                return this.activeDateField;
            }
            set
            {
                this.activeDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool activeDateSpecified
        {
            get
            {
                return this.activeDateFieldSpecified;
            }
            set
            {
                this.activeDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        public DateTime statusTime
        {
            get
            {
                return this.statusTimeField;
            }
            set
            {
                this.statusTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool statusTimeSpecified
        {
            get
            {
                return this.statusTimeFieldSpecified;
            }
            set
            {
                this.statusTimeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string statusDetail
        {
            get
            {
                return this.statusDetailField;
            }
            set
            {
                this.statusDetailField = value;
            }
        }

        /// <remarks/>
        public string salesChannelType
        {
            get
            {
                return this.salesChannelTypeField;
            }
            set
            {
                this.salesChannelTypeField = value;
            }
        }

        /// <remarks/>
        public string salesChannelId
        {
            get
            {
                return this.salesChannelIdField;
            }
            set
            {
                this.salesChannelIdField = value;
            }
        }

        /// <remarks/>
        public decimal salesId
        {
            get
            {
                return this.salesIdField;
            }
            set
            {
                this.salesIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool salesIdSpecified
        {
            get
            {
                return this.salesIdFieldSpecified;
            }
            set
            {
                this.salesIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal beId
        {
            get
            {
                return this.beIdField;
            }
            set
            {
                this.beIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool beIdSpecified
        {
            get
            {
                return this.beIdFieldSpecified;
            }
            set
            {
                this.beIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string remark
        {
            get
            {
                return this.remarkField;
            }
            set
            {
                this.remarkField = value;
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
                return this.offeringBasicField;
            }
            set
            {
                this.offeringBasicField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("offeringPropList")]
        public OfferingInstAttrType[] offeringPropList
        {
            get
            {
                return this.offeringPropListField;
            }
            set
            {
                this.offeringPropListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("productList")]
        public ProdInstDetailInfo[] productList
        {
            get
            {
                return this.productListField;
            }
            set
            {
                this.productListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("contractList")]
        public ContractType[] contractList
        {
            get
            {
                return this.contractListField;
            }
            set
            {
                this.contractListField = value;
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
                return this.offeringInstIdField;
            }
            set
            {
                this.offeringInstIdField = value;
            }
        }

        /// <remarks/>
        public string ownerEntityType
        {
            get
            {
                return this.ownerEntityTypeField;
            }
            set
            {
                this.ownerEntityTypeField = value;
            }
        }

        /// <remarks/>
        public decimal ownerEntityId
        {
            get
            {
                return this.ownerEntityIdField;
            }
            set
            {
                this.ownerEntityIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool ownerEntityIdSpecified
        {
            get
            {
                return this.ownerEntityIdFieldSpecified;
            }
            set
            {
                this.ownerEntityIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal offeringId
        {
            get
            {
                return this.offeringIdField;
            }
            set
            {
                this.offeringIdField = value;
            }
        }

        /// <remarks/>
        public string ownerPartyRoleType
        {
            get
            {
                return this.ownerPartyRoleTypeField;
            }
            set
            {
                this.ownerPartyRoleTypeField = value;
            }
        }

        /// <remarks/>
        public decimal ownerPartyRoleId
        {
            get
            {
                return this.ownerPartyRoleIdField;
            }
            set
            {
                this.ownerPartyRoleIdField = value;
            }
        }

        /// <remarks/>
        public string purchaseSeq
        {
            get
            {
                return this.purchaseSeqField;
            }
            set
            {
                this.purchaseSeqField = value;
            }
        }

        /// <remarks/>
        public decimal brand
        {
            get
            {
                return this.brandField;
            }
            set
            {
                this.brandField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool brandSpecified
        {
            get
            {
                return this.brandFieldSpecified;
            }
            set
            {
                this.brandFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string primaryFlag
        {
            get
            {
                return this.primaryFlagField;
            }
            set
            {
                this.primaryFlagField = value;
            }
        }

        /// <remarks/>
        public string bundleFlag
        {
            get
            {
                return this.bundleFlagField;
            }
            set
            {
                this.bundleFlagField = value;
            }
        }

        /// <remarks/>
        public decimal pOfferingInstId
        {
            get
            {
                return this.pOfferingInstIdField;
            }
            set
            {
                this.pOfferingInstIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool pOfferingInstIdSpecified
        {
            get
            {
                return this.pOfferingInstIdFieldSpecified;
            }
            set
            {
                this.pOfferingInstIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string offeringType
        {
            get
            {
                return this.offeringTypeField;
            }
            set
            {
                this.offeringTypeField = value;
            }
        }

        /// <remarks/>
        public string offeringSubType
        {
            get
            {
                return this.offeringSubTypeField;
            }
            set
            {
                this.offeringSubTypeField = value;
            }
        }

        /// <remarks/>
        public string contractFlag
        {
            get
            {
                return this.contractFlagField;
            }
            set
            {
                this.contractFlagField = value;
            }
        }

        /// <remarks/>
        public string applyObjType
        {
            get
            {
                return this.applyObjTypeField;
            }
            set
            {
                this.applyObjTypeField = value;
            }
        }

        /// <remarks/>
        public decimal applyObjId
        {
            get
            {
                return this.applyObjIdField;
            }
            set
            {
                this.applyObjIdField = value;
            }
        }

        /// <remarks/>
        public decimal applyObjBeId
        {
            get
            {
                return this.applyObjBeIdField;
            }
            set
            {
                this.applyObjBeIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool applyObjBeIdSpecified
        {
            get
            {
                return this.applyObjBeIdFieldSpecified;
            }
            set
            {
                this.applyObjBeIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        public string statusDetail
        {
            get
            {
                return this.statusDetailField;
            }
            set
            {
                this.statusDetailField = value;
            }
        }

        /// <remarks/>
        public DateTime statusDate
        {
            get
            {
                return this.statusDateField;
            }
            set
            {
                this.statusDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool statusDateSpecified
        {
            get
            {
                return this.statusDateFieldSpecified;
            }
            set
            {
                this.statusDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string effMode
        {
            get
            {
                return this.effModeField;
            }
            set
            {
                this.effModeField = value;
            }
        }

        /// <remarks/>
        public DateTime effDate
        {
            get
            {
                return this.effDateField;
            }
            set
            {
                this.effDateField = value;
            }
        }

        /// <remarks/>
        public string expMode
        {
            get
            {
                return this.expModeField;
            }
            set
            {
                this.expModeField = value;
            }
        }

        /// <remarks/>
        public DateTime expDate
        {
            get
            {
                return this.expDateField;
            }
            set
            {
                this.expDateField = value;
            }
        }

        /// <remarks/>
        public string activeMode
        {
            get
            {
                return this.activeModeField;
            }
            set
            {
                this.activeModeField = value;
            }
        }

        /// <remarks/>
        public DateTime activeDate
        {
            get
            {
                return this.activeDateField;
            }
            set
            {
                this.activeDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool activeDateSpecified
        {
            get
            {
                return this.activeDateFieldSpecified;
            }
            set
            {
                this.activeDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public DateTime lastActiveDate
        {
            get
            {
                return this.lastActiveDateField;
            }
            set
            {
                this.lastActiveDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool lastActiveDateSpecified
        {
            get
            {
                return this.lastActiveDateFieldSpecified;
            }
            set
            {
                this.lastActiveDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public DateTime trialBeginDate
        {
            get
            {
                return this.trialBeginDateField;
            }
            set
            {
                this.trialBeginDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool trialBeginDateSpecified
        {
            get
            {
                return this.trialBeginDateFieldSpecified;
            }
            set
            {
                this.trialBeginDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public DateTime trialEndDate
        {
            get
            {
                return this.trialEndDateField;
            }
            set
            {
                this.trialEndDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool trialEndDateSpecified
        {
            get
            {
                return this.trialEndDateFieldSpecified;
            }
            set
            {
                this.trialEndDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal salesId
        {
            get
            {
                return this.salesIdField;
            }
            set
            {
                this.salesIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool salesIdSpecified
        {
            get
            {
                return this.salesIdFieldSpecified;
            }
            set
            {
                this.salesIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string salesChannelId
        {
            get
            {
                return this.salesChannelIdField;
            }
            set
            {
                this.salesChannelIdField = value;
            }
        }

        /// <remarks/>
        public string salesChannelType
        {
            get
            {
                return this.salesChannelTypeField;
            }
            set
            {
                this.salesChannelTypeField = value;
            }
        }

        /// <remarks/>
        public decimal beId
        {
            get
            {
                return this.beIdField;
            }
            set
            {
                this.beIdField = value;
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
                return this.propInstIdField;
            }
            set
            {
                this.propInstIdField = value;
            }
        }

        /// <remarks/>
        public decimal offeringInstId
        {
            get
            {
                return this.offeringInstIdField;
            }
            set
            {
                this.offeringInstIdField = value;
            }
        }

        /// <remarks/>
        public decimal propId
        {
            get
            {
                return this.propIdField;
            }
            set
            {
                this.propIdField = value;
            }
        }

        /// <remarks/>
        public string propCode
        {
            get
            {
                return this.propCodeField;
            }
            set
            {
                this.propCodeField = value;
            }
        }

        /// <remarks/>
        public string complexFlag
        {
            get
            {
                return this.complexFlagField;
            }
            set
            {
                this.complexFlagField = value;
            }
        }

        /// <remarks/>
        public string propValue
        {
            get
            {
                return this.propValueField;
            }
            set
            {
                this.propValueField = value;
            }
        }

        /// <remarks/>
        public decimal pPropInstId
        {
            get
            {
                return this.pPropInstIdField;
            }
            set
            {
                this.pPropInstIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool pPropInstIdSpecified
        {
            get
            {
                return this.pPropInstIdFieldSpecified;
            }
            set
            {
                this.pPropInstIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string ownerEntityType
        {
            get
            {
                return this.ownerEntityTypeField;
            }
            set
            {
                this.ownerEntityTypeField = value;
            }
        }

        /// <remarks/>
        public decimal ownerEntityId
        {
            get
            {
                return this.ownerEntityIdField;
            }
            set
            {
                this.ownerEntityIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool ownerEntityIdSpecified
        {
            get
            {
                return this.ownerEntityIdFieldSpecified;
            }
            set
            {
                this.ownerEntityIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public DateTime effDate
        {
            get
            {
                return this.effDateField;
            }
            set
            {
                this.effDateField = value;
            }
        }

        /// <remarks/>
        public DateTime expDate
        {
            get
            {
                return this.expDateField;
            }
            set
            {
                this.expDateField = value;
            }
        }

        /// <remarks/>
        public decimal beId
        {
            get
            {
                return this.beIdField;
            }
            set
            {
                this.beIdField = value;
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
                return this.prodBasicField;
            }
            set
            {
                this.prodBasicField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("prodAttrList")]
        public ProdInstAttrType[] prodAttrList
        {
            get
            {
                return this.prodAttrListField;
            }
            set
            {
                this.prodAttrListField = value;
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
                return this.contractInstIdField;
            }
            set
            {
                this.contractInstIdField = value;
            }
        }

        /// <remarks/>
        public string ownerEntityType
        {
            get
            {
                return this.ownerEntityTypeField;
            }
            set
            {
                this.ownerEntityTypeField = value;
            }
        }

        /// <remarks/>
        public decimal ownerEntityId
        {
            get
            {
                return this.ownerEntityIdField;
            }
            set
            {
                this.ownerEntityIdField = value;
            }
        }

        /// <remarks/>
        public decimal offeringInstId
        {
            get
            {
                return this.offeringInstIdField;
            }
            set
            {
                this.offeringInstIdField = value;
            }
        }

        /// <remarks/>
        public string contractId
        {
            get
            {
                return this.contractIdField;
            }
            set
            {
                this.contractIdField = value;
            }
        }

        /// <remarks/>
        public string contractCode
        {
            get
            {
                return this.contractCodeField;
            }
            set
            {
                this.contractCodeField = value;
            }
        }

        /// <remarks/>
        public string contractType
        {
            get
            {
                return this.contractTypeField;
            }
            set
            {
                this.contractTypeField = value;
            }
        }

        /// <remarks/>
        public string periodUnit
        {
            get
            {
                return this.periodUnitField;
            }
            set
            {
                this.periodUnitField = value;
            }
        }

        /// <remarks/>
        public string period
        {
            get
            {
                return this.periodField;
            }
            set
            {
                this.periodField = value;
            }
        }

        /// <remarks/>
        public string status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        public DateTime statusTime
        {
            get
            {
                return this.statusTimeField;
            }
            set
            {
                this.statusTimeField = value;
            }
        }

        /// <remarks/>
        public DateTime effDate
        {
            get
            {
                return this.effDateField;
            }
            set
            {
                this.effDateField = value;
            }
        }

        /// <remarks/>
        public DateTime expDate
        {
            get
            {
                return this.expDateField;
            }
            set
            {
                this.expDateField = value;
            }
        }

        /// <remarks/>
        public DateTime maxEndDate
        {
            get
            {
                return this.maxEndDateField;
            }
            set
            {
                this.maxEndDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool maxEndDateSpecified
        {
            get
            {
                return this.maxEndDateFieldSpecified;
            }
            set
            {
                this.maxEndDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public DateTime windowDate
        {
            get
            {
                return this.windowDateField;
            }
            set
            {
                this.windowDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool windowDateSpecified
        {
            get
            {
                return this.windowDateFieldSpecified;
            }
            set
            {
                this.windowDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public DateTime notifyDate
        {
            get
            {
                return this.notifyDateField;
            }
            set
            {
                this.notifyDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool notifyDateSpecified
        {
            get
            {
                return this.notifyDateFieldSpecified;
            }
            set
            {
                this.notifyDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string prolongPeriodUnit
        {
            get
            {
                return this.prolongPeriodUnitField;
            }
            set
            {
                this.prolongPeriodUnitField = value;
            }
        }

        /// <remarks/>
        public string prolongPeriod
        {
            get
            {
                return this.prolongPeriodField;
            }
            set
            {
                this.prolongPeriodField = value;
            }
        }

        /// <remarks/>
        public string prolongType
        {
            get
            {
                return this.prolongTypeField;
            }
            set
            {
                this.prolongTypeField = value;
            }
        }

        /// <remarks/>
        public string autoProlongFlag
        {
            get
            {
                return this.autoProlongFlagField;
            }
            set
            {
                this.autoProlongFlagField = value;
            }
        }

        /// <remarks/>
        public DateTime reserveDate
        {
            get
            {
                return this.reserveDateField;
            }
            set
            {
                this.reserveDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool reserveDateSpecified
        {
            get
            {
                return this.reserveDateFieldSpecified;
            }
            set
            {
                this.reserveDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string reserveAction
        {
            get
            {
                return this.reserveActionField;
            }
            set
            {
                this.reserveActionField = value;
            }
        }

        /// <remarks/>
        public decimal prolongNewContract
        {
            get
            {
                return this.prolongNewContractField;
            }
            set
            {
                this.prolongNewContractField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool prolongNewContractSpecified
        {
            get
            {
                return this.prolongNewContractFieldSpecified;
            }
            set
            {
                this.prolongNewContractFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal prolongOldContract
        {
            get
            {
                return this.prolongOldContractField;
            }
            set
            {
                this.prolongOldContractField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool prolongOldContractSpecified
        {
            get
            {
                return this.prolongOldContractFieldSpecified;
            }
            set
            {
                this.prolongOldContractFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal beId
        {
            get
            {
                return this.beIdField;
            }
            set
            {
                this.beIdField = value;
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
                return this.prodInstIdField;
            }
            set
            {
                this.prodInstIdField = value;
            }
        }

        /// <remarks/>
        public decimal offeringInstId
        {
            get
            {
                return this.offeringInstIdField;
            }
            set
            {
                this.offeringInstIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool offeringInstIdSpecified
        {
            get
            {
                return this.offeringInstIdFieldSpecified;
            }
            set
            {
                this.offeringInstIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string ownerEntityType
        {
            get
            {
                return this.ownerEntityTypeField;
            }
            set
            {
                this.ownerEntityTypeField = value;
            }
        }

        /// <remarks/>
        public decimal ownerEntityId
        {
            get
            {
                return this.ownerEntityIdField;
            }
            set
            {
                this.ownerEntityIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool ownerEntityIdSpecified
        {
            get
            {
                return this.ownerEntityIdFieldSpecified;
            }
            set
            {
                this.ownerEntityIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string ownerPartyRoleType
        {
            get
            {
                return this.ownerPartyRoleTypeField;
            }
            set
            {
                this.ownerPartyRoleTypeField = value;
            }
        }

        /// <remarks/>
        public decimal ownerPartyRoleId
        {
            get
            {
                return this.ownerPartyRoleIdField;
            }
            set
            {
                this.ownerPartyRoleIdField = value;
            }
        }

        /// <remarks/>
        public decimal prodId
        {
            get
            {
                return this.prodIdField;
            }
            set
            {
                this.prodIdField = value;
            }
        }

        /// <remarks/>
        public string prodType
        {
            get
            {
                return this.prodTypeField;
            }
            set
            {
                this.prodTypeField = value;
            }
        }

        /// <remarks/>
        public string purchaseSeq
        {
            get
            {
                return this.purchaseSeqField;
            }
            set
            {
                this.purchaseSeqField = value;
            }
        }

        /// <remarks/>
        public string primaryFlag
        {
            get
            {
                return this.primaryFlagField;
            }
            set
            {
                this.primaryFlagField = value;
            }
        }

        /// <remarks/>
        public string compositeFlag
        {
            get
            {
                return this.compositeFlagField;
            }
            set
            {
                this.compositeFlagField = value;
            }
        }

        /// <remarks/>
        public decimal quantity
        {
            get
            {
                return this.quantityField;
            }
            set
            {
                this.quantityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool quantitySpecified
        {
            get
            {
                return this.quantityFieldSpecified;
            }
            set
            {
                this.quantityFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        public string statusDetail
        {
            get
            {
                return this.statusDetailField;
            }
            set
            {
                this.statusDetailField = value;
            }
        }

        /// <remarks/>
        public DateTime statusDate
        {
            get
            {
                return this.statusDateField;
            }
            set
            {
                this.statusDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool statusDateSpecified
        {
            get
            {
                return this.statusDateFieldSpecified;
            }
            set
            {
                this.statusDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public DateTime effDate
        {
            get
            {
                return this.effDateField;
            }
            set
            {
                this.effDateField = value;
            }
        }

        /// <remarks/>
        public DateTime expDate
        {
            get
            {
                return this.expDateField;
            }
            set
            {
                this.expDateField = value;
            }
        }

        /// <remarks/>
        public decimal beId
        {
            get
            {
                return this.beIdField;
            }
            set
            {
                this.beIdField = value;
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
                return this.propInstIdField;
            }
            set
            {
                this.propInstIdField = value;
            }
        }

        /// <remarks/>
        public string ownerEntityType
        {
            get
            {
                return this.ownerEntityTypeField;
            }
            set
            {
                this.ownerEntityTypeField = value;
            }
        }

        /// <remarks/>
        public decimal ownerEntityId
        {
            get
            {
                return this.ownerEntityIdField;
            }
            set
            {
                this.ownerEntityIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool ownerEntityIdSpecified
        {
            get
            {
                return this.ownerEntityIdFieldSpecified;
            }
            set
            {
                this.ownerEntityIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal prodInstId
        {
            get
            {
                return this.prodInstIdField;
            }
            set
            {
                this.prodInstIdField = value;
            }
        }

        /// <remarks/>
        public decimal propId
        {
            get
            {
                return this.propIdField;
            }
            set
            {
                this.propIdField = value;
            }
        }

        /// <remarks/>
        public string propCode
        {
            get
            {
                return this.propCodeField;
            }
            set
            {
                this.propCodeField = value;
            }
        }

        /// <remarks/>
        public string complexFlag
        {
            get
            {
                return this.complexFlagField;
            }
            set
            {
                this.complexFlagField = value;
            }
        }

        /// <remarks/>
        public string propValue
        {
            get
            {
                return this.propValueField;
            }
            set
            {
                this.propValueField = value;
            }
        }

        /// <remarks/>
        public decimal pPropInstId
        {
            get
            {
                return this.pPropInstIdField;
            }
            set
            {
                this.pPropInstIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool pPropInstIdSpecified
        {
            get
            {
                return this.pPropInstIdFieldSpecified;
            }
            set
            {
                this.pPropInstIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public DateTime effDate
        {
            get
            {
                return this.effDateField;
            }
            set
            {
                this.effDateField = value;
            }
        }

        /// <remarks/>
        public DateTime expDate
        {
            get
            {
                return this.expDateField;
            }
            set
            {
                this.expDateField = value;
            }
        }

        /// <remarks/>
        public decimal beId
        {
            get
            {
                return this.beIdField;
            }
            set
            {
                this.beIdField = value;
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


























}