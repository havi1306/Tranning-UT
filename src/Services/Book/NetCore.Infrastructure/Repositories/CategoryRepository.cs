using NetCore.Core.BookAudio.Entities;
using NetCore.Infrastructure;
using NetCore.Infrastructure.Repositories.Interfaces;

namespace NetCore.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly string _connectionString;

        public CategoryRepository(AppConnections appConnection) : base(appConnection.DbContext)
        {
            this._connectionString = appConnection.DbContext;
        }
    }
}
