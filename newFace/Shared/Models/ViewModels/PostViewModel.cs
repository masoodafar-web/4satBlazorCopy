using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using newFace.Shared.Models.Resource;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace newFace.Shared.Models.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }

        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "تصویر")]
        public string Img { get; set; }

        public string ImgThumbnail { get; set; }


        [Display(Name = "ویدئو")]
        public string Video { get; set; }

        public string VideoThumbnail { get; set; }


        [Display(Name = "فایل")]
        public string File { get; set; }


        [Display(Name = "فایل متنی")]
        public string DocumentFile { get; set; }


        [Display(Name = "توضیحات")]
        public string Desc { get; set; }




        [Display(Name = "دسته بندی")]
        public string CategoryName { get; set; }


        [Display(Name = "دسته بندی")]
        public int? CategoryId { get; set; }


        [Display(Name = "سطح")]

        public string LevelName { get; set; }

        [Display(Name = "سطح")]

        public int? LevelId { get; set; }

        [Display(Name = "کاربر انتشار دهنده")]
        public string UserFullName { get; set; }

        [Display(Name = "کاربر انتشار دهنده")]
        public string UserId { get; set; }

        [Display(Name = "کاربر انتشار دهنده")]
        public string LoginUserId { get; set; }

        [Display(Name = "کاربر انتشار دهنده")]
        public int LoginUserGenId { get; set; }

        [Display(Name = "کاربر انتشار دهنده")]
        public string UserImg { get; set; }

        [Display(Name = "کاربر انتشار دهنده")]
        public string UserUserName { get; set; }

        [Display(Name = "کاربر انتشار دهنده")]
        public string UserNickName { get; set; }


        [Display(Name = "کاربر انتشار دهنده")]
        public double UserCredit { get; set; }


        [Display(Name = "لایک")]
        public int Like { get; set; }

        [Display(Name = "امتیاز")]
        public int Rate { get; set; }


        [Display(Name = "دیسلایک")]
        public int DisLike { get; set; }

        [Display(Name = "بازدید")]
        public int Seen { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public DateTime CDate { get; set; }


        [Display(Name = "تاریخ ویرایش")]
        public DateTime? MDate { get; set; }


        [Display(Name = "نوع پست")]
        public PostType Type { get; set; }


        [Display(Name = "نوع تبلیغات")]
        //public string AdsType { get; set; }
        public AdsType AdsType { get; set; }


        [Display(Name = "نوع تبلیغات")]
        public int? AdsTypeId { get; set; }

        [Display(Name = "آیا حذف شده؟")]
        public bool IsDeleted { get; set; }

        public int CommentCount { get; set; }

        public PostChangeRequestForPost PostChangeRequest { get; set; }

        public bool IsFavorite { get; set; }

        public bool IsLike { get; set; }

        public bool IsDisLike { get; set; }
        public Category Category { get; set; }
        public ApplicationUser Users { get; set; }
        public Level Levels { get; set; }
        public List<PostChangeRequest> PostChangeRequests { get; set; }

    }
    public class PostChangeRequestForPost
    {

        public string UserId { get; set; }

        public bool IsExist { get; set; }

        public int? LevelId { get; set; }
        public int PostId { get; set; }
        public PostType PostType { get; set; }
        public string LevelName { get; set; }
        public SelectList LevelIdList { get; set; }
        public SelectList CategoryIdList { get; set; }

        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }

    }
    public class LikeResultViewModel : Result
    {
        public int Like { get; set; }
        public int DisLike { get; set; }
        public bool IsLike { get; set; } = false;
        public bool IsDisLike { get; set; } = false;
        public int Rate { get; set; }

    }

}