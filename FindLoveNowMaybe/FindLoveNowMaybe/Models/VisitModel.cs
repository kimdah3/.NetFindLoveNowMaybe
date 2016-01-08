using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindLoveNowMaybe.Models
{
    public class VisitModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public string Age { get; set; }
        public string Picture { get; set; }
        public bool Sex { get; set; }
        public bool InterestedMen { get; set; }
        public bool InterestedWomen { get; set; }
    }
}