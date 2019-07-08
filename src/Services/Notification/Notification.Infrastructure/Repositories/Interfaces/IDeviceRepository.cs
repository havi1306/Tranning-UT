using Notification.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Infrastructure.Repositories.Interfaces
{
    public interface IDeviceRepository : IRepository<Device>
    {
        /// <summary>
        /// Get all devices by username
        /// </summary>
        /// <param name="username">Username to get devices</param>
        /// <returns>A collection of device info</returns>
        Task<IEnumerable<Device>> GetAllByUsernameAsync(string username);

        /// <summary>
        /// Get all devices paging
        /// </summary>
        /// <param name="page">Username to get devices</param>
        /// <param name="limit">Username to get devices</param>
        /// <returns>A collection of device info</returns>
        Task<IEnumerable<Device>> GetAllPagingAsync(int page, int limit);
    }
}
