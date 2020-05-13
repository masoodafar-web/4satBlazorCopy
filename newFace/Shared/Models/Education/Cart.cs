using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.Education
{
    public class Cart: BaseEntity
    {
    
        [Display(Name = "تاریخ ثبت")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public DateTime SubmitDate { get; set; }

        [Display(Name = "نام کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string UserId { get; set; }
        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public int ProductId { get; set; }

        [Display(Name = "نوع سبد")]
        public CartType CartType { get; set; }

        [Display(Name = "کاربر دریافت کننده هدیه")]
        public string RecieverUserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Display(Name = "اعتبار")]
        public double? Credit { get; set; }

        //-------------------------------------------------سهام
        [Display(Name = "درصد سهام درخواستی")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فقط عدد وارد کنید")]

        public int? ShareholderPercent { get; set; }

    }

    public enum CartType
    {
        //0
        [Display(Name = "سبد عادی")]
        Normal,
        //1
        [Display(Name = "سبد هدیه")]
        Gift,
        //2
       [Display(Name = "سبد سهام")]
        Shareholder
    }
}