using System;
using System.Collections.Generic;
using System.Text;
using AudioBook.Core.Entities;

namespace AudioBook.Core.Entities
{
    public class AudioBook_Authors : BaseEntity
    {
        public int AudioBookId { get; set; }
        public int AuthorId { get; set; }
    }
}
