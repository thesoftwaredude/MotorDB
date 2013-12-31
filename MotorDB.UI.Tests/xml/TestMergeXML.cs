using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using MotorDB.Core;
using NUnit.Framework;

namespace MotorDB.UI.Tests.xml
{
    [TestFixture]
    public class TestMergeXML
    {
        [Test]
        public void TestName()
        {
            var mergeXml = new MergeXml();
            var result = mergeXml.MergeDocument("xml\\ui.xml", "xml\\core.xml");
            var a = 1;
        }

        [Test]
        public void ProcessFile()
        {
            string MethodExpression = "/doc/members/member[@name='M:{0}']";
            var xmlui = "<?xml version=\"1.0\"?>" +
                        "<doc>" +
                        "    <assembly>" +
                        "        <name>MotorDB.UI</name>" +
                        "    </assembly>" +
                        "    <members>" +
                        "        <member name=\"M:MotorDB.UI.api.PolicyController.Get\">" +
                        "            <summary>" +
                        "            Get a list of all policies" +
                        "            </summary>" +
                        "            <returns>List of <see cref=\"T:MotorDB.Core.Models.Policy\"/> objects</returns>" +
                        "        </member>" +
                        "    </members>" +
                        "</doc>";
            string documentPath = "xml\\ui.xml";
            var xmldoc = new XmlDocument();
            xmldoc.LoadXml(xmlui);
            var _documentNavigator = xmldoc.CreateNavigator();

            string selectExpression = String.Format(CultureInfo.InvariantCulture, MethodExpression, "MotorDB.UI.api.PolicyController.Get");
            var summary = _documentNavigator.SelectSingleNode(selectExpression);
            var returns = summary.SelectSingleNode("returns");
            var rtn = returns.InnerXml;
            var startOfseeNode = rtn.IndexOf("<see");
            var endOfseeNode = rtn.IndexOf("/>", startOfseeNode);

            var seeObjects = rtn.Substring(startOfseeNode+11, rtn.Substring(startOfseeNode+11).IndexOf("/>")-2).Split('.');
            var seeObject = seeObjects.Last();
            var returnDescription = string.Format("{0}{1}{2}", rtn.Substring(0, startOfseeNode), seeObject, rtn.Substring(endOfseeNode+2));
            
            Assert.That(returnDescription, Is.EqualTo("List of Policy objects"));

            var a = 1;
            
        }
    }
}
