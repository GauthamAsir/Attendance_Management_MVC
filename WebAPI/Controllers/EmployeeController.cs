using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataLayer;
using System.Data.Entity;

namespace WebAPI.Controllers
{
    public class EmployeeController : ApiController
    {

        DBHelper dbHelper = new DBHelper();

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public List<EmployeeDetail> Get()
        {
            return dbHelper.GetAllEmployees();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public EmployeeDetail GetEmployee(int id)
        {
            return dbHelper.GetEmployee(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public HttpResponseMessage AddEmployee(EmployeeDetail employee)
        {
            bool result = dbHelper.AddEmployee(employee);
            if (result == true)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public HttpResponseMessage UpdateEmployeeInfo(int id, EmployeeDetail employee)
        {
            bool result = dbHelper.UpdateEmployeeInfo(id, employee);
            if (result == true)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public HttpResponseMessage DeleteEmployeeInfo(int id)
        {
            bool result = dbHelper.DeleteEmployeeInfo(id);
            if (result == true)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/GetAllManagers")]
        public List<EmployeeDetail> GetAllManagers()
        {
            return dbHelper.GetAllManagers();
        }

    }
}
