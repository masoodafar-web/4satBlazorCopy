using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace newFace.Shared.Models.ViewModels
{
    public class QuestionAnswerViewModel
    {
        public Question Question { get; set; }

        List<Answer> _A=new List<Answer>();
        public List<Answer> AnswerList{get => _A; set => value = _A;}


    }
}