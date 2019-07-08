using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Queries.CategoryDetail
{
    public class CategoryDetailDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
