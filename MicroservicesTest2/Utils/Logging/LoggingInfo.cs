using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using log4net.Layout;
using log4net.Util;

namespace MicroservicesTest2.Utils.Logging
{
    public class ActionLayoutPattern : PatternLayout
    {
        public ActionLayoutPattern()
        {
            AddConverter("zombieLoggingInfo", typeof (ActionConverter));
        }
    }
}