using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.ViewModels
{
    public class ChatContactViewModels
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string NickName { get; set; }

        public int UnSeenCount { get; set; }

        public string Avatar { get; set; }

        public double Credit { get; set; }

        public Chat Chat { get; set; }

        public DateTime UpdateDate { get; set; }

    }
}