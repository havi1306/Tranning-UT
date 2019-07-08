using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCore.Core.Models;

namespace NetCore.API.Services
{
    public interface IApplePushService
    {
        string SendPushNotification(PushNotificationModel model);

        string SendVOIPNotification(PushNotificationModel model);

        void CheckDeviceToken();
    }
}
