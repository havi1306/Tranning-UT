using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using NetCore.Core.BookAudio.Entities;
using NetCore.Core.Entities;
using NetCore.Infrastructure;
using NetCore.Infrastructure.Repositories;
using NetCore.Infrastructure.Repositories.Interfaces;

namespace NetCore.Infrastructure.Repositories
{
    public class DeviceRepository : Repository<Device>, IDeviceRepository
    {
        private readonly string _connectionString;

        public DeviceRepository(AppConnections appConnection) : base(appConnection.DbContext)
        {
            this._connectionString = appConnection.DbContext;
        }

        /// <summary>
        /// Get all devices by username
        /// </summary>
        /// <param name="username">Username to get devices</param>
        /// <returns>A collection of device info</returns>
        public async Task<IEnumerable<Device>> GetAllByUsernameAsync(string username)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(this._connectionString))
                {
                    // Open connection
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    var data = await conn.QueryAsync<Device>(@"SELECT * FROM Devices WHERE Username = @Username", param: new { Username = username });

                    conn.Close();

                    if (data == null)
                    {
                        return Enumerable.Empty<Device>();
                    }

                    return data;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get all devices paging
        /// </summary>
        /// <param name="page">Username to get devices</param>
        /// <param name="limit">Username to get devices</param>
        /// <returns>A collection of device info</returns>
        public async Task<IEnumerable<Device>> GetAllPagingAsync(int page, int limit)
        {
            try
            {
                if (page <= 0)
                {
                    page = 1;
                }

                if (limit < 0)
                {
                    limit = 0;
                }

                using (SqlConnection conn = new SqlConnection(this._connectionString))
                {
                    // Open connection
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    var offset = (page - 1) * limit;

                    var data = await conn.QueryAsync<Device>(
                        @"SELECT * FROM Devices ORDER BY CreatedAt OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY", 
                        param: new { Offset = offset, @Limit = limit });

                    conn.Close();

                    if (data == null)
                    {
                        return Enumerable.Empty<Device>();
                    }

                    return data;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
