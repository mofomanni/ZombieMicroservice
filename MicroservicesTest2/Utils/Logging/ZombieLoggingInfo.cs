using MicroservicesTest2.Models;
using Newtonsoft.Json;

namespace MicroservicesTest2.Utils.Logging
{
    public class ZombieLoggingInfo
    {
        public ZombieLoggingInfo()
        {
        }

        public string CorrelationId { get; set; }

        public string Uri { get; set; }

        public string Class { get; set; }

        public string MethodName { get; set; }

        public int Duration { get; set; }

        public string ExceptionDetails { get; set; }

        public string ZombieItSelf
        {
            get { return base.ToString(); }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}