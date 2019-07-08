using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NetCore.Core.DTO.Request
{
    public class PushNotificationRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Message { get; set; }

        public string Type { get; set; }
    }
}
