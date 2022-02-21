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
    public class ManagerAttendanceController : Controller
    {
        Business bl = new Business();

        public async Task<AuthModel> GetEmpId(string token)
        {
            return await new Business().GetAuthModel(token);
        }

        // GET: ManagerAttendance
        public async Task<ActionResult> Index()
        {
            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();

            AuthModel auth = await GetEmpId(accessToken);
            int managerid = int.Parse(auth.EmployeeId);

            ViewBag.Role = auth.RoleName;
            ViewBag.EmployeeId = managerid;
            List<ProjectAttendanceDetails> accounts = await bl.ManagerViewPendingAttendance(accessToken, managerid);
            return View(accounts);
        }
        public async Task<ActionResult> Accept(int id, int empid)
        {
            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();

            AuthModel auth = await GetEmpId(accessToken);
            int managerid = int.Parse(auth.EmployeeId);

            ViewBag.Role = auth.RoleName;

            bool res = await bl.UpdateAttendance(accessToken, managerid, new ProjectAttendance()
            {
                EmployeeId = empid,
                ProjectId = id,
                AttendanceStatus = 1,
            });
            if (res)
                return RedirectToAction("Index/" + managerid);
            return HttpNotFound();

        }
        public async Task<ActionResult> Reject(int id, int empid)
        {
            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();

            AuthModel auth = await GetEmpId(accessToken);
            int managerid = int.Parse(auth.EmployeeId);

            ViewBag.Role = auth.RoleName;

            bool res = await bl.UpdateAttendance(accessToken, managerid, new ProjectAttendance()
            {
                EmployeeId = empid,
                ProjectId = id,
                AttendanceStatus = 2,
            });
            if (res)
                return RedirectToAction("Index/" + managerid);
            return HttpNotFound();

        }
    }
}