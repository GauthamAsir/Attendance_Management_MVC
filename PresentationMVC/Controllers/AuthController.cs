using PresentationMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using Newtonsoft.Json;
using System.Threading.Tasks;
using DataLayer;

namespace PresentationMVC.Controllers
{
    public class AuthController : Controller
    {

        Business Business = new Business();

        // GET: Auth-Login
        public ActionResult Login()
        {
            ViewBag.Title = "Login";
            return View();
        }

        // GET: Auth-SignUp
        public ActionResult SignUp()
        {
            ViewBag.Title = "SignUp";
            return View();
        }

        // POST: Auth-Login
        [HttpPost]
        public async Task<ActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {

                string NewToken = await Business.GetToken(new UserLogin()
                {
                    UserName = loginModel.EmployeeID.ToString(),
                    Password = loginModel.Password,
                    grant_type = "password"
                });

                try
                {

                    if (string.IsNullOrEmpty(NewToken))
                    {
                        ViewBag.Message = "Token is Empty";
                        return View(loginModel);
                    }

                    MyAccessToken AccessToken = JsonConvert.DeserializeObject<MyAccessToken>(NewToken);
                    Session["token"] = AccessToken.access_token;

                    AuthModel authModel = await Business.GetAuthModel(AccessToken.access_token);

                    if (!authModel.RoleName.Equals("Admin"))
                    {
                        DateTime? lastRefreshedDate = await Business.GetLastRefreshedDate(
                        int.Parse(loginModel.EmployeeID.ToString()), AccessToken.access_token);

                        if (lastRefreshedDate.Value.Month != DateTime.Now.Month &&
                            lastRefreshedDate.Value.Month < DateTime.Now.Month &&
                            lastRefreshedDate.Value.Year <= DateTime.Now.Year)
                        {
                            await Business.AddMonthlyLeaves(int.Parse(loginModel.EmployeeID.ToString()), AccessToken.access_token);
                        }
                    }

                    if (authModel.RoleName.Equals("Admin"))

                        return Redirect("~/AdminDashboard/Index");

                    if (authModel.RoleName.Equals("Manager"))
                        return Redirect("~/ManagerDashboard/Index");

                    return Redirect("~/UserDashboard/Index");

                }
                catch (Exception e)
                {
                    ViewBag.Message = e.StackTrace;
                    return View(loginModel);
                }

            }
            return View(loginModel);
        }

        // POST: Auth-SignUp
        [HttpPost]
        public async Task<ActionResult> SignUp(Models.SignUpModel signUpModel)
        {
            if (ModelState.IsValid)
            {
                string strSignUp = JsonConvert.SerializeObject(signUpModel);
                string result = await Business.SignUp(strSignUp);

                if (string.IsNullOrEmpty(result))
                {
                    ViewBag.Message = "Something went wrong!";
                    return View(signUpModel);
                }

                List<KeyValuePair<string, string>> res = JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(result);

                if (res.Find(r => r.Key == "status").Value == "1")
                {
                    string NewToken = await Business.GetToken(new UserLogin()
                    {
                        UserName = res.Find(r => r.Key == "eid").Value,
                        Password = signUpModel.Password,
                        grant_type = "password"
                    });

                    MyAccessToken AccessToken = JsonConvert.DeserializeObject<MyAccessToken>(NewToken);
                    Session["token"] = AccessToken.access_token;

                    return Redirect("~/UserDashboard/Index");

                }

            }
            return View(signUpModel);
        }

    }
}