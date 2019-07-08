using AudioBook.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AudioBook.Infrastructure.Repositories.Interfaces
{
    public interface IAuthorRepository : IRepository<Author>
    {
        /// <summary>
        /// Count all Author paging
        /// </summary>
        /// <param name="search">Username to get Author</param>
        /// <returns>A collection of Author info</returns>
        Task<int> CountAllAsync(string search);

        /// <summary>
        /// Get all Author paging
        /// </summary>
        /// <param name="page">Username to get Author</param>
        /// <param name="limit">Username to get Author</param>
        /// <param name="search">Username to get Author</param>
        /// <returns>A collection of Author info</returns>
        Task<IEnumerable<Author>> GetAllPagingAsync(int page, int limit, string search);
    }
}