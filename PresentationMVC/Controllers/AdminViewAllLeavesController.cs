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
    public class AdminViewAllLeavesController : Controller
    {
        // GET: AdminViewAllLeaves
        public async Task<ActionResult> Index(DateTime? fromDate, DateTime? toDate)
        {
            Business busines = new Business();

            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();

            var leaves = await busines.AdminViewAllLeave(accessToken);

            var fileredLeaves = leaves;

            if(fromDate.HasValue && toDate.HasValue)
            {
                fileredLeaves = leaves.FindAll(l => l.Start_Date >= fromDate && l.End_Date <= toDate);
            }

            if (fromDate.HasValue && !toDate.HasValue)
            {
                fileredLeaves = leaves.FindAll(l => l.Start_Date >= fromDate);
            }

            return View(fileredLeaves);
        }
    }
}