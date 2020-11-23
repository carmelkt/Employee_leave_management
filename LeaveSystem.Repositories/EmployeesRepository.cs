using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveSystem.DomainModels;

namespace LeaveSystem.Repositories
{
    public interface IEmployeesRepository
    {
        void CreateEmployee(Employee u);
        List<Employee> GetEmployees();
        List<Employee> GetEmployeesByEmail(string Email);
        List<Employee> GetEmployeesByEmailAndPassword(string Email, string Password);
        List<Employee> GetEmployeesByEmployeeID(int EmployeeID);
        int GetLatestEmployeeID();
    }

    public class EmployeesRepository: IEmployeesRepository
    {
        LeaveSystemDatabaseDbContext db;
        public EmployeesRepository()
        {
            db = new LeaveSystemDatabaseDbContext();
        }
        public void CreateEmployee(Employee u)
        {
            db.Employees.Add(u);
            db.SaveChanges();
        }
        public List<Employee> GetEmployees()
        {
            List<Employee> us = db.Employees.Where(temp => temp.IsManager == false).OrderBy(temp => temp.EmployeeName).ToList();
            return us;
        }
        public List<Employee> GetEmployeesByEmail(string Email)
        {
            List<Employee> us = db.Employees.Where(temp => temp.Email == Email).ToList();
            return us;
        }
        public List<Employee> GetEmployeesByEmailAndPassword(string Email, string Password)
        {
            List<Employee> us = db.Employees.Where(temp => temp.Email == Email&&temp.PasswordHash==Password).ToList();
            return us;
        }
        public List<Employee> GetEmployeesByEmployeeID(int EmployeeID)
        {
            List<Employee> us = db.Employees.Where(temp => temp.EmployeeID==EmployeeID).ToList();
            return us;

        }
        public int GetLatestEmployeeID()
        {
            int uid = db.Employees.Select(temp => temp.EmployeeID).Max();
            return uid;

        }

    }
}
