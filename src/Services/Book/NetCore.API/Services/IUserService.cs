using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCore.API.Models.Request;

namespace NetCore.API.Services
{
    public interface IUserService
    {
        int CreateEmployee(EmployeeRegisterModel model);
    }
}
