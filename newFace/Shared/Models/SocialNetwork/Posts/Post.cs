namespace newFace.Shared.Models
{
    using newFace.Shared.Models.Education;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Post")]
    public class Post: BaseEntity
    {
  
        [Display(Name ="عنوان")]
        public string Title { get; set; }

        [Display(Name ="تصویر")]
        [DataType(DataType.Upload)]
        public string Img { get; set; }

        public string ImgThumbnail { get; set; }


        [Display(Name ="ویدئو")]
        [DataType(DataType.Upload)]
        public string Video { get; set; }

        public string VideoThumbnail { get; set; }


        [Display(Name = "فایل")]
        [DataType(DataType.Upload)]
        public string File { get; set; }


        [Display(Name = "فایل متنی")]
        [DataType(DataType.Upload)]
        public string DocumentFile { get; set; }


        [Display(Name = "توضیحات")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string Desc { get; set; }


        [Display(Name ="دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Display(Name ="سطح")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int LevelId { get; set; }
        ////[ScriptIgnore]
        //[JsonIgnore]

        [ForeignKey("LevelId")]
        public Level Levels { get; set; }


        [Display(Name ="کاربر انتشار دهنده")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser Users { get; set; }


        [Display(Name ="لایک")]
        public int Like { get; set; }

        [Display(Name = "امتیاز")]
        public int Rate { get; set; }


        [Display(Name ="دیسلایک")]
        public int DisLike { get; set; }

        [Display(Name = "بازدید")]
        public int Seen { get; set; }

        [Display(Name ="تاریخ ایجاد")]
        [DataType(DataType.DateTime)]
        public DateTime CDate { get; set; }

        
        [Display(Name ="تاریخ ویرایش")]
        [DataType(DataType.DateTime)]
        public DateTime? MDate { get; set; }


        [Display(Name ="نوع پست")]
        public PostType Type { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نوع تبلیغات")]
        public AdsType AdsType { get; set; }



        [Display(Name ="آیا حذف شده؟")]
        public bool IsDeleted { get; set; }

        public int CommentCount { get; set; }

        //اعتبار کاربر برای همین پست
        [Display(Name = "اعتبار کاربر")]
        public double UserCredit { get; set; }

        public List<Comment> Comment { get; set; }

        public List<PostChangeRequest> PostChangeRequests { get; set; }

        [JsonIgnore]
        public List<Favorite> Favorites { get; set; }
        public List<Like> Likes { get; set; }

        public bool IsFavorite { get; set; }

    }
    public enum PostType
    {
        //0
        [Display(Name ="پست شخصی")]
        Post,

        //1
        [Display(Name = "شبکه دانش")]
        Knowledg,

        //2
        [Display(Name = "تبلیغات")]
        Ads
    }

    public enum AdsType
    {
        //0
        [Display(Name = "مشاغل فردی")]
        Person,

        //1
        [Display(Name = "مشاغل شرکتی")]
        Company,

        //2
        [Display(Name = "پیشنهادهای همکاری")]
        Assist,

        //3
        [Display(Name = "خرید و فروش")]
        SellBuy,

        //4
        [Display(Name = "رزومه شخصی")]
        ProfileResume
    }
}
