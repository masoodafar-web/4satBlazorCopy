using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using newFace.Shared.Models;
using Microsoft.AspNetCore.Identity;


namespace newFace.Server.Utility
{


    public class HelperClass
    {
        private UserManager<ApplicationUser> UserManager;

   
        public Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }
        public bool IsConfirmEmail(string UserId)
        {
            if (!String.IsNullOrEmpty(UserId) && !String.IsNullOrWhiteSpace(UserId))
            {
                switch (UserManager.IsEmailConfirmedAsync(UserManager.FindByIdAsync(UserId).Result).Result)
                {
                    case false:
                        return false;
                    case true:
                        return true;
                    default:
                        return false;
                }
            }
            else
            {
                return false;
            }

        }

    }
    public class Video
    {
        public string title { get; set; }
        public string source { get; set; }
        public string poster { get; set; }
    }
}