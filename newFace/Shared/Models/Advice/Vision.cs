using newFace.Shared.Models;
using newFace.Shared.Models.Resource;

namespace newFace.Shared.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Vision")]
    public partial class Vision : BaseEntity
    {
        [Display(Name = "درصد پیشرفت")]
        public float Percent { get; set; }

        [Display(Name = "بروز است؟")]
        public bool IsUpdate { get; set; }


        [JsonIgnore]
        [Display(Name = "کاربر")]
        [Required]
        public string UserId { get; set; }

        [JsonIgnore]
        [ForeignKey("UserId")]
        public virtual ApplicationUser Users { get; set; }

        [Display(Name = "دسته بندی")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public Enums.VisionStatus VisionStatus { get; set; }
        public VisionType VisionType { get; set; }

        [JsonIgnore]
        //اولویتی که بر اساس آن درجه اهمیت نسبت به تعداد مهارت های وارد شده یا علاقه کاربر به آن مهارت ها و در نتیجه علاقه به آن هدف قرار داده میشود
        [Display(Name = "اولویت محصولات")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [Range(float.MinValue, float.MaxValue, ErrorMessage = "لطفا فقط عدد وارد کنید")]
        public float Priority { get; set; }

        [JsonIgnore]
        [Display(Name = "زمان آزاد")]
        public int? FreeTime { get; set; }

        [JsonIgnore]
        [Display(Name = "مبلغ پس انداز")]
        public long? AmountOfSavings { get; set; }

        [JsonIgnore]
        [Display(Name = "درآمد ماهانه")]
        public long? Income { get; set; }

        [JsonIgnore]
        [Display(Name = "سرمایه اولیه")]
        public long? InitialInvestment { get; set; }

        [JsonIgnore]
        [Display(Name = "هدف درآمدی")]
        public long? EarningsGoal { get; set; }

        [JsonIgnore]
        [Display(Name = "بازه زمانی ماهانه")]
        public long? MonthlyInterval { get; set; }

        //---------------------------------------------------------------
        [JsonIgnore]
        [Display(Name = "سرپرست")]
        public string DevelopmentManagerUserId { get; set; }

        [ForeignKey("DevelopmentManagerUserId")]
        public virtual ApplicationUser DevelopmentManager { get; set; }
        //----------------------------------------------------------------
        [JsonIgnore]
        [Display(Name = "مدیر آموزش")]
        public string DirectorOfEducationUserId { get; set; }

        [ForeignKey("DirectorOfEducationUserId")]
        public virtual ApplicationUser DirectorOfEducation { get; set; }
        //-----------------------------------------------------------------
    }

    public enum VisionType
    {
        //0
        [Display(Name = "کاری")]
        Work = 0,
        //1
        [Display(Name = " فردی")]
        Person = 1,
        //2
        [Display(Name = "مالی")]
        Financial = 2,

    }
}
