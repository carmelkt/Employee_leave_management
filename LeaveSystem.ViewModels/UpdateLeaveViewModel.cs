using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveSystem.ViewModels
{
    public class UpdateLeaveViewModel  //BY EMPLOYEE
    {
        public int LeaveID { get; set; }
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
        public virtual EmployeeViewModel Employee { get; set; }
        public string RequestText { get; set; }// check anagin

    }
}
