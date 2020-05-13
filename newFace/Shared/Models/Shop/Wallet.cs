using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using newFace.Shared.Models.Resource;

namespace newFace.Shared.Models.Shop
{
    public class Wallet: BaseEntity
    {

        public Wallet()
        {
            CDate = DateTime.Now;
        }

        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string UserId { get; set; }
        //--------------------------------------------
        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را مشخص کنید")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:#,##0 تومان}")]
        [Range(1, long.MaxValue, ErrorMessage = "برای {0} از اعداد مناسب استفاده کنید")]
        public double Amount { get; set; }

        //-----------------Create---------------------
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public DateTime CDate { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string CUserId { get; set; }

        //------------------Edit----------------------
        public DateTime? EDate { get; set; }

        public string EUserId { get; set; }
        //--------------------------------------------

        public string RefId { get; set; }

        public Enums.Statue Statue { get; set; }

        public TransactionTypeEnum TransactionType { get; set; }

        public int BillId { get; set; }
    }

    public enum TransactionTypeEnum
    {
        [Display(Name = "کاهش")]
        Decrease = 0,
        [Display(Name = "افزایش")]
        Increase = 1
    }
}