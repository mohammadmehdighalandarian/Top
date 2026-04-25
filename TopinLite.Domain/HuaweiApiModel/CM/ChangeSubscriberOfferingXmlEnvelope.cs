using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TopinLite.Domain.HuaweiApiModel.CM
{
   public class Envelope
    {
        public Body Body { get; set; }

        private static XmlSerializerNamespaces staticxmlns;

        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces xmlns { get { return staticxmlns; } set { } }

        public Envelope()
        {
            staticxmlns = new XmlSerializerNamespaces();
            staticxmlns.Add("soap", "http://schemas.xmlsoap.org/soap/envelope/");
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.8.4084.0")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.huawei.com/bes/crminterface/cmservices")]
    public class Body
    {
        public changeSubscriberOfferingReqMsg changeSubscriberOfferingReqMsg { get; set; }
    }
}
