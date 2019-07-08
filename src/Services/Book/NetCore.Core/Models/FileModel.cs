using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Core.Models
{
    /// <summary>
    /// Fileview model
    /// </summary>
    public class FileModel
    {
        /// <summary>
        /// Gets or sets File Id
        /// </summary>
        public Guid Id { get; set; }

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
    }
}
