using System.Web.Http.Controllers;

namespace MotorDB.UI.Areas.HelpPage
{
    public interface IResponseDocumentationProvider
    {
        string GetResponseDocumentation(HttpActionDescriptor actionDescriptor);
    }
}