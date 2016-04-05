using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Castle.Core.Logging;
using log4net;
using log4net.Core;

namespace MicroservicesTest2
{
    public class ApiCallLoggingHandler<T>:  DelegatingHandler
    {
        private readonly ILog _logger;
        private readonly Level _logLevel;

        public ApiCallLoggingHandler(ILog logger, Level logLevel)
        {
            _logger = logger;
            this._logLevel = logLevel;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string correlationId = "N/A";
            if (request.Headers.Contains("BGT-CorrelationId"))
            {
                correlationId = request.Headers.GetValues("BGT-CorrelationId").First();
            }
            else if (!request.Properties.ContainsKey("BGT-CorrelationId"))
            {
                correlationId= Guid.NewGuid().ToString();
            }

            request.Properties["BGT-CorrelationId"] = correlationId;
            log4net.LogicalThreadContext.Properties["BGT-CorrelationId"] = correlationId;

            DateTime start = DateTime.Now;

            _logger.Logger.Log(typeof(T), _logLevel, CreateStringFromRequest(request) + "Start [" + DateTime.Now + "]", null);

            var ret= base.SendAsync(request, cancellationToken);
            var duration = DateTime.Now - start;
            _logger.Logger.Log(typeof(T), _logLevel, CreateStringFromRequest(request) + "End [" + DateTime.Now + "] call took [" + duration.Milliseconds + "]ms",null);

            return ret;
        }

        private string CreateStringFromRequest(HttpRequestMessage request)
        {
            string ret = "BGT-CorrelationId[" + request.Properties["BGT-CorrelationId"] + "]";

            return ret;
        }
    }
}