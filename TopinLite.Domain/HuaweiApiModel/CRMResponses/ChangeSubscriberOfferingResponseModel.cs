namespace TopinLite.Domain.HuaweiApiModel.CRMResponses.ChangeSubscriberOffering
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public partial class EnvelopeChangeSubscriberOfferingResponse
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
        private changeSubscriberOfferingRspMsg changeSubscriberOfferingRspMsgField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bes/crminterface/cmservices")]
        public changeSubscriberOfferingRspMsg changeSubscriberOfferingRspMsg
        {
            get
            {
                return changeSubscriberOfferingRspMsgField;
            }
            set
            {
                changeSubscriberOfferingRspMsgField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bes/crminterface/cmservices")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://www.huawei.com/bes/crminterface/cmservices", IsNullable = false)]
    public partial class changeSubscriberOfferingRspMsg
    {
        private resultHeader resultHeaderField;

        private changeSubscriberOfferingResponse changeSubscriberOfferingResponseField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public resultHeader resultHeader
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
        public changeSubscriberOfferingResponse changeSubscriberOfferingResponse
        {
            get
            {
                return changeSubscriberOfferingResponseField;
            }
            set
            {
                changeSubscriberOfferingResponseField = value;
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

        private decimal resultCodeField;

        private string resultDescField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bes/crminterface/crmheader")]
        public decimal version
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
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bes/crminterface/crmheader")]
        public decimal resultCode
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
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bes/crminterface/crmheader")]
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
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
    public partial class changeSubscriberOfferingResponse
    {
        private long orderIdField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.huawei.com/bes/crminterface/cmservices")]
        public long orderId
        {
            get
            {
                return orderIdField;
            }
            set
            {
                orderIdField = value;
            }
        }
    }
}