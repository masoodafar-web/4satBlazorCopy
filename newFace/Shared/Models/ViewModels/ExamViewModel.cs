using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.ViewModels
{
    public class ExamViewModel
    {
        public Exam Exam { get; set; }

        List<QuestionAnswerViewModel> _l = new List<QuestionAnswerViewModel>();
        public List<QuestionAnswerViewModel> Questions { get { return _l; } set { value=_l; } }
    }
}