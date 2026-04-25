using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopinLite.Domain.HuaweiApiModel.CM
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bes/crminterface/cmservices")]
    public partial class changeSubscriberOfferingRspMsg
    {

        private ResultHeader resultHeaderField;

        private ChangeSubscriberOfferingResponse changeSubscriberOfferingResponseField;

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
        public ChangeSubscriberOfferingResponse changeSubscriberOfferingResponse
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
    public partial class ChangeSubscriberOfferingResponse
    {

        private decimal orderIdField;

        /// <remarks/>
        public decimal orderId
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

}
