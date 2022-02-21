using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class ProjectDetailsController : ApiController
    {

        DBHelper DBHelper = new DBHelper();

        // Add Employee To Project
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public HttpResponseMessage AddEmployeeToProject(ProjectAttendance projectAttendance)
        {
            if (projectAttendance.ProjectId <= 0)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            if (projectAttendance.EmployeeId <= 0)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            if (projectAttendance.DateOfAttendance == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            if (projectAttendance.EndDate == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            DBHelper.AddEmployeeToProject(projectAttendance);
            return new HttpResponseMessage(HttpStatusCode.OK);

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public HttpResponseMessage RemoveEmployeeFromProject(int id, int projectid)
        {
            if (projectid <= 0)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            if (id <= 0)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            DBHelper.RemoveEmployeeFromProject(empid: id, projectid: projectid);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // List of Employees Working on Project
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public List<EmployeeDetail> ListEmployeesInProject(int id)
        {
            return DBHelper.ListEmployeesFromProject(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/ProjectDetails/ListEmpNotInProject/{id?}")]
        public List<EmployeeDetail> ListEmployeesNotInProject(int id)
        {
            return DBHelper.ListEmployeesNotInProject(id);
        }

    }
}
