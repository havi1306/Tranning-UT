using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace AudioBook.Api.Criterias
{
    public class GetAllAudioBookFilter
    {
        [FromQuery(Name = "page")]
        public int Page { get; set; } = 1;

        [FromQuery(Name = "limit")]
        public int Limit { get; set; } = 10;

        [FromQuery(Name = "search")]
        public string Search { get; set; }

        [FromQuery(Name = "category_id")]
        public int? CategoryId { get; set; }
    }
}
