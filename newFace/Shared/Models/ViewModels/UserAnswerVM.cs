using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.ViewModels
{
    public class UserAnswerVM
    {
        public int QuestionId { get; set; }

        public int ExamId { get; set; }

        public int SelectedChoice { get; set; }
    }
}