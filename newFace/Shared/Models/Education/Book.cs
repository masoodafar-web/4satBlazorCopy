namespace newFace.Shared.Models
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Book: BaseEntity
    {
    

        [Display(Name = "نویسنده")]
        public int? AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual Producter Author { get; set; }
        //----------------------------------------------------------------//

        [Display(Name = "مترجم")]
        public int? TranslatorId { get; set; }

        [ForeignKey("TranslatorId")]
        public virtual Producter Translators { get; set; }
        //----------------------------------------------------------------//

        [Display(Name = "ناشر")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public int PublisherId { get; set; }

        [ForeignKey("PublisherId")]
        public virtual Producter Publishers { get; set; }
        //----------------------------------------------------------------//

        [Display(Name = "گوینده")]
        public int? SpeakerId { get; set; }

        [ForeignKey("SpeakerId")]
        public virtual Producter Speakers { get; set; }
        //----------------------------------------------------------------//

        [Display(Name = "نام محصول")]

        public int ProductId { get; set; }
        //----------------------------------------------------------------//

        [Display(Name = "تعداد صفحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فقط عدد وارد کنید")]
        public int PageCount { get; set; }
        //----------------------------------------------------------------//

        [Display(Name = "بارکد")]

        [StringLength(50)]
        public string Barcode { get; set; }
        //----------------------------------------------------------------//

        [Display(Name = "فایل صوتی")]

        //[StringLength(50)]
        public string FileAudio { get; set; }
        //----------------------------------------------------------------//

        [Display(Name = "فایل کتاب")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        //[StringLength(50)]
        public string FileText { get; set; }
        //----------------------------------------------------------------//

        [Display(Name = "حجم فایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فقط عدد وارد کنید")]
        public double Size { get; set; }
        //----------------------------------------------------------------//

        [Display(Name = "نمونه متن کتاب")]
        [DataType(DataType.MultilineText)]
        //[StringLength(200)]
        public string Partofbook { get; set; }


        //----------------------------------------------------------------//

        [JsonIgnore]
        [ForeignKey("ProductId")]
        public virtual Product Products { get; set; }
    }
}
