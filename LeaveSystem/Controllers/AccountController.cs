using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System.Web.Mvc;
using LeaveSystem.ViewModels;
using LeaveSystem.ServiceLayer;
using LeaveSystem.DomainModels;
using System.Security.Cryptography;

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
        public ActionResult Register()
        {
            ViewBag.Departments = ds.GetDepartments();
            ViewBag.Roles = rs.GetRoles();
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(RegisterViewModel rvm)
        {
            LeaveSystemDatabaseDbContext db = new LeaveSystemDatabaseDbContext();

            //if (Request.Files.Count >= 1)
            if (Request.Files.Count >= 1)
            {
                var file = Request.Files[0];
                var imgBytes = new Byte[0];

                try
                {
                    imgBytes = new Byte[file.ContentLength];
                    file.InputStream.Read(imgBytes, 0, file.ContentLength);
                }
                catch (Exception)
                {
                    imgBytes = new Byte[file.ContentLength - 1];
                    file.InputStream.Read(imgBytes, 0, file.ContentLength);
                }
                var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                rvm.ImageUrl = base64String;
            }

            /*string GenerateHash(string inputData)
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(inputData));
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
            }*/

            //rvm.EmployeeName = rvm.EmployeeName;
            this.us.CreateEmployee(rvm);
            //db.Employees.Add(p);
            //db.SaveChanges();
            
            
            return RedirectToAction("Index", "Home");

           // us.CreateEmployee(rvm);
            //return RedirectToAction("Index", "Home");
            // if (ModelState.IsValid)
            /* {

                 int uid = this.us.CreateEmployee(rvm);
                 Session["CurrentUserID"] = uid;
                 Session["CurrentUserName"] = rvm.EmployeeName;
                 Session["CurrentUserEmail"] = rvm.Email;
                 Session["CurrentUserPassword"] = rvm.Password;
                 Session["CurrentUserIsAdmin"] = false;
                 Session["CurrentUserDepartmentID"] = rvm.Department.DepartmentID;
                 if (Request.Files.Count >= 1)
                 {
                     var file = Request.Files[0];
                     var imgBytes = new Byte[file.ContentLength];
                     file.InputStream.Read(imgBytes, 0, file.ContentLength);
                     var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                     rvm.Photo = base64String;
                 }
                 Session["CurrentUserPhoto"] = rvm.Photo;
                 return RedirectToAction("Index", "Home");
             }
             //else
             {
                // ModelState.AddModelError("x", "Invalid Information");
                 // remove later
                 //return View();
             }*/
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
                    Session["CurrentUserIsAdmin"] = uvm.IsManager;
                    Session["CurrentUserRoleName"] = uvm.role.RoleName;
                    if (uvm.IsManager)
                    {
                        return RedirectToRoute(new { controller = "Home", action = "Index" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
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
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LeaveRequest()
        {
            LeaveViewModel lvm = new LeaveViewModel();
            return View(lvm);
        }

        [HttpPost]
        public ActionResult LeaveRequest(LeaveViewModel lvm)
        {
            //lvm.EmployeeID= Convert.ToInt32(Session["CurrentUserID"]);
            //EmployeeViewModel evm = this.us.GetEmployeesByEmployeeID(lvm.EmployeeID);
            this.ls.LeaveRequest(lvm);


            return RedirectToAction("Index", "Home");
        }
    }
}