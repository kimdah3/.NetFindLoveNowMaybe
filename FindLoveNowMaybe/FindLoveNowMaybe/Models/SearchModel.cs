using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Repositories;

namespace FindLoveNowMaybe.Controllers
{
    public class SearchModel
    {
        [Display(Name = "Input first name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Empty search field will return all users!")]
        public string SearchText { get; set; }
        public List<User> FoundUsers { get; set; }
    }
}