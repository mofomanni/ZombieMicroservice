using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MicroservicesTest_2_HumanService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                    routeTemplate: "api/human/{controller}",
                    defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
