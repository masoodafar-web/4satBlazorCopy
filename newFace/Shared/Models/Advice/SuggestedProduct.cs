using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.Advice
{
    public class SuggestedProduct:BaseEntity
    {

        [Display(Name = "اولویت محصولات")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [RegularExpression("^[0-9]*(?:\\.[0-9]*)?$", ErrorMessage = "لطفا فقط عدد وارد کنید")]
        public float ProductPriorityFV { get; set; }
        [Display(Name = "اولویت مهارتها")]
        public int? SkillPriorityFV { get; set; }

        [Display(Name = "خوانده شده")]
        public bool IsReadProduct { get; set; }

        [Display(Name = "محصول")]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [Display(Name = "مهارت")]
        public int SkillCatId { get; set; }
        [ForeignKey("SkillCatId")]
        public virtual Category SkillCategory { get; set; }

        [Display(Name = "هدف")]
        public int? VisionCatId { get; set; }
        [ForeignKey("VisionCatId")]
        public virtual Category VisionCategory { get; set; }

        [Display(Name = "کاربر")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser Users { get; set; }
    }
}