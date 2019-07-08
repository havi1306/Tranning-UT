using AudioBook.Core.Models;
using AudioBook.Infrastructure.Repositories.Interfaces;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Queries.CategoryPaging
{
    public class CategoryPagingQueryHandler : IRequestHandler<CategoryPagingQuery, PagedData<CategoryPagingDTO>>
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryPagingQueryHandler(ICategoryRepository categoryRepo)
        {
            this._categoryRepo = categoryRepo;
        }

        public async Task<PagedData<CategoryPagingDTO>> Handle(CategoryPagingQuery request, CancellationToken cancellationToken)
        {
            var data = await this._categoryRepo.GetAllPagingAsync(request.Page, request.Limit, request.Search);
            var dataDTO = data.Adapt<IEnumerable<CategoryPagingDTO>>();

            var total = await this._categoryRepo.CountAllAsync(request.Search);

            var result = new PagedData<CategoryPagingDTO>(dataDTO, total);

            return result;
        }
    }
}
