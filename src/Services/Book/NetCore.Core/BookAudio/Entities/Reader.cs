using System;
using System.Collections.Generic;
using System.Text;
using NetCore.Core.Entities;

namespace NetCore.Core.BookAudio.Entities
{
    public class Reader : BaseEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
    }
}
