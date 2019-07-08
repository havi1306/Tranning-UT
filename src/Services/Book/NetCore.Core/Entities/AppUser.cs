using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Core.Entities
{
    public class AppUser : BaseEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string NormalizeUserName { get; set; }
        public string PasswordHash { get; set; }
    }
}
