using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using newFace.Shared.Models.Resource;

namespace newFace.Shared.Models.Education
{
    public class ExamResult: BaseEntity
    {
        [Display(Name = "کاربر")]
        [Required]
        public string UserId { get; set; }
        [Display(Name = "آزمون")]
       // [Required]
        public int? ExamId { get; set; }
        [Display(Name = "تعداد پاسخ صحیح")]
        [Required]
        public int CorrectAnswersCount { get; set; }
        [Display(Name = "درصد پاسخ صحیح")]
        [Required]
        public double CorrectAnswerPercent { get; set; }

        [Display(Name = "نمره پاسخ صحیح")]
        [Required]
        public double CorrectAnswerScore { get; set; }

        [Display(Name = "نتیجه ازمون")]
        [Required]
        public StatusExam StatusExam { get; set; }

        [Display(Name = "تاریخ آزمون")]
        public DateTime ExamDateTime { get; set; }


        [Display(Name = "نوع آزمون")]
        public Enums.StatusTypeQuestion ExamType { get; set; }

        public int? ExamResultId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public List<UserAnswer> UserAnswersLists { get; set; }

        [ForeignKey("ExamId")]
        public Exam Exam { get; set; }
    }

    public enum StatusExam
    {
        //0
        
        [Display(Name = "مردود")]
        Failed
        ,
        //1
        [Display(Name = "قبول")]
        Pass,
    }
}