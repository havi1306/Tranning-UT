using AudioBook.Core.Entities;
using AudioBook.Infrastructure.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Commands.CategoryCreate
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly ICategoryRepository _categoryRepo;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepo)
        {
            this._categoryRepo = categoryRepo;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = new Category()
            {
                Name = request.Name,
                Description = request.Description,
                CreatedAt = DateTime.Now,
                CreatedBy = "havi"
            };

            return await this._categoryRepo.InsertAsync(entity);
        }
    }
}
