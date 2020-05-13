using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.ViewModels
{
    public class WorkSampleViewModel
    {
        public int Id { get; set; }

        [Display(Name = " عنوان کار")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        [Display(Name = "تصویر")]
        [DisplayFormat(NullDisplayText = "....")]
        public string ImgAddress { get; set; }

        [Display(Name = "تصویر")]
        [DisplayFormat(NullDisplayText = "....")]
        public string ImgThumbnail { get; set; }

        [Display(Name = "دسته بندی")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, Int32.MaxValue, ErrorMessage = "لطفا {0} را انتخاب کنید یا ثبت جدید انجام دهید ")]   
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا {0} را انتخاب کنید یا ثبت جدید انجام دهید ")]
        public int CategoryId { get; set; }

        [Display(Name = "دسته بندی")]
        public string CategoryName { get; set; }

        [Display(Name = "تاریخ پایان")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Date { get; set; }

        [Display(Name = "توضیحات")]
        [DisplayFormat(NullDisplayText = "....")]
        public string Desc { get; set; }

        [Display(Name = "ایدی کاربر")]
        [DisplayFormat(NullDisplayText = "....")]
     public string UserId { get; set; }
        [Display(Name = "نام کاربر")]
        public string UserName { get; set; }

    }
   
}