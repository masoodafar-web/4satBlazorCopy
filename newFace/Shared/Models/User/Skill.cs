namespace newFace.Shared.Models
{
    using newFace.Shared.Models.Resource;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Skill : BaseEntity
    {

        [Display(Name = "درصد")]
        [Range(minimum: 0, maximum: 100, ErrorMessage = "درصد باید بین 0 تا 100 باشد")]
        public float Percent { get; set; }
        //------------------------------------------------------------
        //به دلیل این که برای نمایش اهداف در صفحه های سایت به این فیلد نیاز داریم حذف گردید
        //[JsonIgnore]
        [Display(Name = "نوع مهارت")]
        public SkillType SkillType { get; set; }

        public bool IsUpdate { get; set; }
        public bool Lvl1 { get; set; }
        public bool Lvl2 { get; set; }
        public bool Lvl3 { get; set; }
        public bool Lvl4 { get; set; }

        [Display(Name = "اعتبار")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public long Credit { get; set; }

        [Display(Name = "امتیاز")]
        public long Point { get; set; }
        //------------------------------------------------------------
        //به دلیل این که برای نمایش اهداف در صفحه های سایت به این فیلد نیاز داریم حذف گردید
        //[JsonIgnore]
        [Display(Name = "دسته بندی")]
        public int CategoryId { get; set; }

        [JsonIgnore]
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        //------------------------------------------------------------
        [JsonIgnore]
        [Display(Name = "کاربر")]
        public string UserId { get; set; }

        [JsonIgnore]
        [ForeignKey("UserId")]
        public virtual ApplicationUser Users { get; set; }

        //------------------------------------------------------------
        public bool IsPassRatingExam { get; set; }
        public int LevelId { get; set; }
        [ForeignKey("LevelId")]
        public Level Level { get; set; }



    }
    public enum SkillType
    {
        //0
        [Display(Name = "اکتسابی")]
        Adventitious,
        //1
        [Display(Name = "انتسابی")]
        Assigned
    }
}
