using System.Diagnostics;
using System.Linq;
using Castle.DynamicProxy;
using log4net;
using LoggingLib;
using MicroservicesTest_2_HumanService.Models;

namespace MicroservicesTest_2_HumanService.Logging
{
    public class MethodCallLogging:GenericLoggingInterceptor
    {
        public MethodCallLogging(ILog logger) : base(logger)
        {
        }

        public override void Intercept(IInvocation invocation)
        {
            ZombieLoggingInfo info = new ZombieLoggingInfo();
            info.Class = invocation.TargetType.FullName;
            info.MethodName = invocation.Method.Name + " <S>";
            info.ZombieItSelf = GetZombieSightingFromArguments(invocation);

            var sw = Stopwatch.StartNew();
            Logger.Debug(info);

            invocation.Proceed();
            sw.Stop();
            info.Duration = sw.Elapsed.Milliseconds;
            info.MethodName = invocation.Method.Name + " <F>";
            Logger.Debug(info);
        }

        public string GetZombieSightingFromArguments(IInvocation invocation)
        {
            if (invocation.Arguments != null)
            {
                return (from arg in invocation.Arguments where arg != null && arg.GetType() == typeof (ZombieSighting) select ((ZombieSighting) arg).ToString()).FirstOrDefault();
            }

            return "";
        }
    }
}