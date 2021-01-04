using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveSystem.DomainModels;
using LeaveSystem.ViewModels;
using LeaveSystem.Repositories;
using AutoMapper;
using AutoMapper.Configuration;

namespace LeaveSystem.ServiceLayer
{
    public interface IRolesService
    {
        void AddRole(RoleViewModel cvm);
        List<RoleViewModel> GetRoles();
        RoleViewModel GetRoleByRoleID(int RoleID);
    }
    public class RolesService : IRolesService
    {
        IRolesRepository cr;
        public RolesService()
        {
            cr = new RolesRepository();
        }

        public void AddRole(RoleViewModel cvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<RoleViewModel, Role>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Role c = mapper.Map<RoleViewModel, Role>(cvm);
            cr.AddRole(c);
        }

        public List<RoleViewModel> GetRoles()
        {
            List<Role> c = cr.GetRoles();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Role, RoleViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<RoleViewModel> cvm = mapper.Map<List<Role>, List<RoleViewModel>>(c);
            return cvm;
        }

        public RoleViewModel GetRoleByRoleID(int RoleID)
        {
            Role c = cr.GetRoleByRoleID(RoleID).FirstOrDefault();
            RoleViewModel cvm = null;
            if (c != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Role, RoleViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                cvm = mapper.Map<Role, RoleViewModel>(c);
            }
            return cvm;
        }
    }
}

