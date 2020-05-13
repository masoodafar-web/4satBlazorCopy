using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.User
{
    public class UserPushToken : BaseEntity
    {
        public string UserId { get; set; }

        public string Token { get; set; }
    }
}