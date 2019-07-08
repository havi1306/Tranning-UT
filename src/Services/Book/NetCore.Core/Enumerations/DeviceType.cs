using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NetCore.Core.Enumerations
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
