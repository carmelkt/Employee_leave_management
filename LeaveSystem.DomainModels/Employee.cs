using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LeaveSystem.DomainModels
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeID { get; set; }
        public int RoleID { get; set; }
        public int DepartmentID { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Mobile { get; set; }
        public bool IsManager { get; set; }
        public string ImageUrl { get; set; }

        [ForeignKey("RoleID")]
        public virtual Role Role { get; set; }
        [ForeignKey("DepartmentID")]
        public virtual Department Department { get; set; }

    }
}
