using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi.Models.Post
{
    public class PostEditViewModel
    {
        public string PostBody { get; set; }

        public string PostTitle { get; set; }

        [Display(Name = "Hashtags:")]
        [MaxLength(50)]
        public string Hashtags { get; set; }
       
    }
}