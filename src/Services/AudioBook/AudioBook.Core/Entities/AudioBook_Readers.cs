using System;
using System.Collections.Generic;
using System.Text;

namespace AudioBook.Core.Entities
{
    public class AudioBook_Readers : BaseEntity
    {
        public int AudioBookId { get; set; }
        public int ReaderId { get; set; }
    }
}
