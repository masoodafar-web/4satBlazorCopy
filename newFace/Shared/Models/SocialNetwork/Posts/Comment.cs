using newFace.Shared.Models.Resource;

namespace newFace.Shared.Models
{
    using newFace.Shared.Models.ViewModels;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Comment")]
    public class Comment : BaseEntity
    {

        [DataType(DataType.MultilineText)]
        public string Desc { get; set; }

        //-----------------------------------------------------------------------------
        [Display(Name = "امتیاز")]
        public int? Rank { get; set; }

        //-----------------------------------------------------------------------------
    
        [Display(Name = "تاریخ ایجاد")]
        [DataType(DataType.DateTime)]
        public DateTime CDate { get; set; }

        //-----------------------------------------------------------------------------

        //ForeignKey
        [Display(Name = "کامنت والد")]
        public int? ParentId { get; set; }

        ////[ScriptIgnore]
        //[JsonIgnore]
        [ForeignKey("ParentId")]
        public virtual Comment CommentParent { get; set; }
        //-----------------------------------------------------------------------------

        //ForeignKey
        [Display(Name = "کامنت والد اول")]
        public int? FirstParentId { get; set; }

        //[ScriptIgnore]
        [JsonIgnore]
        [ForeignKey("FirstParentId")]
        public Comment FirstCommentParent { get; set; }
        //-----------------------------------------------------------------------------
        //ForeignKey
        [Display(Name = "پست")]
        public int? PostId { get; set; }

        //[ScriptIgnore]
        [JsonIgnore]
        [ForeignKey("PostId")]
        public Post Post { get; set; }

        //------------------------------------------------------------------------------

        //ForeignKey
        [Display(Name = "محصول")]
        public int? ProductId { get; set; }
        //[ScriptIgnore]
        [JsonIgnore]
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        //------------------------------------------------------------------------------

        //ForeignKey
        [Display(Name = "بلاگ")]
        public int? BlogId { get; set; }
        // //[ScriptIgnore]
        //  [JsonIgnore]
        [ForeignKey("BlogId")]
        public Blog Blog { get; set; }

        //------------------------------------------------------------------------------

        //ForeignKey
        [Display(Name = "کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserId { get; set; }

        ////[ScriptIgnore]
        //[JsonIgnore]
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        //-------------------------------------------------------------------------------
        //[ScriptIgnore]
        [JsonIgnore]
        [InverseProperty("CommentParent")]
        public List<Comment> CommentsChilds { get; set; }

        ////[ScriptIgnore]
        //[JsonIgnore]
        [InverseProperty("FirstCommentParent")]
        public virtual List<Comment> FirstCommentsChilds { get; set; }
    }
}
