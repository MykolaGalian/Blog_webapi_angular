using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi.Models.Account
{
    public class LoginViewModel
    {
        [Required]
        [MaxLength(10)]
        [MinLength(3)]
        [Display(Name = "Login:")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(10)]
        [MinLength(3)]
        [Display(Name = "Password:")]
        public string Password { get; set; }
    }
}