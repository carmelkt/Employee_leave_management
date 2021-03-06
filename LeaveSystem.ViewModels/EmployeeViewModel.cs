﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveSystem.ViewModels
{
   public class EmployeeViewModel
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Mobile { get; set; }
        public bool IsSpecialPermission { get; set; }
        public string ImageUrl { get; set; }
        public virtual DepartmentViewModel department { get; set; }
        public virtual RoleViewModel role { get; set; }
    }
}
