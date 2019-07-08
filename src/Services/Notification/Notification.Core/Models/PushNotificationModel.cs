using System;
using System.Collections.Generic;
using System.Text;

namespace Notification.Core.Models
{
    public class PushNotificationModel
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public KeyValuePair<string, object> Parameter { get; set; }
        public List<string> DeviceTokens { get; set; }
    }
}
