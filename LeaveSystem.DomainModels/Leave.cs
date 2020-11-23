using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveSystem.DomainModels
{
    public class Leave
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeaveID { get; set; }
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
        public int LeaveStatus { get; set; }

        public int EmployeeID { get; set; }
       
        public string RequestText { get; set; }

        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }

        
    }
}
