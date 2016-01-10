using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repositories;

namespace FindLoveNowMaybe.Models
{
    public class FriendsInCategoryModel
    {
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
}