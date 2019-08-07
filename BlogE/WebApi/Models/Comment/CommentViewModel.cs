using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models.User;

namespace WebApi.Models.Comment
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string CommentBody { get; set; }
        public int PostId { get; set; }
        public DateTime? DateCreation { get; set; }
        public DateTime? LastEdit { get; set; }

        public int? UserInfoId { get; set; }
        public ProfileViewModel UserInfo { get; set; }

    }
}