using newFace.Shared.Models.Financial;

namespace newFace.Shared.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   

    public  class FactorforsaleProduct: BaseEntity
    {
               
      

        [Display(Name = "نام کاربر")]
        public string UserId { get; set; }

        //----------------------------------------------------------//

        [Display(Name = "محصول")]
        public int ProductId { get; set; }

        //----------------------------------------------------------//

        [Display(Name = "تعداد")]
        public int Count { get; set; }

        //----------------------------------------------------------//

        [Display(Name = "قیمت واحد")]
        public double UnitPrice { get; set; }

        //----------------------------------------------------------//

        [Display(Name = "مبلغ تخفیف")]
        public double Discount { get; set; }

        //----------------------------------------------------------//

        [Display(Name = "قیمت نهایی")]
        public double TotalPrice { get; set; }

        //----------------------------------------------------------//
        [Display(Name = "نوع خرید ")]

        public BuyType BuyType { get; set; }

        //----------------------------------------------------------//

        [Display(Name = "فاکتور")]
        public int BillId { get; set; }

        
        //---------------------ForeignKey-------------------------//

        [ForeignKey("ProductId")]
        public Product Products { get; set; }


        [ForeignKey("BillId")]
        public Bill Bill { get; set; }


        [ForeignKey("UserId")]

        public ApplicationUser Users { get; set; }

    }
    public enum BuyType
    {
        //0
        [Display(Name = "خرید عادی")]
        Normal=0,
        [Display(Name = "خرید به عنوان هدیه")]
        Gift=1,
        [Display(Name = "خرید به عنوان سهام")]
        Shareholder=2,
        [Display(Name = "نشان شده ها")]
        Favorit=3,//البته این از انواع خرید نیست ولی برای استفاده تو  سریسها گذاشتمش مسعود
    }
}
