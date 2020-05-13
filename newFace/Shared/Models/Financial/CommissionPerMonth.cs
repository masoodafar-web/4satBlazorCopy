using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.Financial
{
    public class CommissionPerMonth : BaseEntity
    {
        [Display(Name = "کاربر")]
        [Required(ErrorMessage = "لطفا {0} را مشخص کنید")]
        public int UserGeneologyId { get; set; }

        [ForeignKey("UserGeneologyId")]
        public UserGeneology UserGeneology { get; set; }
        //--------------------------------------------------------

        [Display(Name = "مبلغ کارمزد")]
        [Required(ErrorMessage = "لطفا {0} را مشخص کنید")]
        public long AmountFees { get; set; }
       
        //--------------------------------------------------------
        [Display(Name = "ماه")]
        public string Month { get; set; }
        //--------------------------------------------------------
        [Display(Name = "سال")]
        public string Year { get; set; }

        //--------------------------------------------------------
        [Display(Name = "تاریخ")]
        public DateTime CDate { get; set; }
        //--------------------------------------------------------
        [Display(Name = "تاریخ")]
        public DateTime UDate { get; set; }

        //--------------------------------------------------------
        [Display(Name = "نوع کارمزد")]
        public FeeType FeeType { get; set; }


    }
    public enum FeeType{
        //0
        [Display(Name = "غیر مستقیم")]
        IndirectFees,

        //1
        [Display(Name = "مستقیم")]
        DirectFees
    }
    
}