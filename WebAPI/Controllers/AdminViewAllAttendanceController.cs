using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataLayer;

namespace WebAPI.Controllers
{
    public class AdminViewAllAttendanceController : ApiController
    {
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public List<sp_AdminViewAllAttendance_Result> AdminViewAllAttandance(DateTime? StartDate, DateTime? EndDate)
        {
            return new DBHelper().AdminViewAllAttandance(StartDate, EndDate);
        }
    }
}
