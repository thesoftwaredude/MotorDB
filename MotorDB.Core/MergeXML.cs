using System.Xml.Linq;
using System.Xml.XPath;

namespace MotorDB.Core
{
    public class MergeXml
    {
        public XPathNavigator MergeDocument(string rootXmlDocument, string xmlDocumentToMerge)
        {
            var xml1 = XDocument.Load(rootXmlDocument);
            var xml2 = XDocument.Load(xmlDocumentToMerge).Descendants("members");

            foreach (var element in xml2.Descendants("member"))
            {
                xml1.Root.Element("members").LastNode.AddAfterSelf(element);
            }

            return xml1.CreateNavigator();
        }
    }
}
