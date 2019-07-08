using System;
using System.Collections.Generic;
using System.Text;
using NetCore.Core.Entities;

namespace NetCore.Core.BookAudio.Entities
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
