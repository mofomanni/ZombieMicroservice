using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using log4net;
using MicroservicesTest_2_HumanService.Logging;
using MicroservicesTest_2_HumanService.Utils;
using Owin;
using ProxyGenerator = Castle.DynamicProxy.ProxyGenerator;

namespace MicroservicesTest_2_HumanService.Properties
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            WebApiConfig.Register(config);

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
       
            builder.Register(ctx =>
            {
                IZombieCreator zombieCreator = new ZombieCreator();

                ProxyGenerator generator = new ProxyGenerator();
                IZombieCreator proxy = (IZombieCreator)generator.CreateInterfaceProxyWithTarget(
                    typeof(IZombieCreator),
                    zombieCreator,
                    new MethodCallLogging(LogManager.GetLogger(typeof(ZombieCreator))));

                return proxy;
            }).SingleInstance();

            var container = builder.Build();

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }
    }
}