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
    public class LeaveController : Controller
    {
        Business bLL = new Business();

        public async Task<AuthModel> GetEmpId(string token)
        {
            return await new Business().GetAuthModel(token);
        }

        // GET: Leave
        public async Task<ActionResult> Index()
        {

            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();

            AuthModel auth = await GetEmpId(accessToken);
            int empId = int.Parse(auth.EmployeeId);

            ViewBag.Role = auth.RoleName;
            ViewBag.EmployeeId = empId;
            return View(await bLL.ViewLeave(accessToken, empId));
        }

        // GET: Leave/Create
        public async Task<ActionResult> RequestLeave()
        {
            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();

            AuthModel auth = await GetEmpId(accessToken);
            int empId = int.Parse(auth.EmployeeId);

            ViewBag.Role = auth.RoleName;
            ViewBag.EmployeeId = empId;
            ViewBag.RemainingLeaves = (await bLL.GetRemainingLeaves(accessToken, empId)).ToString();
            return View();
        }

        public static int GetWorkingDays(DateTime from, DateTime to)
        {
            var totalDays = 0;
            for (var date = from; date <= to; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday
                    && date.DayOfWeek != DayOfWeek.Sunday)
                    totalDays++;
            }
            return totalDays;
        }
        // POST: Leave/Create
        [HttpPost]
        public async Task<ActionResult> RequestLeave(LeaveTransactionDetail leaveTransaction)
        {
            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();
            ViewBag.EmployeeId = leaveTransaction.EmployeeId;

            AuthModel auth = await GetEmpId(accessToken);
            int empId = int.Parse(auth.EmployeeId);

            ViewBag.Role = auth.RoleName;

            ViewBag.RemainingLeaves = (await bLL.GetRemainingLeaves(accessToken, leaveTransaction.EmployeeId)).ToString();
            if (ModelState.IsValid)
            {
                int days = GetWorkingDays(leaveTransaction.StartDate, leaveTransaction.EndDate);
                if (days != 0)
                {
                    bool res = await bLL.RequestLeave(accessToken, leaveTransaction);
                    if (res)
                        return RedirectToAction("Index/" + leaveTransaction.EmployeeId);
                    TempData["Message"] = "Check for leaves on the same day";
                }
                else
                    TempData["Message"] = "You have applied leaves only for weekends";
            }
            return View();
        }

        // GET: Leave/Edit/5
        public async Task<ActionResult> Update(int Transid)
        {
            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();

            AuthModel auth = await GetEmpId(accessToken);
            int empId = int.Parse(auth.EmployeeId);

            ViewBag.Role = auth.RoleName;
            ViewBag.EmployeeId = empId;
            List<LeaveTransactionDetail> leave = null;
            leave = await bLL.ViewLeave(accessToken, empId);
            return View(leave.Find(a => a.EmployeeId == empId && a.TransactionId == Transid));
        }

        // POST: Leave/Edit/5
        [HttpPost]
        public async Task<ActionResult> Update(int Transid, LeaveTransactionDetail leaveTransaction)
        {
            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();
            leaveTransaction.TransactionId = Transid;
            ViewBag.EmployeeId = leaveTransaction.EmployeeId;

            AuthModel auth = await GetEmpId(accessToken);
            int empId = int.Parse(auth.EmployeeId);

            ViewBag.Role = auth.RoleName;

            if (ModelState.IsValid)
            {
                int days = GetWorkingDays(leaveTransaction.StartDate, leaveTransaction.EndDate);
                if (days != 0)
                {
                    bool res = await bLL.UpdateLeave(accessToken, leaveTransaction);
                    if (res)
                        return RedirectToAction("Index/" + leaveTransaction.EmployeeId);
                    TempData["Message"] = "Check for leaves on the same day";
                }
                else
                    TempData["Message"] = "You have applied leaves only from weekends";
            }
            return View();
        }

        // GET: Leave/Delete/5
        public async Task<ActionResult> Delete(int Transid, int? s)
        {
            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();

            AuthModel auth = await GetEmpId(accessToken);
            int empId = int.Parse(auth.EmployeeId);

            ViewBag.Role = auth.RoleName;
            ViewBag.EmployeeId = empId;

            List<LeaveTransactionDetail> leave = null;
            leave = await bLL.ViewLeave(accessToken, empId);

            LeaveTransactionDetail leaveTransactionDetail = leave.Find(a => a.TransactionId == Transid);

            if (leaveTransactionDetail.StartDate == DateTime.Now.Date && leaveTransactionDetail.TransactionStatus == "Accepted")
            {
                TempData["Message"] = "your leave is already accepted for today and cannot be deleted!!";
                return RedirectToAction("Index/" + empId);
            }
            
            return View(leaveTransactionDetail);
        }

        // POST: Leave/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, int Transid)
        {
            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();

            AuthModel auth = await GetEmpId(accessToken);
            int empId = int.Parse(auth.EmployeeId);

            ViewBag.Role = auth.RoleName;

            bool res = await bLL.DeleteLeave(accessToken, Transid);
            if (res)
            {
                return RedirectToAction("Index/" + id);
            }
            return ErrorPage();
        }

        [HandleError]
        public ActionResult ErrorPage()
        {
            return View();
        }

    }
}