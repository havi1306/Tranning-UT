using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Core.DTO.Response
{
    public class TurnResponse
    {
        public string CustomerName { get; set; }
        public string CustomerService { get; set; }
        public bool IsWorkingAppointment { get; set; }
        public DateTime TimeBeginWorking { get; set; }
    }
}
