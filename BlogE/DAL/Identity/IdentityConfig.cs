using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;


namespace DAL.Identity
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser, int>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser, int> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new CustomUserStore(context.Get<ApplicationDbContext>()));
            
            // Configure validation logic for usernames 
            manager.UserValidator = new UserValidator<ApplicationUser, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords 
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = false,
            };
            
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser, int>(
                        dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }


    public class ApplicationRoleManager : RoleManager<CustomRole, int>
    {
        public ApplicationRoleManager(CustomRoleStore store)
            : base(store)
        { }

        
    }
    //https://docs.microsoft.com/en-us/aspnet/identity/overview/extensibility/change-primary-key-for-users-in-aspnet-identity
}
