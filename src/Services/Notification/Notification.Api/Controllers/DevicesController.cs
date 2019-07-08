using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Notification.Api.Services.Interfaces;
using Notification.Core.DTO.Request;

namespace Notification.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IDeviceService _deviceService;

        public DevicesController(
            ILogger<DevicesController> logger,
            IDeviceService deviceService)
        {
            this._deviceService = deviceService;
            this._logger = logger;
        }
        
        /// <summary>
        /// Get all device tokens
        /// </summary>
        /// <param name="page">Page select</param>
        /// <param name="limit">Limit select</param>
        /// <returns>List of device token</returns>
        [HttpGet]
        public async Task<IActionResult> Gets(int page = 1, int limit = 10)
        {
            try
            {
                var result = await this._deviceService.GetAllPaging(page, limit);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);

                throw ex;
            }
        }

        /// <summary>
        /// Add new device
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Id</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]NewDevice request)
        {
            try
            {
                var id = await this._deviceService.Insert(User.Identity.Name, request);

                return this.Ok(id);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);

                throw ex;
            }
        }

        /// <summary>
        /// Send push notification to client app
        /// </summary>
        /// <param name="request">Push notification request</param>
        /// <returns>Send Push Ok</returns>
        [HttpPost("push")]
        public async Task<IActionResult> Push([FromBody]PushNotificationRequest request)
        {
            try
            {
                await this._deviceService.SendPush(request);

                return this.Ok();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);

                throw ex;
            }
        }

        /// <summary>
        /// Send push notification to client app
        /// </summary>
        /// <param name="request">Push notification request</param>
        /// <returns>Send Push Ok</returns>
        [HttpPost("voipnotification")]
        public async Task<IActionResult> VOIPNotification([FromBody]PushNotificationRequest request)
        {
            try
            {
                await this._deviceService.SendVOIPNotification(request);

                return this.Ok();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);

                throw ex;
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await this._deviceService.Delete(id);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);

                throw ex;
            }
        }
    }
}
