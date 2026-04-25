namespace TopinLite.Domain.HuaweiApiModel.CRMResponses.CheckOfferEligibility
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>

    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]

    public partial class EnvelopCheckOfferEligibilityResponse
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

        private CheckOfferingEligibilityRspMsg CheckOfferingEligibilityRspMsgield;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bme/cbsinterface/arservices")]
        public CheckOfferingEligibilityRspMsg CheckOfferingEligibilityRspMsg
        {                                     
            get
            {
                return CheckOfferingEligibilityRspMsgield;
            }
            set
            {
                CheckOfferingEligibilityRspMsgield = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bes/crminterface/cmservices")]
    public partial class CheckOfferingEligibilityRspMsg
    {

        private ResultHeader resultHeaderField;

        private CheckOfferingEligibilityResponse checkOfferingEligibilityResponseField;

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
        public CheckOfferingEligibilityResponse CheckOfferingEligibilityResponse
        {
            get
            {
                return checkOfferingEligibilityResponseField;
            }
            set
            {
                checkOfferingEligibilityResponseField = value;
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
                return versionField;
            }
            set
            {
                versionField = value;
            }
        }

        /// <remarks/>
        public string resultCode
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
        public string msgLanguageCode
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
        public string resultDesc
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("additionalProperty")]
        public ResultHeaderAdditionalProperty[] additionalProperty
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
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.huawei.com/bes/crminterface/cmservices")]
    public partial class CheckOfferingEligibilityResponse
    {

        private decimal offerIdField;

        private decimal isMatchedField;

        private SimpleProperty[] additionalPropertyField;

        /// <remarks/>
        public decimal offerId
        {
            get
            {
                return offerIdField;
            }
            set
            {
                offerIdField = value;
            }
        }

        /// <remarks/>
        public decimal isMatched
        {
            get
            {
                return isMatchedField;
            }
            set
            {
                isMatchedField = value;
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
                return codeField;
            }
            set
            {
                codeField = value;
            }
        }

        /// <remarks/>
        public string value
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
                return codeField;
            }
            set
            {
                codeField = value;
            }
        }

        /// <remarks/>
        public string value
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
}
