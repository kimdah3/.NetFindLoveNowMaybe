using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindLoveNowMaybe.Models
{
    public class MessageModel  
    {
        public int SenderID { get; set; } 
        public int RecieverID { get; set; }
        public string SenderFullName { get; set; }
        public string Message { get; set; }
    }
}