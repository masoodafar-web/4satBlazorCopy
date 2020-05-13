using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using newFace.Shared.Models.Education;

namespace newFace.Shared.Models.ViewModels
{
    public class ExamResultVM
    {
        public ExamResult ExamResult { get; set; }

        public Product Product { get; set; }

        public ProductScale ProductScale { get; set; }
    }
}