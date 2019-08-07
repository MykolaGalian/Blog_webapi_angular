using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contracts
{
    public interface IBLLUnitOfWork
    {
        IAdminService AdminService { get; }
        ICommentService CommentService { get; }
        IPostService PostService { get; }
        IUserInfoService UserInfoService { get; }
        IUserManagerService UserManagerService { get; }
        IModeratorService ModeratorService { get; }

    }
}
