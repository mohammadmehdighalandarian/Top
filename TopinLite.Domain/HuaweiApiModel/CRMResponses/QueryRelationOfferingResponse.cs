using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopinLite.Domain.HuaweiApiModel.CRMResponses.QueryRelationOffering
{



    //// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    ///// <remarks/>
    //[System.SerializableAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    //[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    //public partial class Envelope
    //{

    //    private EnvelopeBody bodyField;

    //    /// <remarks/>
    //    public EnvelopeBody Body
    //    {
    //        get
    //        {
    //            return this.bodyField;
    //        }
    //        set
    //        {
    //            this.bodyField = value;
    //        }
    //    }
    //}

    ///// <remarks/>
    //[System.SerializableAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    //public partial class EnvelopeBody
    //{

    //    private EnvelopeBodyQueryRelationOfferingRspMsg queryRelationOfferingRspMsgField;

    //    /// <remarks/>
    //    public EnvelopeBodyQueryRelationOfferingRspMsg QueryRelationOfferingRspMsg
    //    {
    //        get
    //        {
    //            return this.queryRelationOfferingRspMsgField;
    //        }
    //        set
    //        {
    //            this.queryRelationOfferingRspMsgField = value;
    //        }
    //    }
    //}

    ///// <remarks/>
    //[System.SerializableAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    //public partial class EnvelopeBodyQueryRelationOfferingRspMsg
    //{

    //    private EnvelopeBodyQueryRelationOfferingRspMsgResultHeader resultHeaderField;

    //    /// <remarks/>
    //    public EnvelopeBodyQueryRelationOfferingRspMsgResultHeader resultHeader
    //    {
    //        get
    //        {
    //            return this.resultHeaderField;
    //        }
    //        set
    //        {
    //            this.resultHeaderField = value;
    //        }
    //    }
    //}

    ///// <remarks/>
    //[System.SerializableAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    //public partial class EnvelopeBodyQueryRelationOfferingRspMsgResultHeader
    //{

    //    private decimal versionField;

    //    private byte resultCodeField;

    //    private string msgLanguageCodeField;

    //    private string resultDescField;

    //    /// <remarks/>
    //    public decimal version
    //    {
    //        get
    //        {
    //            return this.versionField;
    //        }
    //        set
    //        {
    //            this.versionField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public byte resultCode
    //    {
    //        get
    //        {
    //            return this.resultCodeField;
    //        }
    //        set
    //        {
    //            this.resultCodeField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public string msgLanguageCode
    //    {
    //        get
    //        {
    //            return this.msgLanguageCodeField;
    //        }
    //        set
    //        {
    //            this.msgLanguageCodeField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public string resultDesc
    //    {
    //        get
    //        {
    //            return this.resultDescField;
    //        }
    //        set
    //        {
    //            this.resultDescField = value;
    //        }
    //    }
    //}













    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "")]
    [System.Xml.Serialization.XmlRoot(ElementName = "Envelope", Namespace = "", IsNullable = false)]
    public partial class EnvelopeQueryRelationOfferingResponse
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
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class EnvelopeBody
    {
        private QueryRelationOfferingRspMsg queryRelationOfferingRspMsgField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "")]
        public QueryRelationOfferingRspMsg QueryRelationOfferingRspMsg
        {
            get
            {
                return queryRelationOfferingRspMsgField;
            }
            set
            {
                queryRelationOfferingRspMsgField = value;
            }
        }
    }

    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "")]
    [System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
    public partial class QueryRelationOfferingRspMsg
    {
        private ResultHeader resultHeaderField;

        private QueryRelationOfferingResponse queryRelationOfferingResponseField;

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
        public QueryRelationOfferingResponse QueryRelationOfferingResponse
        {
            get
            {
                return queryRelationOfferingResponseField;
            }
            set
            {
                queryRelationOfferingResponseField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "")]
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
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "")]
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
    [System.Xml.Serialization.XmlType(Namespace = "")]
    public partial class QueryRelationOfferingResponse
    {

        private string[] offeringIDField;

        private SimpleProperty1[] additionalPropertyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("OfferingID")]
        public string[] OfferingID
        {
            get
            {
                return offeringIDField;
            }
            set
            {
                offeringIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("additionalProperty")]
        public SimpleProperty1[] additionalProperty
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
    [System.Xml.Serialization.XmlType(TypeName = "SimpleProperty", Namespace = "")]
    public partial class SimpleProperty1
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
