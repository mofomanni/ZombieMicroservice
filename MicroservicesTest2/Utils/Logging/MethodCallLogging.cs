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
            ZombieLoggingInfo info = null;
            //if (invocation.Arguments != null)
            //{
            //    foreach (var arg in invocation.Arguments)
            //    {
            //        if (arg.GetType() == typeof(ZombieSighting))
            //        {
            //            info = new ZombieLoggingInfo
            //            {
            //                CorrelationId = ((ZombieSighting)arg).CorrelationId
            //            };
            //        }
            //    }
            //}

            if (info == null)
            {
                info = new ZombieLoggingInfo();
            }

            info.Class = invocation.TargetType.FullName;
            info.MethodName = invocation.Method.Name;
            Logger.Debug(info);

            var sw = Stopwatch.StartNew();
          //  Logger.Info(string.Format("::Executing Method '{0}.{1}'", invocation.TargetType.FullName, invocation.Method.Name));
            invocation.Proceed();
            sw.Stop();

            
            info.Duration = sw.Elapsed.Milliseconds;
            Logger.Debug(info);
            

        //    Logger.Debug(string.Format("::Executed Method '{0}.{1}' ({2:0.000}ms)", invocation.TargetType.FullName, invocation.Method.Name, sw.Elapsed.TotalMilliseconds));
        }
    }
}