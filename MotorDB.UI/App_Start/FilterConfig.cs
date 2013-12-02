using System.Web;
using System.Web.Mvc;

//cs1591
namespace MotorDB.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}