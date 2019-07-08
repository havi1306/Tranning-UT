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
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepo;

        public AuthorService(IAuthorRepository authorRepo)
        {
            this._authorRepo = authorRepo;
        }

        public async Task Delete(Author author)
        {
            var data = await this._authorRepo.DeleteAsync(author);
        }

        public async Task<IEnumerable<AuthorDetailResponse>> GetAllPagingAsync(int page = 1, int limit = 10, string search = "")
        {
            var data = await this._authorRepo.GetAllPagingAsync(page, limit, search);
            var dto = data.Adapt<IEnumerable<AuthorDetailResponse>>();

            return dto;
        }

        public async Task<AuthorDetailResponse> GetById(int id)
        {
            var entity = await this._authorRepo.GetAsync(id);

            var dto = entity.Adapt<AuthorDetailResponse>();

            return dto;
        }

        public async Task<int> CountAllAsync(string search)
        {
            var total = await this._authorRepo.CountAllAsync(search);

            return total;
        }

        public async Task<int> InsertAsync(AuthorCreateRequest dto)
        {
            var entity = new Author()
            {
               Name = dto.Name,
               Description = dto.Description,
               DateOfBirth = dto.DateOfBirth,
               CreatedAt = DateTime.Now,
               CreatedBy = "Ha Vi"
            };

            return await this._authorRepo.InsertAsync(entity);
        }

        public async Task Update(AuthorUpdateRequest dto)
        {
            var data = await this._authorRepo.GetAsync(dto.Id);

            data.Name = dto.Name;
            data.Description = dto.Description;

            var result = await this._authorRepo.UpdateAsync(data);
        }
    }
}