using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace File.Core.Entities
{
    /// <summary>
    /// File entity
    /// </summary>
    [Table("FileInfo")]
    public class FileInfo
    {
        /// <summary>
        /// Gets or sets File Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets File Name
        /// </summary>
        public string OriginalName { get; set; }

        /// <summary>
        /// Gets or sets File Type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets File Length
        /// </summary>
        public long Length { get; set; }

        /// <summary>
        /// Gets or sets File Extension
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// Gets or sets Relative path
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Time created file
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// File created by username
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// File is deleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// File is verified
        /// </summary>
        public bool IsVerified { get; set; }
    }
}
