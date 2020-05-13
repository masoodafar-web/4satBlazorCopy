using newFace.Shared.Models.Financial;

namespace newFace.Shared.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Gift")]
    public class Gift: BaseEntity
    {

        [Display(Name = "کاربر هدیه دهنده")]
        public string UserSend { get; set; }
        [ForeignKey("UserSend")]
        public ApplicationUser SendUsers { get; set; }


        [Display(Name = "کاربر هدیه گیرنده")]
        public string UserResiv { get; set; }
        [ForeignKey("UserResiv")]
        public ApplicationUser ResiveUsers { get; set; }


        [Display(Name = "هدیه")]
        public int PorductId { get; set; }
        [ForeignKey("PorductId")]
        public Product Products { get; set; }

        [Display(Name = " تاریخ ارسال هدیه")]
        public DateTime Date { get; set; }

        public bool Status { get; set; }



        //-------------------ForeignKey---------------------------//

        public int? BillId { get; set; }
        [ForeignKey("BillId")]
        public Bill Bill { get; set; }
    }
}
