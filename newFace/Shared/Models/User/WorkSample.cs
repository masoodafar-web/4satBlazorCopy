namespace newFace.Shared.Models
{
    using newFace.Shared.Models.Resource;
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public class WorkSample: BaseEntity
    {

        [Display(Name = "عنوان")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "تصویر")]
        [DisplayFormat(NullDisplayText = "....")]
        public string ImgAddress { get; set; }

        [Display(Name = "تصویر")]
        [DisplayFormat(NullDisplayText = "....")]
        public string ImgThumbnail { get; set; }

        [Display(Name = "تاریخ پایان")]
        [DisplayFormat(NullDisplayText = "....")]
        public DateTime Date { get; set; }

        [Display(Name = "تویحات")]
        [DisplayFormat(NullDisplayText = "....")]
        public string Desc { get; set; }


        [Display(Name = "دسته بندی")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]

        public virtual Category Category { get; set; }
        [Display(Name = "کاربر")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]

        public virtual ApplicationUser Users { get; set; }
    }
}
