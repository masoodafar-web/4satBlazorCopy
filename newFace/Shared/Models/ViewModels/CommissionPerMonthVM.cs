using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using newFace.Shared.Models.Financial;

namespace newFace.Shared.Models.ViewModels
{
    public class CommissionPerMonthVM
    {

        public int Id { get; set; }


        [Display(Name = "کاربر")]
       // [Required(ErrorMessage = "لطفا {0} را مشخص کنید")]
        public string UserGeneologyId { get; set; }

       
        //--------------------------------------------------------

        [Display(Name = "مبلغ کارمزد")]
        //[Required(ErrorMessage = "لطفا {0} را مشخص کنید")]
        public long AmountFees { get; set; }

        //--------------------------------------------------------
        [Display(Name = "ماه")]
        public string Month { get; set; }
        //--------------------------------------------------------
        [Display(Name = "سال")]
        public string Year { get; set; }

        //--------------------------------------------------------
        [Display(Name = "تاریخ")]
        public string CDate { get; set; }
       
        //--------------------------------------------------------
        [Display(Name = "نوع کارمزد")]
        public FeeType FeeType { get; set; }
      
    }
}