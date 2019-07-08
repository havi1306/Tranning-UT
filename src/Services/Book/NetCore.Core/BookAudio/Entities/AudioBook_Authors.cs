using System;
using System.Collections.Generic;
using System.Text;
using NetCore.Core.Entities;

namespace NetCore.Core.BookAudio.Entities
{
    public class AudioBook_Authors : BaseEntity
    {
        public int AudioBookId { get; set; }
        public int AuthorId { get; set; }
    }
}
