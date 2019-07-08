using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NetCore.Core.Models;

namespace NetCore.API.Services
{
    public interface IFileService
    {
        Task<FileModel> Save(string path, IFormFile file);
    }
}
