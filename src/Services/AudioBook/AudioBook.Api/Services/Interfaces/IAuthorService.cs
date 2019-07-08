using AudioBook.Core.DTO.Request;
using AudioBook.Core.DTO.Response;
using AudioBook.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AudioBook.Api.Services.Interfaces
{
    public interface IAuthorService
    {

        Task<int> CountAllAsync(string search);

        Task<IEnumerable<AuthorDetailResponse>> GetAllPagingAsync(int page, int limit, string search);

        Task<AuthorDetailResponse> GetById(int id);

        Task<int> InsertAsync(AuthorCreateRequest dto);

        Task Update(AuthorUpdateRequest entity);

        Task Delete(Author author);
    }
}