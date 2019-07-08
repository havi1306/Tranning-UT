using File.Api.Services.Interfaces;
using File.Core.DTO.Response;
using File.Infrastructure.Repositories.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace File.Api.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepo;

        public FileService(IFileRepository fileRepo)
        {
            this._fileRepo = fileRepo;
        }

        /// <summary>
        /// Add a file
        /// </summary>
        /// <param name="rootPath">Root directory path</param>
        /// <param name="path">Directory path</param>
        /// <param name="file">File upload</param>
        /// <returns>FileView Model</returns>
        public async Task<FileDetailResponse> Save(string rootPath, string filePath, IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    using (var stream = new FileStream(Path.Combine(rootPath, filePath), FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var fileEntity = new File.Core.Entities.FileInfo()
                    {
                        OriginalName = file.FileName,
                        Type = file.ContentType,
                        Length = file.Length,
                        Extension = Path.GetExtension(file.FileName),
                        Path = filePath,
                        IsDeleted = false,
                        IsVerified = false,
                        CreatedAt = DateTime.Now,
                        CreatedBy = "tinlvv"
                    };

                    await this._fileRepo.InsertAsync(fileEntity);

                    return fileEntity.Adapt<FileDetailResponse>();
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
