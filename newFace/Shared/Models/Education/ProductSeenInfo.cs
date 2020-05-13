namespace newFace.Shared.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   

    public class ProductSeenInfo: BaseEntity
    {

        public string UserId { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]

        public Product Product { get; set; }

        // public int? VideoId { get; set; }

        public double Credit { get; set; }
        public double Point { get; set; }

        public bool IsComplete { get; set; }

        public string BookLastseenPage { get; set; }

        [Display(Name = "تاریخ")]
        public DateTime? LastseenDate { get; set; }
        [Display(Name = "تاریخ")]
        public string LastseenCDate { get; set; }

    }
}
