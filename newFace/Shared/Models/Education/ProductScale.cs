namespace newFace.Shared.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProductScale")]
    public partial class ProductScale: BaseEntity
    {

        [JsonIgnore]
        [Display(Name = "اولویت")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [RegularExpression("^[0-9]*(?:\\.[0-9]*)?$", ErrorMessage = "لطفا فقط عدد وارد کنید")]
        public float Priority { get; set; }


        [Display(Name = " دسته بندی")]
        //[Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public int? CatId { get; set; }
        [ForeignKey("CatId")]
        public virtual Category Category { get; set; }

        [JsonIgnore]
        [Display(Name = "سطح")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public int LevelId { get; set; }

        [JsonIgnore]
        [ForeignKey("LevelId")]
        public virtual Level Levels { get; set; }

        [JsonIgnore]
        public int ProductId { get; set; }

        [Display(Name = "اعتبار")]
        public int Credit { get; set; }

        [JsonIgnore]
        [Display(Name = "امتیاز")]
        public int Point { get; set; }

        [JsonIgnore]
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
