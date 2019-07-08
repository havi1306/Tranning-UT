using AudioBook.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioBook.Infrastructure.Repositories.Interfaces
{
     public interface IAudioBookRepository : IRepository<AudioBookInfo>
    {
    }
}
