using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;

namespace PresentationMVC.Controllers
{
    public class AdminViewAllAttendanceController : Controller
    {
        // GET: AdminViewAllAttendance
        public async Task<ActionResult> Index(DateTime? fromDate, DateTime? toDate)
        {
            Business busines = new Business();

            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();

            if(fromDate.HasValue && toDate.HasValue)
            {
                return View(await busines.AdminViewAllAttendance(accessToken,
                fromDate.Value, toDate.Value));
            }

            if (fromDate.HasValue && !toDate.HasValue)
            {
                return View(await busines.AdminViewAllAttendance(accessToken,
                fromDate.Value, DateTime.Now));
            }

            return View(await busines.AdminViewAllAttendance(accessToken,
                DateTime.Now.AddMonths(-1), DateTime.Now));
        }
    }
}