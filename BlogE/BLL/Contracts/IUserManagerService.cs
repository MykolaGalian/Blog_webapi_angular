using BLL.DTO;
using BLL.Infrastructure;
using DAL.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contracts
{
    public interface IUserManagerService
    {
        Task<bool> CheckUserByLoginPas(DTOUser user);
        Task<OperationDetails> Create(DTOUser user);
        Task<ClaimsIdentity> GetClaim(string username, string password);
        Task<DTOUser> GetUserByLogin(string login);
        Task<bool> IsUserInRoleAdmin(int userId); //check user if Admin
        Task<bool> IsUserInRoleModerator(int userId); //check user if Moderator
        Task ChangePassword(int userId, string oldPass, string newPass);


    }
}
