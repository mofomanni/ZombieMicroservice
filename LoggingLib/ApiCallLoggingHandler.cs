using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Castle.Core.Logging;
using log4net;
using log4net.Core;
using LoggingLib;

namespace MicroservicesTest2
{
    public class ApiCallLoggingHandler<T>:  DelegatingHandler
    {
        protected readonly ILog _logger;
        protected readonly Level _logLevel;

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
                correlationId = Guid.NewGuid().ToString();
            }

            request.Properties["BGT-CorrelationId"] = correlationId;
            log4net.LogicalThreadContext.Properties["BGT-CorrelationId"] = correlationId;

            Stopwatch duration = Stopwatch.StartNew();

            ZombieLoggingInfo info = new ZombieLoggingInfo();
            info.Uri = request.RequestUri.AbsoluteUri + " <S>";

            _logger.Debug(info);

            var ret = base.SendAsync(request, cancellationToken);
            
            ret.ContinueWith(response =>
            {
                duration.Stop();
                info.Duration = (int)duration.ElapsedMilliseconds;
                info.Uri = request.RequestUri.AbsoluteUri + " <F>";
                _logger.Debug(info);
            }, cancellationToken);

            return ret;
        }
    }
}