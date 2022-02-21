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
    public class AdminLeaveController : Controller
    {

        Business bLL = new Business();

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

            ViewBag.Role = auth.RoleName;

            return View(await bLL.AdminViewPendingLeaves(accessToken));
        }

        public async Task<ActionResult> UpdateLeaveStatus(int id, int Transid, string TransStatus)
        {

            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();

            bool res = await bLL.UpdateLeaveStatus(accessToken, new LeaveTransactionDetail()
            {
                EmployeeId = id,
                TransactionId = Transid,
                TransactionStatus = TransStatus
            });
            if (res)
                return RedirectToAction("Index");
            return HttpNotFound();

        }

        // GET: AdminLeave/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminLeave/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminLeave/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminLeave/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminLeave/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminLeave/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminLeave/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}