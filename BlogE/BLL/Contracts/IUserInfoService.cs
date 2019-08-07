using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contracts
{
    public interface IUserInfoService : IDisposable
    {
        Task<List<DTOUser>> GetAllUserInfo();
        Task Update(DTOUser user);
        Task Delete(int userId);
        Task SetUserAvatar(int userId, string avatar);
        Task<DTOUser> GetUserById(int id);
        Task<DTOUser> GetUserByLogin(string login);
        Task<bool> CheckLoginExist(string login);
    }
}
