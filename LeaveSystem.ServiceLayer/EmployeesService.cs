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
using StackOverflowProject.ServiceLayer;

namespace LeaveSystem.ServiceLayer
{
    public interface IEmployeesService
    {
        int CreateEmployee(RegisterViewModel uvm);
        void UpdateEmployee(int eid, string EmployeeName, string Mobile);
        List<EmployeeViewModel> GetEmployees();
        EmployeeViewModel GetEmployeesByEmail(string Email);
        EmployeeViewModel GetEmployeesByEmailAndPassword(string Email, string Password);
        EmployeeViewModel GetEmployeesByEmployeeID(int EmployeeID);
    }
    public class EmployeesService: IEmployeesService
    {
        IEmployeesRepository ur;

        public EmployeesService()
        {
            ur = new EmployeesRepository();
        }

        public int CreateEmployee(RegisterViewModel uvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<RegisterViewModel, Employee>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Employee u = mapper.Map<RegisterViewModel, Employee>(uvm);
            u.PasswordHash = SHA256HashGenerator.GenerateHash(uvm.PasswordHash);
            
            ur.CreateEmployee(u);
            int uid = ur.GetLatestEmployeeID();
            return uid;
        }

        public void UpdateEmployee(int eid, string EmployeeName,string Mobile)
        {
            ur.UpdateEmployee(eid, EmployeeName,Mobile);
        }
        public List<EmployeeViewModel> GetEmployees()
        {
            List<Employee> u = ur.GetEmployees();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, EmployeeViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<EmployeeViewModel> uvm = mapper.Map<List<Employee>, List<EmployeeViewModel>>(u);
            return uvm;

        }
        public EmployeeViewModel GetEmployeesByEmailAndPassword(string Email, string Password)
        {
            Employee u = ur.GetEmployeesByEmailAndPassword(Email, SHA256HashGenerator.GenerateHash(Password)).FirstOrDefault();
            EmployeeViewModel uvm = null;
            if (u != null) //MIGRATE ONLY IF THERE IS ATLEAST ONE MATCHIG RECORD
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, EmployeeViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                uvm = mapper.Map<Employee, EmployeeViewModel>(u);
            }
            return uvm;
        }

       public EmployeeViewModel GetEmployeesByEmail(string Email)
        {
            Employee u = ur.GetEmployeesByEmail(Email).FirstOrDefault();
            EmployeeViewModel uvm = null;
            if (u != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, EmployeeViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                uvm = mapper.Map<Employee, EmployeeViewModel>(u);
            }
            return uvm;
        }
        public EmployeeViewModel GetEmployeesByEmployeeID(int EmployeeID)
        {
            Employee u = ur.GetEmployeesByEmployeeID(EmployeeID).FirstOrDefault();
            EmployeeViewModel uvm = null;
            if (u != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, EmployeeViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                uvm = mapper.Map<Employee, EmployeeViewModel>(u);
            }
            return uvm;
        }

    }
}
