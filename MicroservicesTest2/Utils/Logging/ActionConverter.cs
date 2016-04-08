using System;
using System.IO;
using log4net.Core;
using log4net.Util;

namespace MicroservicesTest2.Utils.Logging
{
    public class ActionConverter : PatternConverter
    {
        protected override void Convert(TextWriter writer, object state)
        {
            var loggingEvent = state as LoggingEvent;

            if (loggingEvent == null || !(loggingEvent.MessageObject is LoggingEvent))
            {
                HandleDefault(writer);
                return;
            }

            var zombieLoggingInfo = loggingEvent.MessageObject as ZombieLoggingInfo;

            switch (this.Option.ToLower())
            {
                case "uri":
                    writer.Write(zombieLoggingInfo.Uri);
                    break;
                case "class":
                    writer.Write(zombieLoggingInfo.Class);
                    break;
                case "methodname":
                    writer.Write(zombieLoggingInfo.MethodName);
                    break;
                //case "zombieId":
                //    writer.Write(zombieLoggingInfo.ZombieId);
                //    break;
                case "exceptiondetails":
                    writer.Write(zombieLoggingInfo.ExceptionDetails);
                    break;
                case "zombieitself":
                    writer.Write(zombieLoggingInfo.ZombieItSelf);
                    break;
                case "duration":
                    writer.Write(zombieLoggingInfo.Duration);
                    break;
                default:
                    HandleDefault(writer);
                    break;
            }

        }

        private void HandleDefault(TextWriter writer)
        {
            if (this.Option == "duration")
            {
                writer.Write(-1);
            }
        }
    }
}