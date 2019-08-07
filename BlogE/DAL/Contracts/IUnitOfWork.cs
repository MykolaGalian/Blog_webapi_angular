using DAL.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }

        IGenericRepository<Comment> Comments { get; }
        IGenericRepository<Post> Posts { get; }
        IGenericRepository<UserInfo> UserInfo { get; }
        Task Save(); 
    }
}
