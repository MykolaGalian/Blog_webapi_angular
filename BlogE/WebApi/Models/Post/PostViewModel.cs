using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models.Comment;
using WebApi.Models.User;

namespace WebApi.Models.Post
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string PostBody { get; set; }

        public string PostTitle { get; set; }
        public string PostPicture { get; set; }
        public int UserInfoId { get; set; }
        public virtual ProfileViewModel UserInfo { get; set; }
        public DateTime? DateCreate { get; set; }
        public DateTime? LastEdit { get; set; }
       
        public ICollection<CommentViewModel> Comments { get; set; }
        
        public string Hashtags { get; set; }
        public bool IsBlocked { get; set; }

    }
}