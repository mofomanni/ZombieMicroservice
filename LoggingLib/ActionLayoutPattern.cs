using log4net.Layout;

namespace LoggingLib
{
    public class ActionLayoutPattern : PatternLayout
    {
        public ActionLayoutPattern()
        {
            AddConverter("zombieLoggingInfo", typeof (ActionConverter));
        }
    }
}