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
        [RegularExpression(@"[a-zA-Z \-]+", ErrorMessage = "Your first name can only contain characters.")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Assign last name.")]
        [RegularExpression(@"[a-zA-Z \-]+", ErrorMessage = "Your last name can only contain characters.")]
        public string LastName { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Choose username.")]
        [RegularExpression(@"^[A-Za-z][A-Za-z0-9]*$", ErrorMessage = "Username must only contain numbers or characters without blankspace.")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Type a password.")]
        [MinLength(6, ErrorMessage = "At least 6 characters.")]
        public string Password { get; set; }

        [Display(Name = "Password")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Your password must be between 6-20 characters.")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password doesn't match!")]
        [RegularExpression(@"^[A-Za-z][A-Za-z0-9]*$", ErrorMessage = "Your password must contain numbers and characters without blank space!")]
        public string PasswordMatch { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Assign description.")]
        public string Description { get; set; }

        [Display(Name = "Age")]
        [Required(ErrorMessage = "Assign age 1-99")]
        [RegularExpression(@"^(0?[1-9]|[1-9][0-9])$", ErrorMessage = "You can't be under 1 years old or over 100 years old, sorry!")]
        public string Age { get; set; }

        [Display(Name = "Sex")]
        [Required(ErrorMessage = "State your gender!")]
        public bool Sex { get; set; }

        [Display(Name = "Interested in men")]
        [Required(ErrorMessage = "Choose male or female!")]
        public bool InterestedMen { get; set; }

        [Display(Name = "Interested in women")]
        [Required(ErrorMessage = "Choose male or female!")]
        public bool InterestedWomen { get; set; }


    };
}
