using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Identity
{
    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
       
        public CustomRole(string name) { Name = name; }
        public CustomRole() { }
    }

    //https://docs.microsoft.com/en-us/aspnet/identity/overview/extensibility/change-primary-key-for-users-in-aspnet-identity
}
