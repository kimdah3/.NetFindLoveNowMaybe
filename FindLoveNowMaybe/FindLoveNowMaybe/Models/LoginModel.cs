using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FindLoveNowMaybe.Models
{
    public class LoginModel
    {
        [Display(Name = "Username", ResourceType = typeof(FindLoveNowMaybe.Resources.LoginStrings))]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username can not be empty.")]
        public string UserName { get; set; }

        [Display(Name = "Password", ResourceType = typeof(FindLoveNowMaybe.Resources.LoginStrings))]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password can not be empty.")]
        public string Password { get; set; }

        //Felmeddelande vid login
        public string ErrorMessage { get; set; }
    }
}