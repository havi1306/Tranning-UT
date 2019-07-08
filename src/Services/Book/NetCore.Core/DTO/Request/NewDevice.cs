using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using NetCore.Core.Enumerations;

namespace NetCore.Core.DTO.Request
{
    /// <summary>
    /// Register new device request model
    /// </summary>
    public class NewDevice
    {
        /// <summary>
        /// Store Id
        /// </summary>
        [Required]
        public string StoreId { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Device token
        /// </summary>
        [Required]
        public string DeviceToken { get; set; }

        /// <summary>
        /// Device type: 1: Android, 2: IOS
        /// </summary>
        public DeviceType DeviceType { get; set; }
    }
}
