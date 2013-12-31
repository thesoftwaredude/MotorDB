using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using System.Xml.XPath;
using MotorDB.Core;

namespace MotorDB.UI.Areas.HelpPage
{

    public static class StringExtensions
    {
        public static string IncludeSeeNodeInReturn(this string returnDescription)
        {
            //InnerXml <returns>Returns a list of <see cref="T:MotorDB.Core.Models.Policy" /> objects</returns>
            var startOfseeNode = returnDescription.IndexOf("<see");

            if (startOfseeNode < 0)
                return returnDescription;

            var endOfseeNode = returnDescription.IndexOf("/>", startOfseeNode);

            var crefData = returnDescription.Substring(startOfseeNode + 11, returnDescription.Substring(startOfseeNode + 11).IndexOf("/>") - 2).Split('.');
            var crefObject = crefData.Last();
            var result = String.Format("{0}{1}{2}", returnDescription.Substring(0, startOfseeNode), crefObject, returnDescription.Substring(endOfseeNode + 2));
            return result;
        }
    }

    /// <summary>
    /// A custom <see cref="IDocumentationProvider"/> that reads the API documentation from an XML documentation file.
    /// </summary>
    public class XmlDocumentationProvider : IDocumentationProvider, IResponseDocumentationProvider
    {
        private XPathNavigator _documentNavigator;
        private const string TypeExpression = "/doc/members/member[@name='T:{0}']";
        private const string MethodExpression = "/doc/members/member[@name='M:{0}']";
        private const string ParameterExpression = "param[@name='{0}']";

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlDocumentationProvider"/> class.
        /// </summary>
        /// <param name="documentPath">The physical path to XML document.</param>
        public XmlDocumentationProvider(string documentPath)
        {
            if (documentPath == null)
            {
                throw new ArgumentNullException("documentPath");
            }
            XPathDocument xpath = new XPathDocument(documentPath);
            _documentNavigator = xpath.CreateNavigator();
        }

        public XmlDocumentationProvider(string rootDocument, string secondaryDocument)
        {
            var mergeXML = new MergeXml();
            if (string.IsNullOrEmpty(rootDocument) || string.IsNullOrEmpty(secondaryDocument))
            {
                throw new ArgumentNullException(string.Format("Invalid file locations {0}, {1}", rootDocument, secondaryDocument));
            }

            _documentNavigator = mergeXML.MergeDocument(rootDocument, secondaryDocument);
        }

        public string GetDocumentation(HttpControllerDescriptor controllerDescriptor)
        {
            XPathNavigator typeNode = GetTypeNode(controllerDescriptor);
            return GetTagValue(typeNode, "summary");
        }

        public virtual string GetDocumentation(HttpActionDescriptor actionDescriptor)
        {
            XPathNavigator methodNode = GetMethodNode(actionDescriptor);
            return GetTagValue(methodNode, "summary");
        }

        public virtual string GetDocumentation(HttpParameterDescriptor parameterDescriptor)
        {
            ReflectedHttpParameterDescriptor reflectedParameterDescriptor = parameterDescriptor as ReflectedHttpParameterDescriptor;
            if (reflectedParameterDescriptor != null)
            {
                XPathNavigator methodNode = GetMethodNode(reflectedParameterDescriptor.ActionDescriptor);
                if (methodNode != null)
                {
                    string parameterName = reflectedParameterDescriptor.ParameterInfo.Name;
                    XPathNavigator parameterNode = methodNode.SelectSingleNode(String.Format(CultureInfo.InvariantCulture, ParameterExpression, parameterName));
                    if (parameterNode != null)
                    {
                        return parameterNode.Value.Trim();
                    }
                }
            }

            return null;
        }

        public string GetResponseDocumentation(HttpActionDescriptor actionDescriptor)
        {
            //XPathNavigator methodNode = GetMethodNode(actionDescriptor);
            //return GetTagValue(methodNode, "returns");
            XPathNavigator methodNode = GetMethodNode(actionDescriptor);
            if (methodNode != null)
            {
                XPathNavigator returnsNode = methodNode.SelectSingleNode("returns");
                if (returnsNode != null)
                {
                    return returnsNode.InnerXml.IncludeSeeNodeInReturn();
                }
            }
            return null;
        }

        private XPathNavigator GetMethodNode(HttpActionDescriptor actionDescriptor)
        {
            ReflectedHttpActionDescriptor reflectedActionDescriptor = actionDescriptor as ReflectedHttpActionDescriptor;
            if (reflectedActionDescriptor != null)
            {
                string selectExpression = String.Format(CultureInfo.InvariantCulture, MethodExpression, GetMemberName(reflectedActionDescriptor.MethodInfo));
                return _documentNavigator.SelectSingleNode(selectExpression);
            }

            return null;
        }

        private static string GetMemberName(MethodInfo method)
        {
            string name = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", method.DeclaringType.FullName, method.Name);
            ParameterInfo[] parameters = method.GetParameters();
            if (parameters.Length != 0)
            {
                string[] parameterTypeNames = parameters.Select(param => GetTypeName(param.ParameterType)).ToArray();
                name += String.Format(CultureInfo.InvariantCulture, "({0})", String.Join(",", parameterTypeNames));
            }

            return name;
        }

        private static string GetTagValue(XPathNavigator parentNode, string tagName)
        {
            if (parentNode != null)
            {
                XPathNavigator node = parentNode.SelectSingleNode(tagName);
                if (node != null)
                {
                    return node.Value.Trim();
                }
            }

            return null;
        }

        private static string GetTypeName(Type type)
        {
            if (type.IsGenericType)
            {
                // Format the generic type name to something like: Generic{System.Int32,System.String}
                Type genericType = type.GetGenericTypeDefinition();
                Type[] genericArguments = type.GetGenericArguments();
                string typeName = genericType.FullName;

                // Trim the generic parameter counts from the name
                typeName = typeName.Substring(0, typeName.IndexOf('`'));
                string[] argumentTypeNames = genericArguments.Select(t => GetTypeName(t)).ToArray();
                return String.Format(CultureInfo.InvariantCulture, "{0}{{{1}}}", typeName, String.Join(",", argumentTypeNames));
            }

            return type.FullName;
        }

        private XPathNavigator GetTypeNode(HttpControllerDescriptor controllerDescriptor)
        {
            Type controllerType = controllerDescriptor.ControllerType;
            string controllerTypeName = controllerType.FullName;
            if (controllerType.IsNested)
            {
                // Changing the nested type name from OuterType+InnerType to OuterType.InnerType to match the XML documentation syntax.
                controllerTypeName = controllerTypeName.Replace("+", ".");
            }
            string selectExpression = String.Format(CultureInfo.InvariantCulture, TypeExpression, controllerTypeName);
            return _documentNavigator.SelectSingleNode(selectExpression);
        }
    }
}
