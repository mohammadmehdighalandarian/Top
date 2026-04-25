using Newtonsoft.Json;

using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace TopinLite.Infra.ApiClient.SOAPApi.HuaweiEndpoint.Statics
{
    public class XmlParserLib
    {
        public static string ToString(object[] a)
        {
            StringBuilder sb = new StringBuilder('[');
            if (a.Length > 0)
            {
                sb.Append(a[0]);
                for (int index = 1; index < a.Length; ++index)
                {
                    sb.Append(", ").Append(a[index]);
                }
            }
            sb.Append(']');
            return sb.ToString();
        }

        private static XElement RemoveAllNamespaces(XElement xmlDocument)
        {
            if (!xmlDocument.HasElements)
            {
                XElement xElement = new XElement(xmlDocument.Name.LocalName);
                xElement.Value = xmlDocument.Value;

                return xElement;
            }
            return new XElement(xmlDocument.Name.LocalName, xmlDocument.Elements().Select(el => RemoveAllNamespaces(el)));
        }

        public static string RemoveAllNamespaces(string xmlDocument)
        {
            XElement xmlDocumentWithoutNs = RemoveAllNamespaces(XElement.Parse(xmlDocument));

            return xmlDocumentWithoutNs.ToString();
        }

        public static string CleanAndConvertToJson(string Model)
        {
            XmlDocument doc = new XmlDocument();
            Console.WriteLine("model " + Model);
            string removedNameSpaces = RemoveAllNamespaces(Model);
            Console.WriteLine("removed name spaces: " + removedNameSpaces);
            doc.LoadXml(RemoveAllNamespaces(Model));
            return JsonConvert.SerializeXmlNode(doc);
        }
    }
}