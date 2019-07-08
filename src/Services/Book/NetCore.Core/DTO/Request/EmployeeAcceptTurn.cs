using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Core.DTO.Request
{
    public class EmployeeAcceptTurn
    {
        public string CompanyId { get; set; }
        public string EmployeeId { get; set; }
    }
}
