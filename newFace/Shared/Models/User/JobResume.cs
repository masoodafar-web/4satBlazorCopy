namespace newFace.Shared.Models
{
    using newFace.Shared.Models.Resource;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   


    public partial class JobResume: BaseEntity
    {

        [Display(Name = "عنوان")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string JobTitle { get; set; }
        [Display(Name = "تاریخ شروع")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "تاریخ پایان")]
        [DisplayFormat(NullDisplayText = "....")]
        [DataType(DataType.Date)]

        public DateTime? EndDate { get; set; }
        [Display(Name = "توضیحات")]
        [DisplayFormat(NullDisplayText = "....")]
        public string Desc { get; set; }

        [Display(Name = "سمت")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int JobPositionId { get; set; }
        [JsonIgnore]
        [ForeignKey("JobPositionId")]
        public virtual Resources JobPosition { get; set; }
        //public int CatId { get; set; }
        //[ForeignKey("CatId")]
        //public virtual Category Category { get; set; }
        [Display(Name = "نام شرکت")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Resources Company { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser Users { get; set; }
    }
}
