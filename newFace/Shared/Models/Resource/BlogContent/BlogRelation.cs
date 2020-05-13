using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace newFace.Shared.Models.Resource.BlogContent
{
    public class BlogRelation : BaseEntity
    {
        [Display(Name = "نام")]
        public string Name { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CDate { get; set; } = DateTime.Now;
        [Display(Name = "بلاگ")]
        public int BlogId { get; set; }
        [JsonIgnore]
        [ForeignKey("BlogId")]
        public Blog Blog { get; set; }

        [Display(Name = "نوع")]
        public BlogType BlogType { get; set; }

    }

    public enum BlogType
    {
        //0
        [Display(Name = "فایل")]
        File,
        //1
        [Display(Name = "تگ")]
        Tag,
        //2
        [Display(Name = "کلمه کلیدی")]
        KeyWord,
        //3
        [Display(Name = "متا")]
        Meta
    }
}