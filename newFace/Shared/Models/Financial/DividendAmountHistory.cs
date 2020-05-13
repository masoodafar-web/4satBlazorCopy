using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.Financial
{
    public class DividendAmountHistory : BaseEntity
    {


        [Display(Name = "کاربر")]
        [Required(ErrorMessage = "لطفا {0} را مشخص کنید")]
        public string UserId { get; set; }

        //[ForeignKey("UserId")]
        //public ApplicationUser User { get; set; }
        //-------------------------------------------------------

        [Display(Name = "مبلغ اصلی")]
        [Required(ErrorMessage = "لطفا {0} را مشخص کنید")]
        public long OriginalAmount { get; set; }
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
        //[Display(Name = "نوع سیستم")]
        //public SystemType SystemType { get; set; }
        //--------------------------------------------------------
        public List<CommissionHistory> CommissionHistories { get; set; }

    }


}