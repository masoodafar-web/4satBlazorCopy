using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models
{
    public class GiftViewModel
    {
        [Display(Name = "کاربر هدیه دهنده")]
        public string UserSend { get; set; }
        [Display(Name = "کاربر هدیه دهنده")]
        public string UserSendId { get; set; }

        [Display(Name = "ناشر")]
        public string Author { get; set; }

        public string Designer { get; set; }

        public string Teacher { get; set; }


        [Display(Name = "کاربر هدیه گیرنده")]
        public string UserResiv { get; set; }

        [Display(Name = "کاربر هدیه گیرنده")]
        public string UserResivId { get; set; }

        [Display(Name = "هدیه")]
        public int PorductId { get; set; }

        [Display(Name = "هدیه")]
        public string PorductTitle { get; set; }

        [Display(Name = "هدیه")]
        public string PorductImg { get; set; }

        [Display(Name = " تاریخ ارسال هدیه")]
        public DateTime Date { get; set; }

        public bool Status { get; set; }

        public float Rate { get; set; }
    }
}