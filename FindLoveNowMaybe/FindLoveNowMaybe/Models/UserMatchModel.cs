using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repositories;

namespace FindLoveNowMaybe.Models
{
    public class UserMatchModel
    {
        public User User { get; set; }
        public string MatchValue { get; set; }
    }
}