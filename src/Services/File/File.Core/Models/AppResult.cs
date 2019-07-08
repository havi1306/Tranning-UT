using System;
using System.Collections.Generic;
using System.Text;

namespace File.Core.Models
{
    public class AppResult<TEntity>
    {
        /// <summary>
        /// Thông báo
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Kết quả
        /// </summary>
        public TEntity Data { get; set; }
    }
}
