using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi.Models.Account
{
    public class RegisterViewModel
    {
        

        [Required]
        [MaxLength(10)]
        [MinLength(3)]
        [Display(Name = "Login:")]
        public string Login { get; set; }

        [Required]
        [MaxLength(10)]
        [MinLength(3)]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password is not equal to the above")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password:")]
        public string ConfirmPassword { get; set; }

        public string Address { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "Name:")]
        public string Name { get; set; }

        [MaxLength(10)]
        [Display(Name = "Lastname:")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email:")]
        [MaxLength(20)]
        [MinLength(5)]
        public string Email { get; set; }

    }
}