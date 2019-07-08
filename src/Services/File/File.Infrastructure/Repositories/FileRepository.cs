using File.Core.Entities;
using File.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace File.Infrastructure.Repositories
{
    public class FileRepository : Repository<FileInfo>, IFileRepository
    {
        private readonly string _connectionString;

        public FileRepository(AppConnections appConnection) : base(appConnection.DbContext)
        {
            this._connectionString = appConnection.DbContext;
        }
    }
}
