using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Contracts;
using DAL.Contracts;
using DAL.Models;

namespace BLL.Services
{
    public class BLLUnitOfWork : IBLLUnitOfWork
    {
        private readonly IUnitOfWork _uow;
        private AdminService _adminService;
        private CommentService _commentService;
        private PostService _postService;
        private UserInfoService _userInfoService;
        private UserManagerService _userManagerService;
        private ModeratorService _moderatorService;

        public BLLUnitOfWork(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public IAdminService AdminService
        {
            get
            {
                if (this._adminService == null)
                {
                    this._adminService = new AdminService(_uow);
                }
                return _adminService;
            }
        }

        public IModeratorService ModeratorService
        {
            get
            {
                if (this._moderatorService == null)
                {
                    this._moderatorService = new ModeratorService(_uow);
                }
                return _moderatorService;
            }
        }

        public ICommentService CommentService
        {
            get
            {
                if (this._commentService == null)
                {
                    this._commentService = new CommentService(_uow);
                }
                return _commentService;
            }
        }
        public IPostService PostService
        {
            get
            {
                if (this._postService == null)
                {
                    this._postService = new PostService(_uow);
                }
                return _postService;
            }
        }
        public IUserInfoService UserInfoService
        {
            get
            {
                if (this._userInfoService == null)
                {
                    this._userInfoService = new UserInfoService(_uow);
                }
                return _userInfoService;
            }
        }

        public IUserManagerService UserManagerService
        {
            get
            {
                if (this._userManagerService == null)
                {
                    this._userManagerService = new UserManagerService(_uow);
                }
                return _userManagerService;
            }
        }

        private bool _disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _uow?.Dispose();
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
