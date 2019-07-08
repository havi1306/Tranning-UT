using AudioBook.Api.Application.Queries.CategoryDetail;
using AudioBook.Infrastructure.Repositories.Interfaces;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Queries.Category
{
    public class CategoryDetailQueryHandler : IRequestHandler<CategoryDetailQuery, CategoryDetailDTO>
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryDetailQueryHandler(ICategoryRepository categoryRepo)
        {
            this._categoryRepo = categoryRepo;
        }

        public async Task<CategoryDetailDTO> Handle(CategoryDetailQuery request, CancellationToken cancellationToken)
        {
            var item = await this._categoryRepo.GetAsync(request.Id);

            var model = item.Adapt<CategoryDetailDTO>();

            return model;
        }
    }
}
