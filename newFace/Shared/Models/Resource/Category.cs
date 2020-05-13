using newFace.Shared.Models.Advice;
using newFace.Shared.Models;

namespace newFace.Shared.Models
{
    using newFace.Shared.Models.General;
    using newFace.Shared.Models.Resource;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Category")]
    public class Category: BaseEntity
    {


        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        //-----------------------------------------------------------------------------
        [UIHint("FileUpload")]
        [Display(Name = "تصویر")]
        [DataType(DataType.Upload)]
        public string Img { get; set; }

        //-----------------------------------------------------------------------------

        [Display(Name = "نوع دسته")]
        public CategoryTypeEnum CategoryType { get; set; }


        [Display(Name = "نوع مالی")]
        public CategoryFinancialTypeEnum? CategoryFinancialType { get; set; }

        //-----------------------------------------------------------------------------

        [JsonIgnore]
        public virtual ICollection<ProductScale> ProductScale { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProjectSetting> ProjectSettings { get; set; }
        [JsonIgnore]
        public virtual ICollection<Skill> Skill { get; set; }

        [JsonIgnore]
        public virtual ICollection<Vision> Vision { get; set; }

        [JsonIgnore]
        public virtual ICollection<WorkSample> WorkSamples { get; set; }
        [JsonIgnore]
        [InverseProperty("Parent")]
        public virtual ICollection<Category_Category> Parents { get; set; }
        [JsonIgnore]
        [InverseProperty("Children")]
        public virtual ICollection<Category_Category> childes { get; set; }
        [JsonIgnore]
        [InverseProperty("Category")]
        public virtual ICollection<CategoryLevel> CategoryLevels { get; set; }
        [JsonIgnore]
        [InverseProperty("SkillCategory")]
        public virtual ICollection<SuggestedProduct> SuggestedProductSkill { get; set; }
        [JsonIgnore]
        [InverseProperty("VisionCategory")]
        public virtual ICollection<SuggestedProduct> SuggestedProductVision { get; set; }

    }

    public enum CategoryTypeEnum
    {
        //0
        [Display(Name = "موضوع")]
        Subject=0,
        //1
        [Display(Name = " شغل")]
        Job=1,
        //2
        [Display(Name = " مهارت")]
        Skill=2,
        //3
        [Display(Name = " مالی")]
        Financial=3,
        //4
        [Display(Name = " بلاگ")]
        BlogCategory=4,
        //5
       
        [Display(Name = "ارتقای مهارت")]
        SkillUpgrade=5
    }

    public enum CategoryFinancialTypeEnum
    {
        //0
        [Display(Name = " ندارد")]
        Null = 0,
        //1
        [Display(Name = "بورس")]
        Exchange = 1,
        //2
        [Display(Name = " بانک")]
        Bank=2,
        //3
        [Display(Name = " بیمه")]
        Insurance = 3


    }
}
