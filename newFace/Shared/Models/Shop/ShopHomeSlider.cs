using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using static newFace.Shared.Models.Resource.Enums;

namespace newFace.Shared.Models.Education
{
    public class ShopHomeSlider : BaseEntity
    {

        public ShopHomeSlider()
        {
            CDate = DateTime.Now;
        }

        [Display(Name = "نام تصویر برای سئو")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Name { get; set; }
        //--------------------------------------------
        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [DataType(DataType.Upload)]
        public string Img { get; set; }

        [JsonIgnore]
        [Display(Name = "(KB) حجم تصویر ")]
        public long? ImgSize { get; set; }
        //--------------------------------------------
        [JsonIgnore]
        [Display(Name = "ترتیب نمایش")]
        public int? Order { get; set; }

        [Display(Name = "نوع محصول مرتبط با تصویر")]
        public ProductType producterType { get; set; }

        //-----------------Create---------------------
        [JsonIgnore]
        public DateTime CDate { get; set; }

        [JsonIgnore]
        public string CUserId { get; set; }

        //------------------Edit----------------------
        [JsonIgnore]
        public DateTime? EDate { get; set; }

        [JsonIgnore]
        public string EUserId { get; set; }
        //--------------------------------------------

        //public ShopHomeSliderEnum ShopHomeSliderType { get; set; }

    }

    //public enum ShopHomeSliderEnum
    //{
    //    Book = 0 ,
    //    Course = 1,
    //    Exam = 2
    //}
}