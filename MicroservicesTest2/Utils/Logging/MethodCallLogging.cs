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
            //  Logger.Info(string.Format("::Executing Method '{0}.{1}'", invocation.TargetType.FullName, invocation.Method.Name));
            var sw = Stopwatch.StartNew();
            invocation.Proceed();
            sw.Stop();
            //    Logger.Debug(string.Format("::Executed Method '{0}.{1}' ({2:0.000}ms)", invocation.TargetType.FullName, invocation.Method.Name, sw.Elapsed.TotalMilliseconds));

            ZombieLoggingInfo info =  new ZombieLoggingInfo();;
            info.Class = invocation.TargetType.FullName;
            info.MethodName = invocation.Method.Name;
            info.Duration = sw.Elapsed.Milliseconds;
            Logger.Debug(info);
        }
    }
}