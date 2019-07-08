using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AudioBook.Core.Entities
{
    [Table("AudioBookInfo")]
    public class AudioBookInfo : BaseEntity
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
