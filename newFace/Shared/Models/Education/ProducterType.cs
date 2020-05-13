using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.Education
{
    public class ProducterType:BaseEntity
    {
        [Display(Name = "شناسه")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public int ProducterId { get; set; }

        //-----------------------------------------------------------
        [Display(Name = "نوع")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public EnumProducterType Type { get; set; }

        [ForeignKey("ProducterId")]
        public Producter Producter { get; set; }
    }
    public enum EnumProducterType
    {
        //0
        [Display(Name = "مترجم")]
        Translator,
        //1
        [Display(Name = "ناشر")]
        Publisher,
        //2
        [Display(Name = "گوینده")]
        Announcer,
        //3
        [Display(Name = "مدرس")]
        Teacher,
        //4
        [Display(Name = "طراح سوال")]
        ExamDesigner,
        //5
        [Display(Name = "نویسنده")]
        Author

    }
}