using AudioBook.Core.Entities;
using AudioBook.Infrastructure;
using AudioBook.Infrastructure.Repositories;
using AudioBook.Infrastructure.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace AudioBook.Infrastructure.Repositories
{
    public class UserRepository : Repository<AppUser>, IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(AppConnections appConnection) : base(appConnection.DbContext)
        {
            this._connectionString = appConnection.DbContext;
        }

        /// <summary>
        /// Kiểm tra username và password
        /// </summary>
        /// <param name="email">Email kiểm tra</param>
        /// <param name="email">Password kiểm tra</param>
        /// <returns>Trả về boolean</returns>
        public async Task<bool> CheckPasswordAsync(string email, string password)
        {
            try
            {
                if (email == "levinhtin@gmail.com" && password == "123123")
                {
                    return await Task.FromResult<bool>(true);
                }
                else
                {
                    return await Task.FromResult<bool>(false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
