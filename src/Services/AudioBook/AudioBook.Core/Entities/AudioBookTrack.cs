using System;
using System.Collections.Generic;
using System.Text;

namespace AudioBook.Core.Entities
{
    public class AudioBookTrack : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PathFile { get; set; }
        public int Duration { get; set; }
        public int AudioBookId { get; set; }
        public int ReaderId { get; set; }
    }
}
