using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using DataLayer;

namespace PresentationMVC.Controllers
{
    public class AdminAttendanceController : Controller
    {
        Business bl = new Business();

        public async Task<AuthModel> GetEmpId(string token)
        {
            return await new Business().GetAuthModel(token);
        }

        // GET: AdminAttendance
        public async Task<ActionResult> Index()
        {

            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();

            AuthModel auth = await GetEmpId(accessToken);

            ViewBag.Role = auth.RoleName;

            return View(await bl.AdminViewPendingAttendance(accessToken));
        }

        public async Task<ActionResult> Accept(int id, int empid)
        {
            string accessToken = Session["token"].ToString();

            if (string.IsNullOrEmpty(accessToken))
            {
                return Redirect("~/");
            }

            bool res = await bl.AdminUpdateAttendance(accessToken, new ProjectAttendance()
            {
                EmployeeId = empid,
                ProjectId = id,
                AttendanceStatus = 1,
            });
            if (res)
                return RedirectToAction("Index");
            return HttpNotFound();

        }
        public async Task<ActionResult> Reject(int id, int empid)
        {
            string accessToken = Session["token"].ToString();

            if (string.IsNullOrEmpty(accessToken))
            {
                return Redirect("~/");
            }

            bool res = await bl.AdminUpdateAttendance(accessToken, new ProjectAttendance()
            {
                EmployeeId = empid,
                ProjectId = id,
                AttendanceStatus = 2,
            });
            if (res)
                return RedirectToAction("Index");
            return HttpNotFound();

        }
    }
}