namespace newFace.Shared.Models
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class PostChangeRequest: BaseEntity
    {

        public string UserId { get; set; }

        //public virtual ApplicationUser Users { get; set; }

        public bool IsExist { get; set; }

        //----------------------------------------------------------

        //ForeignKey
        //[JsonIgnore]
        //[Display(Name = "سطح")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? LevelId { get; set; }

        [ForeignKey("LevelId")]
        public virtual Level Level { get; set; }

        //----------------------------------------------------------

        //ForeignKey
        //[JsonIgnore]
        //[Display(Name = "پست")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int PostId { get; set; }

        [JsonIgnore]
        [ForeignKey("PostId")]
        public Post Post { get; set; }

        //----------------------------------------------------------

        //ForeignKey
        //[JsonIgnore]
        //[Display(Name = "دسته")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? CategoryId { get; set; }

        
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        //----------------------------------------------------------


    }
}
