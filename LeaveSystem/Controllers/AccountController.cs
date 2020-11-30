using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System.Web.Mvc;
using System.Web.Helpers;
using LeaveSystem.ViewModels;
using LeaveSystem.ServiceLayer;
using LeaveSystem.DomainModels;
using System.Security.Cryptography;
using LeaveSystem.CustomFilters;

namespace LeaveSystem.Controllers
{
    public class AccountController : Controller
    {
        IEmployeesService us;
        IDepartmentsService ds;
        IRolesService rs;
        ILeavesService ls;
        public AccountController(IEmployeesService us,IDepartmentsService ds, IRolesService rs,ILeavesService ls)
        {
            this.us = us;
            this.ds = ds;
            this.rs = rs;
            this.ls = ls;
        }
        [HRManagerAuthorizationFilter]
        public ActionResult Register()
        {
            /*if(ImageUrl!=null)
            {
                string ImageName = System.IO.Path.GetFileName(ImageUrl.FileName);
                string PhysicalPath = Server.MapPath("~/App_Data/Image/" + ImageName);
                ImageUrl.SaveAs(PhysicalPath);
            }*/

            ViewBag.Departments = ds.GetDepartments();
            ViewBag.Roles = rs.GetRoles();
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(RegisterViewModel rvm)
        {
            
            LeaveSystemDatabaseDbContext db = new LeaveSystemDatabaseDbContext();
            /*if (Image != null)
            {
                string ImageName = System.IO.Path.GetFileName(Image.FileName);
                string PhysicalPath = Server.MapPath("~/Images/" + ImageName);
                Image.SaveAs(PhysicalPath);
                rvm.ImageUrl = PhysicalPath;
            }*/
            //string ImageName = rvm.ImageUrl;
            //string PhysicalPath = Server.MapPath("~/App_Data/Image" + ImageName);
            //rvm.ImageUrl = PhysicalPath;
            
                HttpPostedFileBase hpf = Request.Files["Images"] as HttpPostedFileBase;
            if (hpf != null)
            {
                string saveFileName = Path.GetFileName(hpf.FileName);
                string location = (Server.MapPath("~/EmployeePhoto" + @"\" + saveFileName.Replace('+', '_')));
                Request.Files["Images"].SaveAs(location);
                


                string locx = "EmployeePhoto/" + saveFileName;

                {
                    rvm.ImageUrl = locx;
                }
            }
            //if (Request.Files.Count >= 1)
            

           

            //rvm.EmployeeName = rvm.EmployeeName;
            this.us.CreateEmployee(rvm);
            //db.Employees.Add(p);
            //db.SaveChanges();
            
            
            return RedirectToAction("Index", "Home");

           
        }

        public ActionResult Login()
        {
            LoginViewModel lvm = new LoginViewModel();
            return View(lvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                EmployeeViewModel uvm = this.us.GetEmployeesByEmailAndPassword(lvm.Email, lvm.PasswordHash);
                if (uvm != null)
                {
                    Session["CurrentUserID"] = uvm.EmployeeID;
                    Session["CurrentUserName"] = uvm.EmployeeName;
                    Session["CurrentUserEmail"] = uvm.Email;
                    Session["CurrentUserPassword"] = uvm.PasswordHash;
                    Session["CurrentUserIsAdmin"] = uvm.IsSpecialPermission;
                    Session["CurrentUserRoleName"] = uvm.role.RoleName;
                    Session["CurrentEmployee"] = null;
                    Session["CurrentUserPhoto"] = uvm.ImageUrl;
                   

                   
                    {
                        return RedirectToAction("ProfilePage", "Account");
                    }

                }
                else
                {
                    ModelState.AddModelError("x", "Invalid Email / Password");
                    return View(lvm);
                }
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                return View(lvm);
            }

        }
        [EmployeeAuthorizationFilter]
        public ActionResult ProfilePage()
        {
            int eid = Convert.ToInt32(Session["CurrentUserID"]);
            string RoleName = Session["CurrentUserRoleName"].ToString();
            ViewBag.CurrentRole = RoleName;
            EmployeeViewModel emp = this.us.GetEmployees().Where(temp => temp.EmployeeID == eid).FirstOrDefault();
            ViewBag.EMP = emp;
            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        [EmployeeAuthorizationFilter]
        public ActionResult LeaveRequest()
        {
            List<LeaveViewModel> qt = ls.GetLeaves().Where(temp => temp.EmployeeID == Convert.ToInt32(Session["CurrentUserID"])).ToList();
            
            ViewBag.Leaves = qt;
            LeaveViewModel lvm = new LeaveViewModel();
            return View(qt);
        }

        [HttpPost]
        public ActionResult LeaveRequest(LeaveViewModel lvm)
        {
            lvm.EmployeeID= Convert.ToInt32(Session["CurrentUserID"]);
            //EmployeeViewModel evm = this.us.GetEmployeesByEmployeeID(lvm.EmployeeID);
            this.ls.LeaveRequest(lvm);


            return RedirectToAction("LeaveRequest", "Account");
        }

        [SpecialHRAuthorizationFilter]
        public ActionResult UpdateLeave()
        {
           
            List<LeaveViewModel> AllLeaves = ls.GetLeaves();
            ViewBag.AllLeaves = AllLeaves;
            return View(AllLeaves);
        }
        [HttpPost]
        public ActionResult UpdateLeave(LeaveStatusViewModel lsvm)
        {
            // lsvm.LeaveID = Convert.ToInt32(Request["LeaveID"]); 
            int eid = lsvm.EmployeeID;
            EmployeeViewModel evm = this.us.GetEmployees().Where(temp => temp.EmployeeID == eid).FirstOrDefault();
            string recepient = evm.Email;
            int mailstatus = lsvm.LeaveStatus;

            string subject = "Leave Status Update";
            if (mailstatus == 1)
            {
                string body = "Your leave request has been approved, congratulations";
                WebMail.Send(recepient, subject, body, null, null, null, true, null, null, null, null, null, null);
            }
            else if (mailstatus == 2)
            {
                string body = "Your leave request has been rejected, sorry for the decision";
                WebMail.Send(recepient, subject, body, null, null, null, true, null, null, null, null, null, null);
            }



            this.ls.UpdateLeaveStatus(lsvm.LeaveID, lsvm.LeaveStatus);
            return RedirectToAction("UpdateLeave", "Account");
        }
        [EmployeeAuthorizationFilter]
        public ActionResult EmployeeSearch(string search="")
        {
            ViewBag.search = search;

            List<EmployeeViewModel> Employe = this.us.GetEmployees().Where(temp => temp.EmployeeName != null && temp.EmployeeName.ToLower().Contains(search.ToLower())).ToList();
            ViewBag.Employe = Employe;
            
            return View(Employe);
        }
        [EmployeeAuthorizationFilter]
        public ActionResult EmployeeSearchByRoles(int RoleID=0)
        {
            List<EmployeeViewModel> SearchRoles = this.us.GetEmployees().Where(temp => temp.role.RoleID == RoleID).ToList();
            ViewBag.Roles = rs.GetRoles();
            ViewBag.SearchRoles = SearchRoles;
            return View(SearchRoles);

        }
 
        public ActionResult UpdateEmployee(int? id)
        { int tempx = Convert.ToInt32(Session["CurrentUserID"]);
            if (Session["CurrentUserRoleName"].ToString() == "HR Manager" || (tempx == id && id != null))
            { //int uid = Convert.ToInt32(Session["CurrentUserID"]);
              //EmployeeViewModel uvm=this.us.GetEmployeesByEmployeeID(uid);
              //UpdateEmployeeViewModel uevm = new UpdateEmployeeViewModel() { EmployeeName = uvm.EmployeeName, Email = uvm.Email, Mobile = uvm.Mobile, EmployeeID=uvm.EmployeeID };
                EmployeeViewModel emp = this.us.GetEmployees().Where(temp => temp.EmployeeID != 0 && temp.EmployeeID == id).FirstOrDefault();

                ViewBag.Departments = ds.GetDepartments();
                ViewBag.Roles = rs.GetRoles();
                if (id != 0)
                {
                    ViewBag.empid = id;
                }
                ViewBag.emp = emp;
                return View(emp);
            }
            else { 
                return RedirectToAction("InvalidAccess", "Home");
            }
        }
        [HttpPost]
        public ActionResult UpdateEmployee(UpdateEmployeeViewModel uevm)
        {
            
            
            
            
            this.us.UpdateEmployee(uevm.EmployeeID, uevm.EmployeeName, uevm.Mobile);
            int CurrentEID = Convert.ToInt32(Session["CurrentUserID"]);
            if(CurrentEID==uevm.EmployeeID)
            {
                return RedirectToAction("ProfilePage", "Account");
            }
            return RedirectToAction("EmployeeSearch", "Account");
        }

    }
}