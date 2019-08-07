using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Contracts;
using BLL.DTO;
using DAL.Contracts;
using DAL.Models;

namespace BLL.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _uow;
        public PostService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public void Dispose()
        {
            _uow?.Dispose();
        }

        public async Task<List<DTOPost>> GetAllPosts()
        {
            var posts = (await _uow.Posts.SelectAll()).OrderByDescending(x => x.DateCreate);             

            if (posts == null)
                throw new ArgumentException("Post not exist");
            return AutoMapper.Mapper.Map<IEnumerable<Post>, List<DTOPost>>(posts).ToList();
        }       


        public async Task AddPost(DTOPost dtoPost)
        {
            var post = AutoMapper.Mapper.Map<DTOPost, Post>(dtoPost);
            if (post.PostBody != null) 
            {
                await _uow.Posts.Insert(post);
            }
            else throw new ArgumentException("Wrong data");}

     

        public async Task EditPost(DTOPost dtoPost)
        {
            var post = AutoMapper.Mapper.Map<DTOPost, Post>(dtoPost);

            if (post.UserInfoId != 0)
            {
                await _uow.Posts.Update(post);
            }
            else throw new ArgumentException("Wrong data");
        }

        public async Task DeletePost(int postId)
        {
            List<Comment> postComments = (await _uow.Comments.SelectAll(x => x.PostId == postId)).ToList();
            if (postComments != null && postComments.Count > 0)
                foreach (var pcom in postComments)
                {
                    await _uow.Comments.Delete(pcom.Id);
                }

            await _uow.Posts.Delete(postId);
        }

        public async Task<DTOPost> GetPostByPosId(int postId) 
        {
            var post = await _uow.Posts.SelectById(postId);
            post.Comments = (await _uow.Comments.SelectAll(x => x.PostId == post.Id)).ToList(); //add list of comments

            if (post == null)
                throw new ArgumentException("Post not exist");
            return AutoMapper.Mapper.Map<Post, DTOPost>(post);
        }

        public async Task<IEnumerable<DTOPost>> GetPostsByUserId(int userid)
        {
            var posts = (await _uow.Posts.SelectAll(x => x.UserInfoId == userid)).OrderByDescending(x => x.DateCreate).ToList();
            return AutoMapper.Mapper.Map<IEnumerable<Post>, IEnumerable<DTOPost>>(posts);
        }

      
        public async Task<IEnumerable<DTOPost>> GetPostsByTeg(string teg)
        {
            var posts = (await _uow.Posts.SelectAll(x => x.Hashtags == teg)).OrderByDescending(x => x.DateCreate).ToList();
            return AutoMapper.Mapper.Map<IEnumerable<Post>, IEnumerable<DTOPost>>(posts);
        }

        public async Task<IEnumerable<string>> GetAllTeg()
        {
            var posts = (await _uow.Posts.SelectAll());
            var tags = from p in posts
                select new { tag = p.Hashtags };

           List<string> tagList = new List<string>();
           foreach (var t in tags)
           {
               tagList.Add(t.tag);
           }

           return tagList.Distinct();
        }
    }
}
