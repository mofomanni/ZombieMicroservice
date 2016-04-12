using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Castle.DynamicProxy;
using log4net;
using LoggingLib;
using MicroservicesTest2.Models;

namespace MicroservicesTest2.Utils.Logging
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
            info.MethodName = invocation.Method.Name + " <F>";
            info.Duration = sw.Elapsed.Milliseconds;
            
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