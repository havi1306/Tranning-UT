using AudioBook.Core.Entities;
using System.Threading.Tasks;

namespace AudioBook.Infrastructure.Repositories.Interfaces
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
