using System.Diagnostics;
using Castle.DynamicProxy;
using log4net;

namespace LoggingLib
{
    public abstract class GenericLoggingInterceptor : IInterceptor
    {
        protected readonly ILog Logger;

        protected GenericLoggingInterceptor(ILog logger)
        {
            Logger = logger;
        }

        public abstract void Intercept(IInvocation invocation);
    }
}