using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveSystem.ViewModels
{
    public class LeaveViewModel
    {
       
        public string EmployeeName { get; set; } //added later
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
        public int EmployeeID { get; set; }
        public int DepartmentID { get; set; }
      
        public string RequestText { get; set; }// check anagin
        public EmployeeViewModel Employee { get; set; }
        public DepartmentViewModel Department { get; set; }
        public virtual LeaveStatusViewModel LeaveStatus { get; set; }
    }
}
