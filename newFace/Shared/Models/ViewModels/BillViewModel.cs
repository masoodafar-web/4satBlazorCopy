using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.ViewModels
{
    public class BillViewModel
    {

        public int Id { get; set; }

        [Display(Name = "ردیف")]
        public int Row { get; set; }
        [Display(Name = "تاریخ پرداخت")]
        public string Date { get; set; }
        [Display(Name = "مبلغ کل")]
        public double TotalPrice { get; set; }
        [Display(Name = "مجموع تخفیف")]
        public double TotalDiscount { get; set; }
        [Display(Name = "مبلغ نهایی")]
        public double TotalPayment { get; set; }

        public int PaymentStatus { get; set; }

        [Display(Name = "وضعیت پرداخت")]

        public string Status { get; set; }
    }
}