using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repositories;


namespace FindLoveNowMaybe.Models
{
    public class FriendsModel
    {
        public List<User> Friends { get; set; }
        public List<FriendsInCategoryModel> FriendsInCategoryModels { get; set; }

    }
}