using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveSystem.DomainModels;

namespace LeaveSystem.Repositories
{
    public interface IDepartmentsRepository
    {
        void AddDepartment(Department c);
        List<Department> GetDepartments();
        List<Department> GetDepartmentByDepartmentID(int DepartmentID);

    }
    public class DepartmentsRepository: IDepartmentsRepository
    {
        LeaveSystemDatabaseDbContext db;

        public DepartmentsRepository()
        {
            db = new LeaveSystemDatabaseDbContext();
        }
        public void AddDepartment(Department c)
        {
            db.Departments.Add(c);
            db.SaveChanges();
        }
        public List<Department> GetDepartments()
        {
            List<Department> ct = db.Departments.ToList();
            return ct;
        }
        public List<Department> GetDepartmentByDepartmentID(int DepartmentID)
        {
            List<Department> ct = db.Departments.Where(temp => temp.DepartmentID == DepartmentID).ToList();
            return ct;
        }
    }
}
