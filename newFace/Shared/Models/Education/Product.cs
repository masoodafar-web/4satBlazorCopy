namespace newFace.Shared.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using newFace.Shared.Models.Education;
    using Models.Resource;
    using Newtonsoft.Json;
    using static newFace.Shared.Models.Resource.Enums;
    using newFace.Shared.Models.Advice;

    public class Product : BaseEntity
    {

        [Display(Name = "موضوع")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Title { get; set; }
        //-------------------------------------------------------------//
        [Display(Name = "امتیاز")]
        public float Rate { get; set; }
        //-------------------------------------------------------------//
        [Display(Name = "حق معرف")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فقط عدد وارد کنید")]
        public long ReferralRight { get; set; }
        //-------------------------------------------------------------//
        [Display(Name = "قیمت")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فقط عدد وارد کنید")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public double Price { get; set; }
        //-------------------------------------------------------------//
        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "حداکثر کاراکتر مجاز {1} است")]
        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Description { get; set; }
        //-------------------------------------------------------------//
        [Display(Name = "تاریخ ثبت")]
        // [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public DateTime Date { get; set; }
        //-------------------------------------------------------------//
        [Display(Name = "زبان")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public int LanguageId { get; set; }

        [ForeignKey("LanguageId")]
        public virtual Resources Language { get; set; }
        //-------------------------------------------------------------//
        [Display(Name = "درصد تخفیف")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فقط عدد وارد کنید")]

        public double Discount { get; set; }

        [Display(Name = "قیمت با تخفیف")]
        public double? PriceWithDiscount { get; set; }
        //-------------------------------------------------------------//

        [Display(Name = "نوع محصول")]
        // [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public ProductType Type { get; set; }



        //-------------------------------------------------------------//
        [Display(Name = "درصد سهام قابل فروش")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فقط عدد وارد کنید")]
        public int ShareholderPercentForSell { get; set; }

        [Display(Name = "قیمت هر واحد سهام")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فقط عدد وارد کنید")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public long ShareholderUnitPrice { get; set; }

        [Display(Name = "درصد سهام خریداری شده")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فقط عدد وارد کنید")]
        public int ShareholderPercentSold { get; set; }

        //-------------------------------------------------------------//


        //[StringLength(50)]
        [Display(Name = "تصویر")]
        // [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Img { get; set; }

        //--------------------------تعداد فروش-------------------------------//

        [Display(Name = "تعداد فروش")]
        public long SoldCount { get; set; }


        //--------------------------ForeignKeys-------------------------------//
        [Display(Name = "محصول مربوط")]
        public int? ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product1 { get; set; }

        [InverseProperty("Products")]
        public virtual ICollection<Book> Books { get; set; }

        [JsonIgnore]

        public virtual ICollection<UsersEpubBookInfo> UsersEpubBookInfos { get; set; }

        [InverseProperty("Products")]
        public virtual ICollection<Course> Courses { get; set; }

        [InverseProperty("Products")]
        public virtual ICollection<Exam> Exams { get; set; }

        [JsonIgnore]
        public virtual ICollection<FactorforsaleProduct> FactorforsaleProducts { get; set; }

        [JsonIgnore]
        public virtual ICollection<Gift> Gift { get; set; }


        public virtual ICollection<ProductScale> ProductScale { get; set; }


        [JsonIgnore]
        public virtual ICollection<Shareholder> Shareholders { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProductSeenInfo> ProductSeenInfo { get; set; }

        //[JsonIgnore]
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<SuggestedProduct> SuggestedProducts { get; set; }


        [JsonIgnore]
        public List<Favorite> Favorites { get; set; }

        public bool IsFavorite { get; set; }


    }
}
