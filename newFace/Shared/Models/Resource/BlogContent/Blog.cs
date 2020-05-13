using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using newFace.Shared.Models.Resource.BlogContent;
using Newtonsoft.Json;

namespace newFace.Shared.Models.Resource
{
    public class Blog: BaseEntity
    {
        public Blog()
        {
            CDate = DateTime.Now;
        }


        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        //-------------------------------------------------------
        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Img { get; set; }
        //-------------------------------------------------------
        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ShortDesc { get; set; }
        //-------------------------------------------------------
        [Display(Name = "تاریخ ویرایش")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime? EDate { get; set; }
        //-------------------------------------------------------
        [Display(Name = "تعداد تماشا")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long SeenCount { get; set; }

        //-------------------------------------------------------
        [Display(Name = "محتوای اصلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.MultilineText)]
        public string Editor { get; set; }
        //-------------------------------------------------------
        [Display(Name = "تاریخ ایجاد")]

        public DateTime CDate { get; set; }
        //-------------------------------------------------------
        [JsonIgnore]
        public List<BlogCategory> BlogCategories { get; set; }
        //-------------------------------------------------------
        [JsonIgnore]
        public List<BlogRelation> BlogRelations { get; set; }
        
         //-------------------------------------------------------
       // public List<BlogRelation> Tags { get; set; }
        
         //-------------------------------------------------------
       // public List<BlogRelation> KeyWords { get; set; }
        
         //-------------------------------------------------------
       // public List<BlogRelation> MetaTags { get; set; }
        //-------------------------------------------------------

        public List<Comment> Comments { get; set; }
       


    }
}