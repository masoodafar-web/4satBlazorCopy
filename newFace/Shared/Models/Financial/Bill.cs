using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace newFace.Shared.Models.Financial
{


    public class Bill: BaseEntity
    {
        

        [Display(Name = "نام کاربر")]
        public string UserId { get; set; }
        
        
        //----------------------------------------------------------//

        [Display(Name = "مجموع مبلغ")]
        public double TotalUnitPrice { get; set; }

        //----------------------------------------------------------//

        [Display(Name = "مجموع مبلغ تخفیف")]
        public double TotalDiscount { get; set; }

        //----------------------------------------------------------//


        [Display(Name = "مبلغ نهایی")]
        public double TotalPrice { get; set; }

        //----------------------------------------------------------//

        [Display(Name = "تاریخ ایجاد")]
        public DateTime CDate { get; set; }

        //----------------------------------------------------------//

        [Display(Name = "تاریخ پرداخت")]
        public DateTime? PaymentDate { get; set; }

        //----------------------------------------------------------//

        [Display(Name = "شماره ارجاع")]
        public string RefId { get; set; }

        //----------------------------------------------------------//

        [Display(Name = "وضعیت پرداخت")]
        public int Status { get; set; }

        //----------------------------------------------------------//

        [Display(Name = "نوع پرداخت")]
        public BillType BillType { get; set; }

        //----------------------------------------------------------//
        

        //[Display(Name = "نوع خرید")]
        //public BuyType BuyType { get; set; }

        //----------------------------------------------------------//
        [ForeignKey("UserId")]
        public virtual ApplicationUser Users { get; set; }
      
        //----------------------------------------------------------//



    }

    //public enum BuyType
    //{
    //    //0
    //    [Display(Name = "خرید محصول")]
    //    BuyProduct,

    //    //1
    //    [Display(Name = "افزایش حساب")]
    //    IncreaseCredit
    //}
    public enum BillType
    {
        //0
        [Display(Name = "درگاه پرداخت")]
        PaymentGateway,

        //1
        [Display(Name = "اعتبار حساب")]
        Credit
    }
}
