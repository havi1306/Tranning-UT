using Notification.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notification.Core.Entities
{
    public class Device : BaseEntity
    {
        public int Id { get; set; }
        public string StoreId { get; set; }
        public string Username { get; set; }
        public string DeviceToken { get; set; }
        public DeviceType DeviceType { get; set; }
    }
}
