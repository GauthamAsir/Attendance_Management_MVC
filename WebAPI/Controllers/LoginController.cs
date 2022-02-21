using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class LoginController : ApiController
    {
        DBHelper dbHelper = new DBHelper();

        [HttpPost]
        public HttpResponseMessage Authenticate(LoginCredential login)
        {
            var user = dbHelper.ValidateUser(login);
            if (user == null)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public string Secret(string secret) 
        {
            return SecretConversion.SHA2_512(secret);
        }

    }
}
