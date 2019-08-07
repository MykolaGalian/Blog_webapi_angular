using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models.Comment
{
    public class CommentEditViewModel
    {
        public int Id { get; set; }

        public string CommentBody { get; set; }

        public int PostId { get; set; }
    }
}