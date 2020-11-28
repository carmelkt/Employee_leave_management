using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveSystem.DomainModels;

namespace LeaveSystem.Repositories
{
    public interface IRolesRepository
    {
        void AddRole(Role c);
        List<Role> GetRoles();
        List<Role> GetRoleByRoleID(int RoleID);

    }
    public class RolesRepository : IRolesRepository
    {
        LeaveSystemDatabaseDbContext db;

        public RolesRepository()
        {
            db = new LeaveSystemDatabaseDbContext();
        }
        public void AddRole(Role c)
        {
            db.Roles.Add(c);
            db.SaveChanges();
        }
        public List<Role> GetRoles()
        {
            List<Role> ct = db.Roles.ToList();
            return ct;
        }
        public List<Role> GetRoleByRoleID(int RoleID)
        {
            List<Role> ct = db.Roles.Where(temp => temp.RoleID == RoleID).ToList();
            return ct;
        }


    }
}
