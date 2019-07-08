using AudioBook.Api.Services.Interfaces;
using AudioBook.Core.DTO.Request;
using AudioBook.Core.DTO.Response;
using AudioBook.Core.Entities;
using AudioBook.Infrastructure.Repositories.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AudioBook.Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryService(ICategoryRepository categoryRepo)
        {
            this._categoryRepo = categoryRepo;
        }

        public async Task Delete(Category category)
        {
            var data = await this._categoryRepo.DeleteAsync(category);
        }

        public async Task<IEnumerable<CategoryDetailResponse>> GetAllPagingAsync(int page = 1, int limit = 10, string search = "")
        {
            var data = await this._categoryRepo.GetAllPagingAsync(page, limit, search);
            var dto = data.Adapt<IEnumerable<CategoryDetailResponse>>();

            return dto;
        }

        public async Task<CategoryDetailResponse> GetById(int id)
        {
            var entity = await this._categoryRepo.GetAsync(id);

            var dto = entity.Adapt<CategoryDetailResponse>();

            return dto;
        }

        public async Task<int> CountAllAsync(string search)
        {
            var total = await this._categoryRepo.CountAllAsync(search);

            return total;
        }

        public async Task<int> InsertAsync(CategoryCreateRequest dto)
        {
            var entity = new Category()
            {
                Name = dto.Name,
                Description = dto.Description,
                CreatedAt = DateTime.Now,
                CreatedBy = "havi"
            };

            return await this._categoryRepo.InsertAsync(entity);
        }

        public async Task Update(CategoryUpdateRequest dto)
        {
            var data = await this._categoryRepo.GetAsync(dto.Id);

            data.Name = dto.Name;
            data.Description = dto.Description;

            await this._categoryRepo.UpdateAsync(data);
        }
    }
}