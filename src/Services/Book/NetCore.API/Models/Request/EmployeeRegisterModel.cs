using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.API.Models.Request
{
    /// <summary>
    /// Employee register model
    /// </summary>
    public class EmployeeRegisterModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string CompanyId { get; set; }
        public bool IsActived { get; set; }
    }
}
