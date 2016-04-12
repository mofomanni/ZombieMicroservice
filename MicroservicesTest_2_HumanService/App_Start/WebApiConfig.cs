using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Filters;
using log4net;
using log4net.Core;
using MicroservicesTest2;
using MicroservicesTest_2_HumanService.Controllers;

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

            // Add Logging-Handler to Web API Pipeline
            config.MessageHandlers.Add(new ApiCallLoggingHandler<HumanController>(LogManager.GetLogger(typeof(HumanController)), Level.Debug));
        }
    }

   
}
