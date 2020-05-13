using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.ViewModels
{
    public class CommentViewModel
    {

        public Comment Comment { get; set; }

        public string UserId { get; set; }

        public string UserDisplayName { get; set; }

        public string UserImg { get; set; }


    }
}