using System.Web.Http;
using Unity;
using Unity.WebApi;
using Unity.Mvc5;
using LeaveSystem.ServiceLayer;
using System.Web.Mvc;


namespace LeaveSystem
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<ILeavesService, LeavesService>();
            container.RegisterType<IEmployeesService, EmployeesService>();
            container.RegisterType<IDepartmentsService, DepartmentsService>();
            container.RegisterType<IRolesService, RolesService>();
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}