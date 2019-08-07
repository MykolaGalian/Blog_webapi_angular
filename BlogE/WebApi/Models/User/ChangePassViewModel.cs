using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi.Models.User
{
    public class ChangePassViewModel
    {
        [Required(ErrorMessage = "Password field is required")]
        [MaxLength(20, ErrorMessage = "Too long password")]
        [MinLength(3, ErrorMessage = "Too short password")]
        [DataType(DataType.Password)]
        [Display(Name = "Old Password:")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "NewPassword field is required")]
        [MaxLength(20, ErrorMessage = "Too long password")]
        [MinLength(3, ErrorMessage = "Too short password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public string NewPassword { get; set; }

        [Required]
        [System.ComponentModel.DataAnnotations.Compare(
            "NewPassword", ErrorMessage = "The password and confirmation password are not equals")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password:")]
        public string ConfirmPassword { get; set; }
    }
}