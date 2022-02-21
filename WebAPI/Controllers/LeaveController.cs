using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataLayer;

namespace WebAPI.Controllers
{
    public class LeaveController : ApiController
    {
        DBHelper helper = new DBHelper();

        [HttpGet, Route("GetRemainingLeaves/{id}")]
        public Object GetRemainingLeaves(int id)
        {
            return helper.GetRemainingLeaves(id);
        }

        [HttpGet, Route("ViewLeave/{id}")]

        public List<LeaveTransactionDetail> ViewLeave(int id)
        {
            return helper.ViewLeave(id);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet, Route("ManagerViewPendingLeaves/{id}")]
        public List<LeaveTransactionDetail> ManagerViewPendingLeaves(int id)
        {
            return helper.ManagerViewPendingLeaves(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet, Route("AdminViewPendingLeaves")]
        public List<LeaveTransactionDetail> AdminViewPendingLeaves()
        {
            return helper.AdminViewPendingLeaves();
        }

        [HttpPut, Route("UpdateStatus/")]
        public HttpResponseMessage UpdateLeaveStatus(LeaveTransactionDetail leaveTransaction)
        {
            bool res = helper.UpdateLeaveStatus(leaveTransaction);
            if (res)
                return new HttpResponseMessage(HttpStatusCode.OK);
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        [HttpPost]
        public HttpResponseMessage RequestLeave(LeaveTransactionDetail leaveTransaction)
        {
            int result = helper.RequestLeave(leaveTransaction);
            if (result == 1)
                return new HttpResponseMessage(HttpStatusCode.OK);
            else
                return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        [HttpPut]
        public HttpResponseMessage UpdateLeave(LeaveTransactionDetail leaveTransaction)
        {
            int result = helper.UpdateLeave(leaveTransaction);
            if (result == 1)
                return new HttpResponseMessage(HttpStatusCode.OK);
            else
                return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        [HttpDelete, Route("Delete/{Transid}")]
        public HttpResponseMessage DeleteLeave(int Transid)
        {
            int result = helper.DeleteLeave(Transid);
            if (result == 1)
                return new HttpResponseMessage(HttpStatusCode.OK);
            else
                return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
    }
}
