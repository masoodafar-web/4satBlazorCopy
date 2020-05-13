namespace newFace.Shared.Models
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UsersEpubBookInfo : BaseEntity
    {

        [Display(Name = "محصول")]

        public int ProductId { get; set; }

        [JsonIgnore]
        [ForeignKey("ProductId")]
        public virtual Product Products { get; set; }

        //-----------------------------------------------------------------------//
        [Display(Name = "کاربر")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        //-----------------------------------------------------------------------//

        [Display(Name = "شماره صفحه")]
        public int PageNumber { get; set; }

        //-----------------------------------------------------------------------//
        [Display(Name = "بخش")]
        public string HrefBook { get; set; }

        //-----------------------------------------------------------------------//
        [Display(Name = "کد هایلایت")]
        public string CfiRange { get; set; }
        //-----------------------------------------------------------------------//
        [Display(Name = "رنگ هایلایت")]
        public string HighlightColor { get; set; }
        //-----------------------------------------------------------------------//

        [Display(Name = "تاریخ")]
        public DateTime Date { get; set; }
        //-----------------------------------------------------------------------//

        [Display(Name = "تاریخ")]
        public string CDate { get; set; }
        //-----------------------------------------------------------------------//
        [Display(Name = "متن")]
        public string HighlightText { get; set; }
        //-----------------------------------------------------------------------//
        [Display(Name = "یادداشت")]
        public string Note { get; set; }
    }
}
