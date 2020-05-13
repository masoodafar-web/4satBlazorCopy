using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.Financial
{
    public class Commission: BaseEntity
    {
        public Commission()
        {
            Datetime = DateTime.Now;
        }


        [Required(ErrorMessage = "لطفا {0} را مشخص کنید")]
        public string UserId { get; set; }

        //[ForeignKey("UserId")]
        //public ApplicationUser User { get; set; }
        //-------------------------------------------------------
        [Required(ErrorMessage = "لطفا {0} را مشخص کنید")]
        [Display(Name = "نام زیرمجموعه")]
        public string SubsetId { get; set; }

        [ForeignKey("SubsetId")]
        public ApplicationUser SubsetUser { get; set; }

        //-------------------------------------------------------
        [Required(ErrorMessage = "لطفا {0} را مشخص کنید")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        //--------------------------------------------------------
        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را مشخص کنید")]
        public double Amount { get; set; }
        //--------------------------------------------------------
        [Display(Name = "درصد")]
        [Range(1, 100, ErrorMessage = "برای {0} از اعداد مناسب استفاده کنید")]
        public int Percent { get; set; }
        //--------------------------------------------------------
        [Display(Name = "کارمزد")]
        public double Fee { get; set; }

        [Display(Name = "کارمزد زیرمجموعه ها")]
        public double FeeSubsets { get; set; }
        //--------------------------------------------------------
        [Display(Name = "تاریخ")]
        public DateTime Datetime { get; set; }

        //--------------------------------------------------------

        [Display(Name = " نوع پورسانت")]
        public CommissionTypeEnum CommissionType { get; set; }

    }

    public enum CommissionTypeEnum
    {
        //0
        [Display(Name = "سهام")]
        Shareholder,
        //1
        [Display(Name = "زیرمجموعه")]
        SubsetId,
    }
}