using AudioBook.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Queries.CategoryPaging
{
    public class CategoryPagingQuery : IRequest<PagedData<CategoryPagingDTO>>
    {
        [FromQuery(Name = "search")]
        public string Search { get; set; }

        [FromQuery(Name = "page")]
        public int Page { get; set; } = 1;

        [FromQuery(Name = "limit")]
        public int Limit { get; set; } = 10;
    }
}
