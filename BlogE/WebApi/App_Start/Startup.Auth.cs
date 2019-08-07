using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using WebApi.Providers;
using WebApi.Models;
using BLL.Contracts;
using Ninject;
using WebApi.App_Start;

namespace WebApi
{
    public partial class Startup
    {
        [Inject] 
        public IBLLUnitOfWork UnitOfWork { get; set; }
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public Startup()
        {
            var kernel = NinjectWebCommon.Kernel;
            kernel.Inject(this);
        }

        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the application for OAuth based flow
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"), // //will be used to generate token for valid user
                Provider = new ApplicationOAuthProvider(UnitOfWork),
                
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthAuthorizationServer(OAuthOptions);  // authorization server middleware       

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}
