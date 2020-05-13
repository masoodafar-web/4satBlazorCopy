namespace newFace.Shared.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   

    public class Shareholder : BaseEntity
    {   
        //-------------------------------------------------
        [Display(Name = "کاربر")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser Users { get; set; }
        //-------------------------------------------------
        [Display(Name = "درصد سهام")]
        public int Percent { get; set; }
        //-------------------------------------------------
        [Display(Name = "محصول")]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

    }
}
