using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;

namespace MotorDB.UI.Tests
{
    public static class Extensions
    {
        public static T SetupControllerConfigs<T>(this T controller) where T : ApiController
        {
            var config = new HttpConfiguration();

            var request = new HttpRequestMessage(HttpMethod.Post, "http://server.com/foo");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", controller.GetType().Name } });
            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            return controller;
        }
        public static T AddCustomRequestHeader<T>(this T controller, string header, string value) where T : ApiController
        {
            controller.Request.Headers.Add(header, value);
            return controller;
        }
    }
}
