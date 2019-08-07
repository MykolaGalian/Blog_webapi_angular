using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contracts
{
    public interface IPostService : IDisposable
    {
        Task<List<DTOPost>> GetAllPosts();        
        Task AddPost(DTOPost dtoPost);
        Task EditPost(DTOPost dtoPost);
        Task DeletePost(int postId);
        Task<DTOPost> GetPostByPosId(int postId);
        Task<IEnumerable<DTOPost>> GetPostsByUserId(int userid);
        
        Task<IEnumerable<DTOPost>> GetPostsByTeg(string teg);
        Task<IEnumerable<string>> GetAllTeg();


    }
}
