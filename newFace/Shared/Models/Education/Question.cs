using newFace.Shared.Models.Resource;

namespace newFace.Shared.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   

    public partial class Question: BaseEntity
    {
      
        [Display(Name = "آزمون")]
        public int ExamId { get; set; }

        [Display(Name = "متن سوال")]
        [StringLength(500)]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [DataType(DataType.MultilineText)]
        public string QuestionText { get; set; }

        [Display(Name = "فایل سوال")]
        public string QuesFile { get; set; }


       
        public Questiontype QuestionType { get; set; }

        [Display(Name = "نوع سوال")]
        public Enums.StatusTypeQuestion StatusType { get; set; }

        public  ICollection<Answer> Answers { get; set; }
        [ForeignKey("ExamId")]
        public  Exam Exams { get; set; }

        [Display(Name = "بارم سوال")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]

        public float Score { get; set; }

        public enum Questiontype
        {
            //-1
            //[Display(Name = "چند انتخابی")]
            //CheckBox,
            //0
            [Display(Name = "تستی")]
            RadioButton,
            //1
            [Display(Name = "تشریحی")]
            TextArea
        }
       
    }
}
