using NetCore.Core.BookAudio.Entities;
using NetCore.Infrastructure;
using NetCore.Infrastructure.Repositories;
using NetCore.Infrastructure.Repositories.Interfaces;

namespace NetCore.Infrastructure.Repositories
{
    public class AudioBookRepository : Repository<AudioBook>, IAudioBookRepository
    {
        private readonly string _connectionString;

        public AudioBookRepository(AppConnections appConnection) : base(appConnection.DbContext)
        {
            this._connectionString = appConnection.DbContext;
        }
    }
}
