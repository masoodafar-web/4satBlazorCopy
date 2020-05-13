using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.ViewModels.CategoryViewModel
{
    public class CategoryLevelViewModel
    {
        public int Id { get; set; }
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
        [Display(Name = "نام دسته")]
        public string CategoryName { get; set; }
    }
}