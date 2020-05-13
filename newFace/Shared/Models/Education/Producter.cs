using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using newFace.Shared.Models.Education;

namespace newFace.Shared.Models
{
    using newFace.Shared.Models.ViewModels;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    public class Producter: BaseEntity
    {
      
        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string FullName { get; set; }
        //-----------------------------------------------------------
        [Display(Name = "تصویر")]
        public string Img { get; set; }
        //-----------------------------------------------------------
        [DataType(DataType.MultilineText)]
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        //-----------------------------------------------------------
  
        [JsonIgnore]
        [InverseProperty("Author")]
        public virtual ICollection<Book> AuthorBooks { get; set; }
        //-----------------------------------------------------------
        [JsonIgnore]
        [InverseProperty("Translators")]
        public virtual ICollection<Book> TranslatorBooks { get; set; }
        //-----------------------------------------------------------
        [JsonIgnore]
        [InverseProperty("Publishers")]
        public virtual ICollection<Book> PublisherBooks { get; set; }
        //-----------------------------------------------------------
        [JsonIgnore]
        [InverseProperty("Speakers")]
        public virtual ICollection<Book> SpeakerBooks { get; set; }
        //-----------------------------------------------------------
        [JsonIgnore]
        public virtual ICollection<Exam> Exams { get; set; }
        //-----------------------------------------------------------
        [JsonIgnore]
        public virtual ICollection<Course> Courses { get; set; }
        //-----------------------------------------------------------
        [JsonIgnore]
        public virtual ICollection<ProducterType> ProducterTypes { get; set; }


    }
}
