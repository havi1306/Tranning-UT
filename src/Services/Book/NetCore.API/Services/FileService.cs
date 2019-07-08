using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NetCore.Core.Models;

namespace NetCore.API.Services
{
    public class FileService: IFileService
    {
        /// <summary>
        /// Add a file
        /// </summary>
        /// <param name="path">Directory path</param>
        /// <param name="file">File upload</param>
        /// <returns>FileView Model</returns>
        public async Task<FileModel> Save(string pathFile, IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    Guid identity = Guid.NewGuid();

                    var fileUploadResult = new FileModel()
                    {
                        Id = identity,
                        OriginalName = file.FileName,
                        Type = file.ContentType,
                        Length = file.Length,
                        Extension = Path.GetExtension(file.FileName),
                        Path = pathFile
                    };

                    using (var stream = new FileStream(pathFile, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    return fileUploadResult;
                }

                return null;
            }
            catch
            {
                throw;
            }
        }
    }
}
