using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Web.Http;
using WebAPI.Models;
using DataLayer;

namespace WebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public AuthModel Get()
        {
            return new AuthModel()
            {
                EmployeeId = getClaimValue("EmpId"),
                EmployeeName = getClaimValue(ClaimTypes.Name),
                RoleName = getClaimValue(ClaimTypes.Role)
            };
        }

        string getClaimValue(string type)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            return identity.Claims.Where(c => c.Type == type).Select(c => c.Value).SingleOrDefault();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
