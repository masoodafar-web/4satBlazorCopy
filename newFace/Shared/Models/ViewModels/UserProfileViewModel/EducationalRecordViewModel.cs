using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using newFace.Shared.Models.Resource;

namespace newFace.Shared.Models.ViewModels
{
    public class EducationalRecordViewModel
    {

        public int Id { get; set; }

        [Display(Name = "نام رشته")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, Int32.MaxValue, ErrorMessage = "لطفا {0} را انتخاب کنید یا ثبت جدید انجام دهید ")]   
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا {0} را انتخاب کنید یا ثبت جدید انجام دهید ")]
        public int FieldId { get; set; }

        [Display(Name = "رشته")]
        [DisplayFormat(NullDisplayText = "....")]
        public string FieldName { get; set; }

        [Display(Name = "نام گرایش")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, Int32.MaxValue, ErrorMessage = "لطفا {0} را انتخاب کنید یا ثبت جدید انجام دهید ")]   
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا {0} را انتخاب کنید یا ثبت جدید انجام دهید ")]
        public int OrientationId { get; set; }

        [Display(Name = "گرایش")]
        [DisplayFormat(NullDisplayText = "....")]
        public string OrientationName { get; set; }

        [Display(Name = "نام دانشگاه")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, Int32.MaxValue, ErrorMessage = "لطفا {0} را انتخاب کنید یا ثبت جدید انجام دهید ")]   
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا {0} را انتخاب کنید یا ثبت جدید انجام دهید ")]

        public int UniversityId { get; set; }

        [Display(Name = "نام دانشگاه")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string University { get; set; }

        [Display(Name = "معدل")]
        [DisplayFormat(NullDisplayText = "....")]
        [Range(0, 20, ErrorMessage = "فیلد {0} باید بین 0 تا 20 باشد")]
        public double? Average { get; set; }

        [Display(Name = "مقطع تحصیلی")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public Enums.EducationalStatus Grade { get; set; }

        [Display(Name = "مقطع تحصیلی")]
        public string GradeName { get; set; }

        [Display(Name = "توضیحات")]
        [DisplayFormat(NullDisplayText = "....")]
        public string Desc { get; set; }

        [Display(Name = "تاریخ شروع")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string StartDate { get; set; }

        [Display(Name = "تاریخ پایان")]
        [DisplayFormat(NullDisplayText = "....")]
        public string EndDate { get; set; }

        public string UserId { get; set; }



    }

    public class ResultEducationalRecord : Result
    {
        public EducationalRecordViewModel EducationalRecordViewModel { get; set; }
        public List<EducationalRecordViewModel> EducationalRecordViewModels { get; set; }
    }
    public class RequestEducationalRecord : Request
    {
        public EducationalRecordViewModel EducationalRecordViewModel { get; set; }
        public List<EducationalRecordViewModel> EducationalRecordViewModels { get; set; }
    }
}