using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.ViewModels
{
    public class JobResumeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "عنوان کار")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string JobTitle { get; set; }

        [Display(Name = "تاریخ شروع")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string StartDate { get; set; }

        [Display(Name = "تاریخ پایان")]
        [DisplayFormat(NullDisplayText = "....")]
        public string EndDate { get; set; }

        [Display(Name = "توضیحات")]
        [DisplayFormat(NullDisplayText = "....")]
        public string Desc { get; set; }

        [Display(Name = "کد سمت")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, Int32.MaxValue, ErrorMessage = "لطفا {0} را انتخاب کنید یا ثبت جدید انجام دهید ")]   
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا {0} را انتخاب کنید یا ثبت جدید انجام دهید ")]
        public int JobPositionId { get; set; }

        [Display(Name = "سمت")]
        [DisplayFormat(NullDisplayText = "....")]
        public string JobPosition { get; set; }

        [Display(Name = "نام شرکت")]
        [DisplayFormat(NullDisplayText = "....")]
        public string Company { get; set; }

        [Display(Name = "نام شرکت")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, Int32.MaxValue, ErrorMessage = "لطفا {0} را انتخاب کنید یا ثبت جدید انجام دهید ")]   
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا {0} را انتخاب کنید یا ثبت جدید انجام دهید ")]
        public int CompanyId { get; set; }
        
        public string UserId { get; set; }

    }
    
}