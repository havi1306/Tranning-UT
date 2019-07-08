using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCore.Core.BookAudio.Entities;
using NetCore.Core.Entities;
using NetCore.Infrastructure;
using NetCore.Infrastructure.Repositories.Interfaces;

namespace NetCore.Infrastructure.Repositories.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
    }
}
