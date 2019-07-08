using AudioBook.Api.Services.Interfaces;
using AudioBook.Core.DTO.Request;
using AudioBook.Core.DTO.Response;
using AudioBook.Core.Entities;
using AudioBook.Core.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AudioBook.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            this._authorService = authorService;
        }

        [HttpGet("authors/{id}")]
        public async Task<ActionResult<AuthorDetailResponse>> Get(int id)
        {
            var data = await this._authorService.GetById(id);

            if (data == null)
            {
                return this.NoContent();
            }

            var result = new ApiResult<AuthorDetailResponse>()
            {
                Message = "Get success",
                Data = data
            };

            return this.Ok(result);
        }

        [HttpGet("authors")]
        public async Task<ActionResult<PagedData<AuthorDetailResponse>>> Gets(int page = 1, int limit = 10, string search = "")
        {
            if (page <= 0)
            {
                return this.BadRequest();
            }

            var data = await this._authorService.GetAllPagingAsync(page, limit, search);

            var total = await this._authorService.CountAllAsync(search);

            var result = new ApiResult<PagedData<AuthorDetailResponse>>()
            {
                Message = "",
                Data = new PagedData<AuthorDetailResponse>(data, total)
            };

            return this.Ok(result);
        }

        [HttpPost("authors")]
        public async Task<IActionResult> Post([FromBody] AuthorCreateRequest model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                var error = new { Message = "Name is required!" };

                return this.BadRequest(error);
            }

            var id = await this._authorService.InsertAsync(model);

            var result = new ApiResult<int>()
            {
                Message = "Insert success",
                Data = id
            };

            return this.Created("api/authors", result);
        }

        [HttpPut("authors/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AuthorUpdateRequest model)
        {
            var data = await this._authorService.GetById(id);

            if (data == null)
            {
                return BadRequest(new { error = "Data is not exist" });
            }

            await this._authorService.Update(model);

            var result = new ApiResult<AuthorDetailResponse>()
            {
                Message = "update success",
                Data = null
            };

            return Ok(result);
        }

        [HttpDelete("authors/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await this._authorService.GetById(id);
            if (data == null)
            {
                return NotFound();
            }

            await this._authorService.Delete(data.Adapt<Author>());

            var result = new ApiResult<AuthorDetailResponse>()
            {
                Message = "success",
                Data = null
            };

            return Ok(result);
        }
    }
}