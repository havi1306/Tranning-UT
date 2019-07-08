using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using AudioBook.Infrastructure.Repositories.Interfaces;

namespace AudioBook.Infrastructure.Repositories
{
    /// <summary>
    /// Base Repository
    /// </summary>
    /// <typeparam name="TEntity">Generic entity</typeparam>
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// DB connection string
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Construture repo
        /// </summary>
        /// <param name="connectionString">DB connection string</param>
        public Repository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        /// <summary>
        /// Get entity
        /// </summary>
        /// <param name="id">Khóa chính của entity cần query</param>
        /// <returns>Trả về TEntity</returns>
        public virtual TEntity Get(object id)
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

                    var data = conn.Get<TEntity>(id);

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
        /// Async: Hàm dùng get data theo khóa chính
        /// </summary>
        /// <param name="id">Khóa chính của entity cần query</param>
        /// <returns>Trả về TEntity</returns>
        public virtual async Task<TEntity> GetAsync(object id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(this._connectionString))
                {
                    // Open connection
                    if (conn.State == ConnectionState.Closed)
                    {
                        await conn.OpenAsync();
                    }

                    var data = await conn.GetAsync<TEntity>(id);

                    conn.Close();

                    return data;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Hàm dùng Get all data
        /// </summary>
        /// <returns>Trả về TEntity</returns>
        public virtual IEnumerable<TEntity> GetAll()
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

                    var data = conn.GetAll<TEntity>();

                    conn.Close();

                    return data;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Async: Hàm dùng Get all data
        /// </summary>
        /// <returns>Trả về TEntity</returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(this._connectionString))
                {
                    // Open connection
                    if (conn.State == ConnectionState.Closed)
                    {
                        await conn.OpenAsync();
                    }

                    var data = await conn.GetAllAsync<TEntity>();

                    conn.Close();

                    return data;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Generic TEntity</param>
        /// <returns>dynamic object</returns>
        public virtual dynamic Insert(TEntity entity)
        {
            try
            {
                using (var conn = new SqlConnection(this._connectionString))
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    var result = conn.Insert(entity);

                    conn.Close();

                    return result;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Insert async entity
        /// </summary>
        /// <param name="entity">Generic entity</param>
        /// <returns>dynamic object</returns>
        public virtual async Task<dynamic> InsertAsync(TEntity entity)
        {
            try
            {
                using (var conn = new SqlConnection(this._connectionString))
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                    {
                        await conn.OpenAsync();
                    }

                    var result = await conn.InsertAsync(entity);

                    conn.Close();

                    return result;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Generic TEntity</param>
        /// <returns>boolean: update success or not</returns>
        public virtual bool Update(TEntity entity)
        {
            try
            {
                using (var conn = new SqlConnection(this._connectionString))
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    var result = conn.Update(entity);

                    conn.Close();

                    return result;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Update async entity
        /// </summary>
        /// <param name="entity">Generic TEntity</param>
        /// <returns>boolean: update success or not</returns>
        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            try
            {
                using (var conn = new SqlConnection(this._connectionString))
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                    {
                        await conn.OpenAsync();
                    }

                    var result = await conn.UpdateAsync(entity);

                    conn.Close();

                    return result;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Generic TEntity</param>
        /// <returns>Boolean: delete success or not</returns>
        public virtual bool Delete(TEntity entity)
        {
            try
            {
                using (var conn = new SqlConnection(this._connectionString))
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    var result = conn.Delete(entity);

                    conn.Close();

                    return result;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Generic TEntity</param>
        /// <returns>Boolean: delete success or not</returns>
        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            try
            {
                using (var conn = new SqlConnection(this._connectionString))
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                    {
                        await conn.OpenAsync();
                    }

                    var result = await conn.DeleteAsync(entity);

                    conn.Close();

                    return result;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
