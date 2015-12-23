using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindLoveNowMaybe.Models
{
    public class RegistrationModel
    {
        [Display(Name = "First name")]
        [Required(ErrorMessage = "Assign first name.")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Assign last name.")]
        public string LastName { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Choose username.")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Type a password.")]
        [MinLength(6, ErrorMessage = "At least 6 characters.")]
        public string Password { get; set; }

        [Display(Name = "Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password doesn't match!")]
        public string PasswordMatch { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Assign description.")]
        public string Description { get; set; }

        [Display(Name = "Personal number")]
        [Required(ErrorMessage = "Assign personal number")]
        public string Age { get; set; }

        //[Display(Name = "Picture")]
        //[Required(ErrorMessage = "Assign picture")]
        //public string Picture { get; set; }

        [Display(Name = "Sex")]
        [Required(ErrorMessage = "State your gender!")]
        public bool Sex { get; set; }

        [Display(Name = "Interested in men")]
        [Required(ErrorMessage = "Please choose!")]
        public bool InterestedMen { get; set; }

        [Display(Name = "Interested in women")]
        [Required(ErrorMessage = "Please choose!")]
        public bool InterestedWomen { get; set; }
        

    };
}
