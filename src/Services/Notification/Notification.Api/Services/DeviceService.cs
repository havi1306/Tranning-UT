using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.Extensions.Logging;
using Notification.Api.Services.Interfaces;
using Notification.Core.DTO.Request;
using Notification.Core.Entities;
using Notification.Core.Models;
using Notification.Infrastructure.Repositories.Interfaces;

namespace Notification.Api.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly ILogger<DeviceService> _logger;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IApplePushService _applePushService;

        public DeviceService(
            ILogger<DeviceService> logger,
            IDeviceRepository deviceRepository,
            IApplePushService applePushService)
        {
            this._logger = logger;
            this._deviceRepository = deviceRepository;
            this._applePushService = applePushService;
        }

        /// <summary>
        /// Get all device token paging
        /// </summary>
        /// <param name="page">Page select</param>
        /// <param name="limit">Limit select</param>
        /// <returns>List of device</returns>
        public async Task<IEnumerable<Device>> GetAllPaging(int page, int limit)
        {
            try
            {
                var result = await this._deviceRepository.GetAllPagingAsync(page, limit);

                return result;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Insert new device
        /// </summary>
        /// <param name="username">Username of device</param>
        /// <param name="model">Device info</param>
        /// <returns>Id Device</returns>
        public async Task<int> Insert(string username, NewDevice model)
        {
            try
            {
                var newDevice = model.Adapt<Device>();

                newDevice.Username = model.Username;
                newDevice.CreatedBy = username ?? model.Username;
                newDevice.CreatedAt = DateTime.Now;

                var result = await this._deviceRepository.InsertAsync(newDevice);

                return result;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Send push notification to client app
        /// </summary>
        /// <param name="model">Push information</param>
        /// <returns>Task no return</returns>
        public async Task SendPush(PushNotificationRequest model)
        {
            try
            {
                var userDevices = await this._deviceRepository.GetAllByUsernameAsync(model.Username);

                var iosDevices = userDevices.Where(x => x.DeviceType == Core.Enumerations.DeviceType.IOS).ToList();
                var androidDevices = userDevices.Where(x => x.DeviceType == Core.Enumerations.DeviceType.Android).ToList();

                if (iosDevices.Count > 0)
                {
                    var pushData = new PushNotificationModel
                    {
                        Type = model.Type,
                        Message = model.Message,
                        DeviceTokens = iosDevices.Select(x => x.DeviceToken).ToList()
                    };

                    var result = this._applePushService.SendPushNotification(pushData);

                    this._logger.LogInformation(result);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Send voip notification to client app
        /// </summary>
        /// <param name="model">Push information</param>
        /// <returns>Task no return</returns>
        public async Task SendVOIPNotification(PushNotificationRequest model)
        {
            try
            {
                var userDevices = await this._deviceRepository.GetAllByUsernameAsync(model.Username);

                var iosDevices = userDevices.Where(x => x.DeviceType == Core.Enumerations.DeviceType.IOS).ToList();
                var androidDevices = userDevices.Where(x => x.DeviceType == Core.Enumerations.DeviceType.Android).ToList();

                if (iosDevices.Count > 0)
                {
                    var pushData = new PushNotificationModel
                    {
                        Type = model.Type,
                        Message = model.Message,
                        DeviceTokens = iosDevices.Select(x => x.DeviceToken).ToList()
                    };

                    var result = this._applePushService.SendVOIPNotification(pushData);

                    this._logger.LogInformation(result);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Delete device
        /// </summary>
        /// <param name="id">Id of device</param>
        /// <returns>Delete success</returns>
        public async Task<bool> Delete(int id)
        {
            try
            {
                return await this._deviceRepository.DeleteAsync(new Device { Id = id });
            }
            catch
            {
                throw;
            }
        }
    }
}
