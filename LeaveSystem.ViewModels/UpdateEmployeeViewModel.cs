using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveSystem.ViewModels
{
    public class UpdateEmployeeViewModel
    {
        public int EmployeeID { get; set; }
        [Required]
        [RegularExpression(@"(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string EmployeeName { get; set; }
        [Required]
        public string Mobile { get; set; }
        public string ImageUrl { get; set; }
        public virtual DepartmentViewModel Department { get; set; }
        public virtual RoleViewModel Role { get; set; }
        public virtual EmployeeViewModel Employee { get; set; }
    }
}
