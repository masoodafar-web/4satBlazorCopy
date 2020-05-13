using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models
{
    public class Level : BaseEntity
    {
        [Display(Name = "عنوان")]
        public string Name { get; set; }
        [Display(Name = "عدد")]
        public int Number { get; set; }
        //----------------------------------------------------------------------------------------------------
        //[Display(Name = "اعتبار")]
        //public int Credit { get; set; }

        //[Display(Name = "امتیاز")]
        //public int Point { get; set; }

        //[Display(Name = "حداقل بازه")]
        //public int Min { get; set; }

        //[Display(Name = "حداکثر بازه")]
        //public int Max { get; set; }
        //[ScriptIgnore]
        [JsonIgnore]
        public List<Post> Posts { get; set; }
        public List<Skill> UserSkill { get; set; }
        //public List<Product> Products { get; set; }
    }
}