using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Microsoft.Owin.Cors;

using Owin;

[assembly: OwinStartup(typeof(WebApi.Startup))]

namespace WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);  //Microsoft.Owin.Cors
            ConfigureAuth(app);
        }
    }
}
