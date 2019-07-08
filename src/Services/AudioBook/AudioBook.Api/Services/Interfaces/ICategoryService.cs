using AudioBook.Core.DTO.Request;
using AudioBook.Core.DTO.Response;
using AudioBook.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioBook.Api.Services.Interfaces
{
    public interface ICategoryService
    {

        Task<int> CountAllAsync(string search);

        Task<IEnumerable<CategoryDetailResponse>> GetAllPagingAsync(int page, int limit, string search);

        Task<CategoryDetailResponse> GetById(int id);

        Task<int> InsertAsync(CategoryCreateRequest dto);

        Task Update(CategoryUpdateRequest entity);

        Task Delete(Category category);

        
    }
}
