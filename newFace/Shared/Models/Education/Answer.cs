using Newtonsoft.Json;

namespace newFace.Shared.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Answer: BaseEntity
    {
     
        [Display(Name = "متن گزینه")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Text { get; set; }

        [Display(Name = "سوال")]
        public int QuestionId { get; set; }


        [Display(Name = "گزینه صحیح است؟")]
        [JsonIgnore]
        public bool CorrectAnswer { get; set; }

       
        [ForeignKey("QuestionId")]
        public  Question Questions { get; set; }

    }
}
