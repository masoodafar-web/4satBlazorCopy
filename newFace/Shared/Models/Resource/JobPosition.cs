using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.Resource
{
    public class JobPosition
    {
        public int JobPositionId { get; set; }
        [Display(Name = "سمت")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string JobPositionName { get; set; }
        public virtual List<JobResume> JobResumeList { get; set; }
    }
}