using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveSystem.ViewModels
{
   public class RegisterViewModel
    {
        [Required]
        [RegularExpression(@"(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})")]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        [Compare("PasswordHash")]
        public string ConfirmPassword { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string EmployeeName { get; set; }

        [Required]
        public string Mobile { get; set; }
        public string RoleID { get; set; }
        public string DepartmentID { get; set; }
        public string ImageUrl { get; set; }
        public virtual DepartmentViewModel Department { get; set; }
        public virtual RoleViewModel Role { get; set; }
    }
}
