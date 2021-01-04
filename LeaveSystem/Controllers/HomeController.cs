using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeaveSystem.ServiceLayer;
using LeaveSystem.ViewModels;
using LeaveSystem.CustomFilters;

namespace LeaveSystem.Controllers
{
    public class HomeController : Controller
    {
        ILeavesService qs;
        IDepartmentsService cs;
        IRolesService rs;

        public HomeController(ILeavesService qs,IDepartmentsService cs,IRolesService rs)
        {
            this.qs = qs;
            this.cs = cs;
            this.rs = rs;
        }
        
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult About()
        {
            return View();
        }
        
        public ActionResult Contact()
        {
            return View();
        }
        
        [EmployeeAuthorizationFilter]
        public ActionResult Departments()
        {
           List<DepartmentViewModel> departments= this.cs.GetDepartments();
           return View(departments);
        }
        
        [EmployeeAuthorizationFilter]
        public ActionResult Roles()
        {
           List<RoleViewModel> roles = this.rs.GetRoles();
           return View(roles);
        }
        
        public ActionResult InvalidAccess()
        {
           ViewBag.message = "Invalid Access Request";
           return View();
        }
    }
}
