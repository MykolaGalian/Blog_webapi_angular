using BLL.Contracts;
using DAL.Contracts;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ModeratorService : IModeratorService
    {
        private readonly IUnitOfWork _uow;
        public ModeratorService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task BlockPost(int postId, string aLogin)
        {
            if (postId > 0 && aLogin != null && aLogin.Length > 3)
            {
                Post post = await _uow.Posts.SelectById(postId);
                post.IsBlocked = true;
                await _uow.Posts.Update(post);
            }
            else throw new ArgumentException("Wrong data");
        }

        public async Task UnblockPost(int postId)
        {
            if (postId > 0)
            {
                Post post = await _uow.Posts.SelectById(postId);
                post.IsBlocked = false;
                await _uow.Posts.Update(post);
            }
            else throw new ArgumentException("Wrong data");
        }

        public void Dispose()
        {
            _uow?.Dispose();
        }
    }
}
