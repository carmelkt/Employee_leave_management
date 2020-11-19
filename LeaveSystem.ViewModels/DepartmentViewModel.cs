using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveSystem.ViewModels
{
    public class DepartmentViewModel
    {
        [Required]
        public int DepartmentID { get; set;}
        [Required]
        public string DepartmentName { get; set; }

    }
}
