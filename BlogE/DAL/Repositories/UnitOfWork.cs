using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using DAL.EF;
using DAL.Identity;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _db;

        private GenericRepository<Comment> _comments;
        private GenericRepository<Post> _posts;
        private GenericRepository<UserInfo> _users;

        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public UnitOfWork(string connection)
        {
            _db = new ApplicationDbContext(connection);
        }
        
        public IGenericRepository<Comment> Comments
        {
            get
            {
                if (this._comments == null)
                {
                    this._comments = new GenericRepository<Comment>(_db);
                }
                return _comments;
            }
        }
        public IGenericRepository<Post> Posts
        {
            get
            {
                if (this._posts == null)
                {
                    this._posts = new GenericRepository<Post>(_db); 
                }
                return _posts;
            }
        }

        public IGenericRepository<UserInfo> UserInfo
        {
            get
            {
                if (this._users == null)
                {
                    this._users = new GenericRepository<UserInfo>(_db);
                }
                return _users;
            }
        }
        
        public ApplicationUserManager UserManager
        {
            get
            {
                if (this._userManager == null)
                {
                    this._userManager = new ApplicationUserManager(new CustomUserStore(_db));
                }
                return _userManager;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                if (this._roleManager == null)
                {
                    this._roleManager = new ApplicationRoleManager(new CustomRoleStore(_db));
                }
                return _roleManager;
            }
        }
       
        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        
        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                    _roleManager.Dispose();
                    _userManager.Dispose();
                }
            }
            this._disposed = true;}

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
    }

    //https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
}
