using AudioBook.Core.Entities;
using AudioBook.Infrastructure.Repositories.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AudioBook.Infrastructure.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        private readonly string _connectionString;

        public AuthorRepository(AppConnections appConnection) : base(appConnection.DbContext)
        {
            this._connectionString = appConnection.DbContext;
        }

        /// <summary>
        /// Count all Author paging
        /// </summary>
        /// <param name="search">Username to get Author</param>
        /// <returns>A collection of Author info</returns>
        public async Task<int> CountAllAsync(string search)
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

                    var data = await conn.QueryFirstOrDefaultAsync<int>(
                        @"SELECT COUNT(*) FROM Author WHERE Name LIKE N'%@Search%'",
                        param: new { @Search = search });

                    conn.Close();

                    return data;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get all Author paging
        /// </summary>
        /// <param name="page">Username to get Author</param>
        /// <param name="limit">Username to get Author</param>
        /// <param name="search">Username to get Author</param>
        /// <returns>A collection of Author info</returns>
        public async Task<IEnumerable<Author>> GetAllPagingAsync(int page, int limit, string search)
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

                    var data = await conn.QueryAsync<Author>(
                        @"SELECT * FROM Author WHERE Name LIKE N'%@Search%' ORDER BY CreatedAt OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY",
                        param: new { Offset = offset, @Limit = limit, @Search = search });

                    conn.Close();

                    if (data == null)
                    {
                        return Enumerable.Empty<Author>();
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