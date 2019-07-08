using AudioBook.Infrastructure.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Commands.CategoryUpdate
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly ICategoryRepository _categoryRepo;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepo)
        {
            this._categoryRepo = categoryRepo;
        }

        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var data = await this._categoryRepo.GetAsync(request.Id);

            data.Name = request.Name;
            data.Description = request.Description;

            return await this._categoryRepo.UpdateAsync(data);
        }
    }
}
