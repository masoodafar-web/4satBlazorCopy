using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.SocialNetwork
{
    public class ChatLog
    {
        public ChatLog()
        {
            Date = DateTime.Now;
        }



        public int Id { get; set; }

        public string UserId { get; set; }

        public string ContextConnectionId { get; set; }

        public bool IsConnected { get; set; }

        public bool IsReConnected { get; set; }

        public bool IsDisconnected { get; set; }

        public DateTime Date { get; set; }
    }
}