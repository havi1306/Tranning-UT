using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using NetCore.API.Services;
using NetCore.API.Utilities;
using NetCore.Core.Models;

namespace NetCore.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    //[ApiExplorerSettings(IgnoreApi = true)]
    public class FileController : ControllerBase
    {
        private readonly IHostingEnvironment _environment;
        private readonly IFileService _fileService;

        public FileController(IHostingEnvironment environment, IFileService fileService)
        {
            this._environment = environment;
            this._fileService = fileService;
        }

        // GET api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromForm]List<IFormFile> files)
        {
            var moment = DateTime.Now;

            long size = files.Sum(f => f.Length);

            foreach (var file in files)
            {
                //var fileValidated = await FileHelpers.ProcessFormFile(file, ModelState);
            }

            // Perform a second check to catch ProcessFormFile method
            // violations.
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var data = new List<FileModel>();

            var tasks = new List<Task<FileModel>>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    // full path to file in temp location
                    var filePath = Path.Combine(_environment.ContentRootPath, @"Uploads", string.Format("{0}_{1}", moment.ToString("yyyyMMdd_hhmm"), Regex.Replace(formFile.FileName, @"\s+", "")));
                    
                    var task = this._fileService.Save(filePath, formFile);
                    tasks.Add(task);
                }
            }

            var saveResult = await Task.WhenAll(tasks);

            foreach (var fileModel in saveResult)
            {
                if (fileModel != null)
                {
                    data.Add(fileModel);
                }
            }

            var result = new AppResult<List<FileModel>>()
            {
                Message = "Uploaded",
                Data = data
            };

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return Content("filename not present");

            var path = Path.Combine(this._environment.ContentRootPath, @"Uploads", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;

            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
                {".flac", "audio/x-flac"},
            };
        }
    }
}
