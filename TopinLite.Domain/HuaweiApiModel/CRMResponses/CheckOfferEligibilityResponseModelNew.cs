using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopinLite.Domain.HuaweiApiModel.CRMResponses
{


    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public partial class Envelope
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

        private CheckOfferingEligibilityRspMsg checkOfferingEligibilityRspMsgField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bes/crminterface/cmservices")]
        public CheckOfferingEligibilityRspMsg CheckOfferingEligibilityRspMsg
        {
            get
            {
                return checkOfferingEligibilityRspMsgField;
            }
            set
            {
                checkOfferingEligibilityRspMsgField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bes/crminterface/cmservices")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://www.huawei.com/bes/crminterface/cmservices", IsNullable = false)]
    public partial class CheckOfferingEligibilityRspMsg
    {

        private resultHeader resultHeaderField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public resultHeader resultHeader
        {
            get
            {
                return this.resultHeaderField;
            }
            set
            {
                this.resultHeaderField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
    public partial class resultHeader
    {

        private decimal versionField;

        private long resultCodeField;

        private string msgLanguageCodeField;

        private string resultDescField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bes/crminterface/crmheader")]
        public decimal version
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
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bes/crminterface/crmheader")]
        public long resultCode
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
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bes/crminterface/crmheader")]
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
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bes/crminterface/crmheader")]
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
    }




}
