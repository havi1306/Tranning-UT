using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCore.API.Models.Request;

namespace NetCore.API.Services
{
    public class EmployeeService : IUserService
    {
        public int CreateEmployee(EmployeeRegisterModel model)
        {
            return 1;
        }
    }
}
