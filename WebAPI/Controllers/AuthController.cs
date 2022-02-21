using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class AuthController : ApiController
    {
        DBHelper dbHelper = new DBHelper();

        [HttpPost]
        public List<KeyValuePair<string, string>> SignUp(SignUpModel signUp)
        {
            return dbHelper.SignUp(signUp);
        }

        [HttpGet]
        public DateTime? GetLastRefreshedDate(int id)
        {
            return dbHelper.GetLastRefreshedDate(id);
        }

        [HttpPost]
        public void AddMonthlyLeaves(int empid)
        {
            dbHelper.AddMonthlyLeaves(empid);
        }

    }
}
