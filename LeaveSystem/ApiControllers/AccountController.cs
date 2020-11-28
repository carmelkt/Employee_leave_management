using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LeaveSystem.ServiceLayer;
using LeaveSystem.ViewModels;


namespace LeaveSystem.ApiControllers
{
        public class AccountController : ApiController
        {
            IEmployeesService us;

            public AccountController(IEmployeesService us)
            {
                this.us = us;
            }

            public string Get(string Email)
            {
                if (this.us.GetEmployeesByEmail(Email) != null)
                {
                    return "Found";
                }
                else
                {
                    return "Not Found";
                }
            }
        }
    }

