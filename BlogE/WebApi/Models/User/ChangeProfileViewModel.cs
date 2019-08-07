using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApi.Models.Post;

namespace WebApi.Models.User
{
    public class ChangeProfileViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Login:")]
        public string Login { get; set; }

        [Display(Name = "Name:")]
        public string Name { get; set; }

        [Display(Name = "Lastname:")]
        public string LastName { get; set; }

        [Display(Name = "Address:")]
        public string Address { get; set; }

        [Display(Name = "Avatar:")]
        public string UserAvatar { get; set; }

        [Display(Name = "Email:")]
        public string Email { get; set; }
        public DateTime? DateRegistration { get; set; }
        public bool IsBlocked { get; set; }
    }
}