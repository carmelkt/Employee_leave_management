using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveSystem.ViewModels
{
    public class RequestLeaveViewModel
    {
        [Required]
        public DateTime LeaveStartDate { get; set; }
        [Required] 
        public DateTime LeaveEndDate { get; set; }
        public string RequestText { get; set; }// check anagin
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        public int DepartmentID { get; set; }
    }
}
