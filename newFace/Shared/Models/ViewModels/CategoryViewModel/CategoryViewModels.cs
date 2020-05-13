using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace newFace.Shared.Models.ViewModels
{
    public class CategoryViewModels
    {

        public int Id { get; set; }

        //-----------------------------------------------------------------------------

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        //-----------------------------------------------------------------------------

        [Display(Name = "نوع دسته")]
        public CategoryTypeEnum CategoryType { get; set; }

        [Display(Name = "نوع دسته")]
        public string CategoryTypeName { get; set; }


        [Display(Name = "نوع مالی")]
        public CategoryFinancialTypeEnum CategoryFinancialType { get; set; }

        [Display(Name = "نوع مالی")]
        public string CategoryFinancialTypeName { get; set; }


      
        [Display(Name = "تصویر")]
        [DataType(DataType.Upload)]
        public string Img { get; set; }

        public IFormFile Image { get; set; }
    }
}