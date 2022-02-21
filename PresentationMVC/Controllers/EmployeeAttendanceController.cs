using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using BusinessLayer;
using System.Threading.Tasks;
using PresentationMVC.Models;

namespace PresentationMVC.Controllers
{
    public class EmployeeAttendanceController : Controller
    {
        // GET: Attendance
        Business bl = new Business();

        public async Task<AuthModel> GetEmpId(string token)
        {
            return await new Business().GetAuthModel(token);
        }

        public async Task<ActionResult> Index()
        {

            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();

            AuthModel auth = await GetEmpId(accessToken);
            int empId = int.Parse(auth.EmployeeId);

            ViewBag.EmployeeId = empId;
            ViewBag.Role = auth.RoleName;

            List<ProjectAttendanceDetails> accounts = await bl.Index(accessToken, empId);
            return View(accounts);
        }

        // GET: Attendance/Create
        public async Task<ActionResult> Create()
        {
            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();

            AuthModel auth = await GetEmpId(accessToken);
            int empId = int.Parse(auth.EmployeeId);

            ViewBag.EmployeeId = empId;
            ViewBag.Role = auth.RoleName;

            List<ProjectDetail> accounts = await bl.GetProjectOfEmployees(accessToken, ViewBag.EmployeeId);
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var a in accounts)
            {
                if(a.EndDate < DateTime.Now)
                    continue;

                bool res = await bl.IsAttendanceMarked(accessToken, empId, a.ProjectId);
                if (res)
                    continue;
                items.Add(new SelectListItem()
                {
                    Text = a.ProjectName,
                    Value = a.ProjectId.ToString(),
                });
            }

            ViewBag.Project = items;
            return View();
        }
        // POST: Attendance/Create
        [HttpPost]
        public async Task<ActionResult> Create(ProjectAttendanceDetailsModel pa)
        {
            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();
            bool res = await bl.AddAttendance(accessToken, new ProjectAttendance()
            {
                EmployeeId = pa.EmployeeId,
                ProjectId = pa.ProjectId,
                DateOfAttendance = pa.DateOfRequest,
            });
            if (res)
            {
                return RedirectToAction("Index/" + pa.EmployeeId);
            }
            return HttpNotFound();
        }


        // GET: Attendance/Delete/5
        public async Task<ActionResult> Delete(int id)
        {

            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();

            AuthModel auth = await GetEmpId(accessToken);
            int empId = int.Parse(auth.EmployeeId);

            ViewBag.Role = auth.RoleName;

            List<ProjectAttendanceDetails> accounts = new List<ProjectAttendanceDetails>();
            accounts = await bl.Index(accessToken, empId);
            return View(accounts.Find(a => a.ProjectId == id && a.EmployeeId == empId));
        }

        // POST: Attendance/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, string status)
        {

            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();

            AuthModel auth = await GetEmpId(accessToken);
            int empId = int.Parse(auth.EmployeeId);


            if (status == "Pending")
            {
                List<KeyValuePair<string, string>> success = await bl.DeleteAttendance(accessToken, id, empId);
                if (success.Find(a => a.Key == "status").Value == "200")
                {
                    return RedirectToAction("Index/" + empId);
                }
                ViewBag.Message = success.Find(a => a.Key == "error").Value;
                List<ProjectAttendanceDetails> accounts = new List<ProjectAttendanceDetails>();
                accounts = await bl.Index(accessToken, empId);
                return View(accounts.Find(a => a.ProjectId == id && a.EmployeeId == empId));
            }
            else if (status == "Accepted")
            {
                ViewBag.Message = "Cannot Delete an Accepted Attendance";
                List<ProjectAttendanceDetails> accounts = new List<ProjectAttendanceDetails>();
                accounts = await bl.Index(accessToken, empId);
                return View(accounts.Find(a => a.ProjectId == id && a.EmployeeId == empId));
            }
            else if (status == "Rejected")
            {
                ViewBag.Message = "Cannot Delete an Rejected Attendance";
                List<ProjectAttendanceDetails> accounts = new List<ProjectAttendanceDetails>();
                accounts = await bl.Index(accessToken, empId);
                return View(accounts.Find(a => a.ProjectId == id && a.EmployeeId == empId));
            }
            return HttpNotFound();
        }
    }
}