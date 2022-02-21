using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PresentationMVC.Controllers
{
    public class ManagerDashboardController : Controller
    {
        // GET: ManagerDashboard
        public async Task<ActionResult> Index()
        {
            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();
            ViewBag.Title = "Dashboard";

            AuthModel authModel = await GetEmpId(accessToken);

            ViewBag.Name = authModel.EmployeeName;

            return View();
        }

        public async Task<AuthModel> GetEmpId(string token)
        {
            return await new Business().GetAuthModel(token);
        }

    }
}