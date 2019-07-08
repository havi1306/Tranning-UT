using NetCore.Core.Entities;
using NetCore.Infrastructure.Repositories.Interfaces;
using System.Threading.Tasks;

namespace NetCore.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<AppUser>
    {
        /// <summary>
        /// Kiểm tra username và password
        /// </summary>
        /// <param name="email">Email kiểm tra</param>
        /// <param name="email">Password kiểm tra</param>
        /// <returns>Trả về boolean</returns>
        Task<bool> CheckPasswordAsync(string email, string password);
    }
}
