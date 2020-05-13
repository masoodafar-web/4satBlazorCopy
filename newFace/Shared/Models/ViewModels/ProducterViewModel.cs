using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using newFace.Shared.Models.Education;

namespace newFace.Shared.Models.ViewModels
{
    public class ProducterViewModel
    {
  
        public int Id { get; set; }
        //-----------------------------------------------------------
        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string FullName { get; set; }
        //-----------------------------------------------------------
        [Display(Name = "تصویر")]
        public string Img { get; set; }
        //-----------------------------------------------------------
        [DataType(DataType.MultilineText)]
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        //-----------------------------------------------------------
        [Display(Name = "سمت")]

        public EnumProducterType EnumType { get; set; }
    }
}