using newFace.Shared.Models.Education;

namespace newFace.Shared.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Exam: BaseEntity
    {

        [Display(Name = "نوع آزمون")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public ExamType ExamType { get; set; }


        [Display(Name = "حداقل درصد قبولی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فقط عدد وارد کنید")]
        public double AcceptancePerecentage { get; set; }
        [Display(Name = "نام محصول")]

        public int ProductId { get; set; }

        [Display(Name = "وضعیت")]
        public ExamStatus Status { get; set; }

        [Display(Name = "زمان آزمون (دقیقه)")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فقط عدد وارد کنید")]
        public int ExamTime { get; set; }

        [JsonIgnore]
        [ForeignKey("ProductId")]
        public  Product Products { get; set; }

        [Display(Name = "طراح آزمون")]
        public int? DesignerId { get; set; }

        [ForeignKey("DesignerId")]
        public virtual Producter Designer { get; set; }

        //[JsonIgnore]
        public  ICollection<Question> Questions { get; set; }
        [JsonIgnore]
        public  ICollection<ExamResult> ExamResults { get; set; }    
    }

    public enum ExamType
    {
        //0
        [Display(Name = "تعیین سطح")]
        LevelDetermine,
        //1
        [Display(Name = "معمولی")]
        Normal
    }

    public enum ExamStatus
    {
        //0
        [Display(Name = "فعال")]
        Active,
        [Display(Name = "غیرفعال")]
        DeActive
    }
}
