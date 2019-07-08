using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Notification.Core.Models;

namespace Notification.Api.Services.Interfaces
{
    public interface IApplePushService
    {
        string SendPushNotification(PushNotificationModel model);

        string SendVOIPNotification(PushNotificationModel model);

        void CheckDeviceToken();
    }
}
