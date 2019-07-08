using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Queries.CategoryDetail
{
    public class CategoryDetailQuery : IRequest<CategoryDetailDTO>
    {
        public int Id { get; set; }
    }
}
