using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.Education
{
    public class UserAnswer: BaseEntity
    {

        
        [Display(Name = "کاربر")]
        [Required]
        public string UserId { get; set; }

        //----------------------------------//

        [Display(Name = "سوال")]
        [Required]
        public int QuestionId { get; set; }

        //----------------------------------//

        [Display(Name = "پاسخ کاربر")]
        [Required]
        public int Answer { get; set; }
        //----------------------------------//

        [Display(Name = "نمره سوال")]
        public float Score { get; set; }

        //----------------------------------//

        [Display(Name = "شناسه نتیجه آزمون")]
        public int ExamResultId { get; set; }

        [ForeignKey("ExamResultId")]
        public ExamResult ExamResult { get; set; }

        //----------------ForeignKeys-----------------//

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [ForeignKey("QuestionId")]
        public Question Question { get; set; }

    }
}