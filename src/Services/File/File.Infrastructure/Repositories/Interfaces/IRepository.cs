using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace File.Infrastructure.Repositories.Interfaces
{
    /// <summary>
    /// Base interface repository
    /// </summary>
    /// <typeparam name="TEntity">Generic entity</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get entity
        /// </summary>
        /// <param name="id">Khóa chính của entity cần query</param>
        /// <returns>Trả về TEntity</returns>
        TEntity Get(object id);

        /// <summary>
        /// Async: Hàm dùng get data theo khóa chính
        /// </summary>
        /// <param name="id">Khóa chính của entity cần query</param>
        /// <returns>Trả về TEntity</returns>
        Task<TEntity> GetAsync(object id);

        /// <summary>
        /// Hàm dùng Get all data
        /// </summary>
        /// <returns>Trả về TEntity</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Async: Hàm dùng Get all data
        /// </summary>
        /// <returns>Trả về TEntity</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Generic entity</param>
        /// <returns>dynamic object</returns>
        dynamic Insert(TEntity entity);

        /// <summary>
        /// Insert async entity
        /// </summary>
        /// <param name="entity">Generic entity</param>
        /// <returns>dynamic object</returns>
        Task<dynamic> InsertAsync(TEntity entity);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Generic entity</param>
        /// <returns>boolean: update success or not</returns>
        bool Update(TEntity entity);

        /// <summary>
        /// Update async entity
        /// </summary>
        /// <param name="entity">Generic entity</param>
        /// <returns>boolean: update success or not</returns>
        Task<bool> UpdateAsync(TEntity entity);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Generic entity</param>
        /// <returns>Boolean: delete success or not</returns>
        bool Delete(TEntity entity);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Generic entity</param>
        /// <returns>Boolean: delete success or not</returns>
        Task<bool> DeleteAsync(TEntity entity);
    }
}
