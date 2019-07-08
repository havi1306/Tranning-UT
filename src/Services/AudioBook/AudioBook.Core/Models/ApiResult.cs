using System;
using System.Collections.Generic;
using System.Text;

namespace AudioBook.Core.Models
{
    public class ApiResult<TEntity>
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
