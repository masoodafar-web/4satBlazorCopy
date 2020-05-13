using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.Financial
{
    public class SellPerMonth : BaseEntity
    {

        [Display(Name = "کاربر")]
        [Required(ErrorMessage = "لطفا {0} را مشخص کنید")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }      
        //----------------------------------------------------------//
        [Display(Name = "مبلغ فروش خود")]
        [Required(ErrorMessage = "لطفا {0} را مشخص کنید")]
        public long SellYourself { get; set; }
        //----------------------------------------------------------//
        [Display(Name = "حجم فروش باینر")]
        public long BinarySalesVolume { get; set; }
        //----------------------------------------------------------//
        [Display(Name = "حجم فروش فرار پله")]
        public long BreakSalesVolume { get; set; }
        //-------------------------------------------------------

        [Display(Name = "مبلغ فروش مجموعه")]
        [Required(ErrorMessage = "لطفا {0} را مشخص کنید")]
        public long SellSets { get; set; }
        //--------------------------------------------------------
        [Display(Name = "ماه")]
        public string Month { get; set; }
        //--------------------------------------------------------
        [Display(Name = "سال")]
        public string Year { get; set; }

        //--------------------------------------------------------
        [Display(Name = "تاریخ")]
        public DateTime CDate { get; set; }
        //----------------------------------------------------------//
        [Display(Name = "نوع سیستم")]
        public SystemType SystemType { get; set; }
        //--------------------------------------------------------
        //public List<CommissionHistory> CommissionHistories { get; set; }

    }


}