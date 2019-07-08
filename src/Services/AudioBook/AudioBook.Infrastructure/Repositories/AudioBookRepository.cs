using AudioBook.Core.Entities;
using AudioBook.Infrastructure;
using AudioBook.Infrastructure.Repositories;
using AudioBook.Infrastructure.Repositories.Interfaces;

namespace AudioBook.Infrastructure.Repositories
{
    public class AudioBookRepository : Repository<AudioBookInfo>, IAudioBookRepository
    {
        private readonly string _connectionString;

        public AudioBookRepository(AppConnections appConnection) : base(appConnection.DbContext)
        {
            this._connectionString = appConnection.DbContext;
        }
    }
}
