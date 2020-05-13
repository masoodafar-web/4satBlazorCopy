using newFace.Shared.Models.Resource;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace newFace.Shared.Models.Financial
{


    public class UserGeneology : BaseEntity
    {
        [Display(Name = "نام کاربر")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser Users { get; set; }
        //----------------------------------------------------------//
        [Display(Name = "نام کاربر پدر")]
        public string ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual ApplicationUser Parents { get; set; }
        //----------------------------------------------------------//
        [Display(Name = "نوع ژنولوژی")]
        public int GeneologyTypeId { get; set; }
        [ForeignKey("GeneologyTypeId")]
        public GeneologyType Geneologytype { get; set; }
        //----------------------------------------------------------//
        [Display(Name = "ظرفیت")]
        public int Capacity { get; set; } = 0;
        //----------------------------------------------------------//
        [Display(Name = " شهر")]
        public int? CityId { get; set; }
        [JsonIgnore]
        [ForeignKey("CityId")]
        public City City { get; set; }
        //----------------------------------------------------------//
        [Display(Name = "امتیاز")]
        public double Point { get; set; }
        //----------------------------------------------------------//
        [Display(Name = "پیش فرض")]
        public bool IsDefault { get; set; }
        //----------------------------------------------------------//
        /// <summary>
        /// این مورد را برای این اضافه کردیم که کاربرانی که اموزشگر یا 
        /// مدیر هستند را از کاربر عادی تشخیص دهیم تا آنها 
        /// را به عنوان مدیر در مشاوره مالی پیشنهاد ندهیم
        /// دلیل وجود کاربران معمولی در این جنولوژی محاسبه پورسانت آنها است
        /// @Aspkar
        /// </summary>
        [Display(Name = "در جنولوژی است")]
        public bool IsInGeneology { get; set; }
    }


}
