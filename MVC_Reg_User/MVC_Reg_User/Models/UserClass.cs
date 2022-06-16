using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC_Reg_User.Models
{
    public class UserClass
    {
        [Required(ErrorMessage ="Enter the UserName: ")]
        [Display(Name = "UserName: ")]
        [StringLength(maximumLength:8, MinimumLength = 3, ErrorMessage = "UserName Length Must be Max 7 $ Min 3")]
        public string username { get; set; }

        [Required(ErrorMessage = "What is your role? Admin or Barista?")]
        [Display(Name = "Role: ")]
        public string role { get; set; }

        [Required(ErrorMessage = "Enter The Password: ")]
        [Display(Name = "Password: ")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}