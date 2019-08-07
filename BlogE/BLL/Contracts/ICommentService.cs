using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contracts
{
    public interface ICommentService : IDisposable
    {
        Task AddComment(DTOComment dtoComment);
        Task EditComment(DTOComment dtoComment);
        Task DeleteComment(int dtoCommentId); 
        Task<IEnumerable<DTOComment>> GetCommentsToPost(int postId);
        Task<DTOComment> GetCommentByComId(int comid);
    }
}
