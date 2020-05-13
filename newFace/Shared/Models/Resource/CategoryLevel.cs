using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace newFace.Shared.Models.Resource
{
    public class CategoryLevel:BaseEntity
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        [Display(Name = "کمترین")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Min { get; set; }
        [Display(Name = "بیشترین")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Max { get; set; }
        public int CategoryId { get; set; }
        [JsonIgnore]
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}