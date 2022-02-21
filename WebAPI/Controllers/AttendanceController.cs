using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class AttendanceController : ApiController
    {
        DBHelper helper = new DBHelper();

        [HttpGet]
        public List<ProjectAttendanceDetails> EmployeeViewAttendanceList(int id)
        {
            return helper.EmployeeViewAttendanceList(id);
        }

        [HttpGet]
        [Route("EmployeeProjects/{id?}")]
        public List<ProjectDetail> GetProjectOfEmployees(int id)
        {
            return helper.GetProjectOfEmployees(id);
        }

        [HttpPost]
        public HttpResponseMessage AddAttendance(ProjectAttendance pa)
        {
            bool res = helper.AddAttendance(pa);
            if (res == true)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.BadGateway);
        }

        [HttpDelete]
        [Route("Employee/DeleteAttendance/{empid?}/{projectid?}")]
        public HttpResponseMessage DeleteAttendance(int empid, int projectid)
        {
            int res = helper.DeleteAttendance(empid, projectid);
            if (res == 1)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            if (res == 0)
            {
                return new HttpResponseMessage(HttpStatusCode.NotAcceptable);
            }
            return new HttpResponseMessage(HttpStatusCode.BadGateway);
        }

        [HttpGet]
        [Route("Employee/AttMarked/{empid?}/{projectid?}")]
        public bool IsAttendanceMarked(int empid, int projectid)
        {
            int res = helper.IsAttendanceMarked(empid, projectid);
            return res > 0;
        }

        [HttpGet]
        [Route("Manager/ViewAttendance/{id?}")]
        public List<ProjectAttendanceDetails> ManagerViewAttendanceList(int id)
        {
            return helper.ManagerViewAttendanceList(id);
        }

        [HttpPut]
        [Route("Manager/ApproveAttendance/{id?}")]
        [Route("Admin/ApproveAttendance")]
        public HttpResponseMessage UpdateAttendance(ProjectAttendance pa)
        {
            bool res = helper.UpdateAttendance(pa);
            if (res == true)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.BadGateway);
        }

        [HttpGet]
        [Route("Admin/ViewAttendance")]
        public Object AdminViewAttendanceList()
        {
            return helper.AdminViewAttendanceList();
        }
    }
}
