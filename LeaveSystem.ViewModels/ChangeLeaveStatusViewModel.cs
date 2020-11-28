using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveSystem.ViewModels
{
    public class ChangeLeaveStatusViewModel //BY SUPERIORS
    {   [Required]
        public int LeaveID { get; set; }
        [Required]
        public int EmployeeID { get; set; }
        public virtual EmployeeViewModel Employee { get; set; }

    }
}
