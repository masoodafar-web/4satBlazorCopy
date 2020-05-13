using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace newFace.Shared.Models.Resource
{
    public class Company
    {
        public int CompanyId { get; set; }
        [Display(Name = "نام شرکت")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CompanyName { get; set; }
        [JsonIgnore]
        public virtual List<JobResume> JobResumeList { get; set; }
    }
}