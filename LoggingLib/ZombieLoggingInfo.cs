using Newtonsoft.Json;

namespace LoggingLib
{
    public class ZombieLoggingInfo
    {
        public string Uri { get; set; }

        public string Class { get; set; }

        public string MethodName { get; set; }

        public int Duration { get; set; }

        public string ExceptionDetails { get; set; }

        public string ZombieItSelf { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}