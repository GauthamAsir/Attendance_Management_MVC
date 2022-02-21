using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataLayer;

namespace WebAPI.Controllers
{
    public class AdminViewAllLeaveController : ApiController
    {
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public List<sp_AdminViewAllLeave_Result> AdminViewAllLeaves()
        {
            return new DBHelper().AdminViewAllLeaves();
        }
    }
}
