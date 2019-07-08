using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Notification.Core.Enumerations
{
    public enum DeviceType
    {
        /// <summary>
        /// Android device
        /// </summary>
        [Description("Android")]
        Android = 1,

        /// <summary>
        /// IOS Device
        /// </summary>
        [Description("IOS")]
        IOS = 2,
    }
}
