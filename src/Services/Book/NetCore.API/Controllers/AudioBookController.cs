using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCore.API.Filters;
using NetCore.API.Models.Response;
using NetCore.Core.Models;

namespace NetCore.API.Controllers
{
    [Route("api")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AudioBookController : ControllerBase
    {
        // GET api/values
        [HttpGet("category/{categoryId}/audiobooks")]
        [Audit]
        public async Task<ActionResult<PagedData<AudioBookModel>>> Get()
        {
            var result = new PagedData<AudioBookModel>(new List<AudioBookModel>(), 0);

            return this.Ok(result);
        }
    }
}
