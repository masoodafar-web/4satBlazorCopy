using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.Resource
{
    public class AppUpdate:BaseEntity
    {
        [Display(Name = "آدرس فایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UpdateAddress { get; set; }
        [Display(Name = "ورژن")]
        [Range(1,Int32.MaxValue, ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Vresion { get; set; }
        [Display(Name = "ضروری؟")]
        public bool Isforce { get; set; }
        [Display(Name = "پیام بروزرسانی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UpdateMessage { get; set; }

        //اگر تو بیس انتیتی تاریخ ساخت اومد از اینجا برش دارید
        public DateTime CDate { get; set; }
    }
}