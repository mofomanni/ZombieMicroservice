using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Castle.DynamicProxy;
using log4net;
using MicroservicesTest2;
using MicroservicesTest2.Persistency;
using MicroservicesTest2.Utils.Logging;
using Owin;

public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        var config = new HttpConfiguration();

        WebApiConfig.Register(config);

        var builder = new ContainerBuilder();
        builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

        builder.Register((IComponentContext ctx) =>
        {
            IPersistencyLayer persistencyLayer = new PersistencyLayer(ctx.Resolve<IZombiePersistency>());

            ProxyGenerator generator = new ProxyGenerator();
            IPersistencyLayer proxy = (IPersistencyLayer)generator.CreateInterfaceProxyWithTarget(
                typeof(IPersistencyLayer),
                persistencyLayer,
                new MethodCallLogging(LogManager.GetLogger(typeof(PersistencyLayer))));

            return proxy;
        }).SingleInstance();


        builder.Register<IZombiePersistency>((IComponentContext ctx) =>
        {
            IZombiePersistency zombiePersistency = new ZombiePersistency();

            ProxyGenerator generator = new ProxyGenerator();
            IZombiePersistency proxy = (IZombiePersistency) generator.CreateInterfaceProxyWithTarget(
                typeof(IZombiePersistency), 
                zombiePersistency,
                new MethodCallLogging(LogManager.GetLogger(typeof(ZombiePersistency))));

            return proxy;
        }).SingleInstance();

        var container = builder.Build();

        app.UseAutofacMiddleware(container);
        app.UseAutofacWebApi(config);
        app.UseWebApi(config);
    }
}