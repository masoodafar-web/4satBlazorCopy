using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.General
{
    public class ProjectSetting : BaseEntity
    {
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        //-------------------------------------------------------

        [Display(Name = "لوگو")]
        public string Logo { get; set; }
        //-------------------------------------------------------
        [Display(Name ="دسته بندی پیشفرض کاربران")]
        public int DefultCategoryId { get; set; }
        [ForeignKey("DefultCategoryId")]
        public Category Category { get; set; }
        //-------------------------------------------------------
        [DefaultValue(15)]
        public int MaxCapacity { get; set; }

    }
}