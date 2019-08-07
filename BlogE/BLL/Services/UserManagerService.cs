using BLL.Contracts;
using BLL.DTO;
using BLL.Infrastructure;
using DAL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DAL.Identity;
using DAL.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace BLL.Services
{
    public class UserManagerService : IUserManagerService
    {
        private readonly IUnitOfWork _uow;
        public UserManagerService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<bool> CheckUserByLoginPas(DTOUser dtouser)
        {
           
            ApplicationUser user = await _uow.UserManager.FindAsync(dtouser.Login, dtouser.Password);            

            return  await _uow.UserManager.CheckPasswordAsync(user, dtouser.Password); 
        }

        public async Task<OperationDetails> Create(DTOUser dtouser)
        {
            ApplicationUser user = await _uow.UserManager.FindByEmailAsync(dtouser.Email); //check email
            if (user == null)
            {
              
                user = AutoMapper.Mapper.Map<DTOUser, ApplicationUser>(dtouser);
                user.UserInfo = AutoMapper.Mapper.Map<DTOUser, UserInfo>(dtouser);
                

                var result = await _uow.UserManager.CreateAsync(user, dtouser.Password);
                if (result.Errors.Any())
                 return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                await _uow.UserManager.AddToRoleAsync(user.Id, dtouser.Roles[0]);   // only User type "user"
                await _uow.Save();
                 return new OperationDetails(true, "Registration success", "");
            }
            else
            {
                return new OperationDetails(false, "User with this email exist", "email");
            }
        }

        public async Task<ClaimsIdentity> GetClaim(string username, string password)
        {
            var user = await _uow.UserManager.FindAsync(username, password);

            if (user != null)
            {
                ClaimsIdentity claim = await _uow.UserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ExternalBearer); //UseOAuthBearerTokens method for DefaultAuthentication
                return claim;
            }
            else return null;
        }

        public async Task ChangePassword(int userId, string oldPass, string newPass)
        {
            await _uow.UserManager.ChangePasswordAsync(userId, oldPass, newPass);
        }

        public async Task<DTOUser> GetUserByLogin(string login)
        {
            return AutoMapper.Mapper.Map<ApplicationUser, DTOUser>(await _uow.UserManager.Users.FirstOrDefaultAsync(x => x.UserName == login));
        }

        public async Task<bool> IsUserInRoleAdmin(int userId)
        {
            return await _uow.UserManager.IsInRoleAsync(userId, "admin");
        }

        public async Task<bool> IsUserInRoleModerator(int userId)
        {
            return await _uow.UserManager.IsInRoleAsync(userId, "moderator");
        }      

    }
}
