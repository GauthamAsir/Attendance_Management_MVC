using DataLayer;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using WebAPI.Models;

namespace WebAPI.Models
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        DBHelper DBHelper = new DBHelper();

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.CompletedTask;
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            LoginCredential login = new LoginCredential()
            {
                EmployeeId = Convert.ToInt32(context.UserName),
                LoginPassword = context.Password
            };
            var user = DBHelper.ValidateUser(login);
            if (user == null)
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
                return Task.CompletedTask;
            }
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Role, user.EmployeeDetail.RoleDetail.RoleName));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.EmployeeDetail.FirstName));
            identity.AddClaim(new Claim("EmpId", user.EmployeeDetail.EmployeeId.ToString()));
            context.Validated(identity);
            return Task.CompletedTask;
        }
    }
}