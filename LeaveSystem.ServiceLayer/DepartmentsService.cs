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
    public interface IDepartmentsService
    {
        void AddDepartment(DepartmentViewModel cvm);
        List<DepartmentViewModel> GetDepartments();
        DepartmentViewModel GetDepartmentByDepartmentID(int DepartmentID);
    }
    public class DepartmentsService:IDepartmentsService
    {
        IDepartmentsRepository cr;
        public DepartmentsService()
        {
            cr = new DepartmentsRepository();
        }

        public void AddDepartment(DepartmentViewModel cvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<DepartmentViewModel, Department>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Department c = mapper.Map<DepartmentViewModel, Department>(cvm);
            cr.AddDepartment(c);
        }

        public List<DepartmentViewModel> GetDepartments()
        {
            List<Department> c = cr.GetDepartments();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Department, DepartmentViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<DepartmentViewModel> cvm = mapper.Map<List<Department>, List<DepartmentViewModel>>(c);
            return cvm;
        }

        public DepartmentViewModel GetDepartmentByDepartmentID(int DepartmentID)
        {
            Department c = cr.GetDepartmentByDepartmentID(DepartmentID).FirstOrDefault();
            DepartmentViewModel cvm = null;
            if (c != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Department, DepartmentViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                cvm = mapper.Map<Department, DepartmentViewModel>(c);
            }
            return cvm;
        }
    }
}
