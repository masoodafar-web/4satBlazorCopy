using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace newFace.Shared.Models.Resource.BlogContent
{
    public class BlogCategory : BaseEntity
    {
        [Display(Name = "بلاتگ")]
        public int BlogId { get; set; }
        [JsonIgnore]
        [ForeignKey("BlogId")]
        public Blog Blog { get; set; }

        [Display(Name = "دسته بندی")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}