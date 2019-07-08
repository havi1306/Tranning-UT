using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Notification.Core.DTO.Request;
using Notification.Core.Entities;

namespace Notification.Api.Services.Interfaces
{
    public interface IDeviceService
    {
        /// <summary>
        /// Get all device token paging
        /// </summary>
        /// <param name="page">Page select</param>
        /// <param name="limit">Limit select</param>
        /// <returns>List of device</returns>
        Task<IEnumerable<Device>> GetAllPaging(int page, int limit);

        /// <summary>
        /// Insert new device
        /// </summary>
        /// <param name="username">Username of device</param>
        /// <param name="model">Device info</param>
        /// <returns>Id Device</returns>
        Task<int> Insert(string username, NewDevice model);

        /// <summary>
        /// Send push notification to client app
        /// </summary>
        /// <param name="model">Push information</param>
        /// <returns>Task no return</returns>
        Task SendPush(PushNotificationRequest model);

        /// <summary>
        /// Send voip notification to client app
        /// </summary>
        /// <param name="model">Push information</param>
        /// <returns>Task no return</returns>
        Task SendVOIPNotification(PushNotificationRequest model);

        /// <summary>
        /// Delete device
        /// </summary>
        /// <param name="id">Id of device</param>
        /// <returns>Delete success</returns>
        Task<bool> Delete(int id);
    }
}
