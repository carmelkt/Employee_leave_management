using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveSystem.ViewModels
{
    public class LeaveStatusViewModel
    {
        public int LeaveID { get; set;}
        public int LeaveStatus { get; set; }
        public int LeaveStartDate { get; set; }
        public int LeaveEndDate { get; set; }
        public int EmployeeID { get; set; }
        public virtual EmployeeViewModel Employee { get; set; }

    }
}
