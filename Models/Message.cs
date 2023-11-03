using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedChatWebSite.Models
{
    public class Message
    {
        public DateTime DateCreate {get;set;}
        public string UserId{get;set;}
        public string? Text {get;set;}
    }
}