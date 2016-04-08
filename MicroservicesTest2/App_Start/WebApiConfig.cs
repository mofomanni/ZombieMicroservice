using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using log4net;
using log4net.Core;
using MicroservicesTest2.Controllers;
using MicroservicesTest2.Persistency;

namespace MicroservicesTest2
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
                    routeTemplate: "api/v7/{controller}",
                    defaults: new { id = RouteParameter.Optional }
            );

            // Add Logging-Handler to Web API Pipeline
            config.MessageHandlers.Add(new ApiCallLoggingHandler<ZombieController>(LogManager.GetLogger(typeof(ZombieController)),Level.Debug));
        }
    }
}
