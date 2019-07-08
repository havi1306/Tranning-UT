using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using NetCore.Core.Entities;

namespace NetCore.Core.BookAudio.Entities
{
    [Table("AudioBook")]
    public class AudioBook : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageBackground { get; set; }
        public int Views { get; set; }
        public int Rate { get; set; }
        public int Duration { get; set; }
    }
}
