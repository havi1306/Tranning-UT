using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Core.Constants
{
    public class AppSettings
    {
        public bool EnableLog { get; set; }

        public PushNotificationCfg PushNotification { get; set; }

        public VOIPNotificationCfg VOIPNotification { get; set; }
    }
}
