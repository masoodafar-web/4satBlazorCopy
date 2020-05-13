using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace newFace.Shared.Models.Resource
{
    public class Category_Category : BaseEntity
    {
  
        //درصدی که از شغل(هدف) یا ارتقای مهارت (هدف) بالا سری خود کامل میشود
        [Display(Name = "درصد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(0, 100, ErrorMessage = "عدد وارد شده بین 0 تا 100 باید باشد")]
        public int Percent { get; set; }

        // [اولویت هر مهارت در شغل خودش  یا  [شغل در موضوع خودش
        [Display(Name = "اولویت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Priority { get; set; }

        [Display(Name = "پدر")]
        public int? ParentCatId { get; set; }
        //---------------------------------------------------------
        [JsonIgnore]
        [ForeignKey("ParentCatId")]
        public virtual Category Parent { get; set; }
        //---------------------------------------------------------

        [Display(Name = "فرزند")]
        public int ChildrenCatId { get; set; }
        //---------------------------------------------------------

        [JsonIgnore]
        [ForeignKey("ChildrenCatId")]
        public virtual Category Children { get; set; }
        //---------------------------------------------------------

    }
}
