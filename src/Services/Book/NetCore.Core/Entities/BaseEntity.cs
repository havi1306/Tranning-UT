using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Core.Entities
{
    public abstract class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int? ModifiedBy{ get; set; }
    }
}
