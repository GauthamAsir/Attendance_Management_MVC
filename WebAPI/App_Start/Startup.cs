using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPI.Models;

[assembly: OwinStartup(typeof(WebAPI.App_Start.Startup))]

namespace WebAPI.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                //The Path for generating the Token
                TokenEndpointPath = new PathString("/almtoken"),
                //Setting the Token Expired Time => 24 hrs = 1 day
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                //The Class AuthorizationServerProvider will validate the user Credentials
                Provider = new AuthorizationServerProvider()
            };
            //Token Generation
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
        }
    }
}