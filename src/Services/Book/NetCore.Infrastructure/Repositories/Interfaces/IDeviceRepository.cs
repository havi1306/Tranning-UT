using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCore.Core.BookAudio.Entities;
using NetCore.Core.Entities;
using NetCore.Infrastructure;
using NetCore.Infrastructure.Repositories.Interfaces;

namespace NetCore.Infrastructure.Repositories.Interfaces
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
