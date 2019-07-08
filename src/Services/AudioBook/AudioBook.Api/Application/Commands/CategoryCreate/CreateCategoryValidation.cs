using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Commands.CategoryCreate
{
    public class CreateCategoryValidation : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidation()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}
