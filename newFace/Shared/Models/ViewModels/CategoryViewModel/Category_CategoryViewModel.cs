using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace newFace.Shared.Models.ViewModels.CategoryViewModel
{
    public class Category_CategoryViewModel
    {
        public int Id { get; set; }
        //درصدی که از شغل(هدف) یا ارتقای مهارت (هدف) بالا سری خود کامل میشود
        [Display(Name = "درصد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(0, 100, ErrorMessage = "عدد وارد شده بین 0 تا 100 باید باشد")]
        public int Percent { get; set; }

        // [اولویت هر مهارت در شغل خودش  یا  [شغل در موضوع خودش
        [Display(Name = "اولویت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Priority { get; set; }
        //---------------------------------------------------------

        [Display(Name = "نوع دسته")]
        public CategoryTypeEnum CategoryType { get; set; }
        //---------------------------------------------------------

        [Display(Name = "پدر")]
        public int? ParentCatId { get; set; }
        //---------------------------------------------------------
        [Display(Name = "نام پدر")]

        public string ParentCatName { get; set; }
        //---------------------------------------------------------
       
        [Display(Name = "فرزند")]
        public int ChildrenCatId { get; set; }
        //---------------------------------------------------------
       [Display(Name = "نام فرزند")]
        public string ChildrenCatName { get; set; }
        //---------------------------------------------------------
   
        public bool HasChild { get; set; }
        //---------------------------------------------------------
    }
}