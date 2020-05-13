using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace newFace.Shared.Models.Resource
{
    public class Resources: BaseEntity
    {

        [Display(Name = "عنوان")]
        public string Name { get; set; }
        //----------------------------------------------------------------------------------------------------
        [Display(Name = "نوع")]
        public ResourcesTypeEnum ResourcesType { get; set; }
        //----------------------------------------------------------------------------------------------------
        [JsonIgnore]
        [InverseProperty("Language")]
        public virtual List<Product> Language { get; set; }
        [JsonIgnore]
        [InverseProperty("University")]
        public virtual List<EducationalRecord> University { get; set; }
        [JsonIgnore]
        [InverseProperty("JobPosition")]
        public virtual List<JobResume> JobPosition { get; set; }
        [JsonIgnore]
        [InverseProperty("Company")]
        public virtual List<JobResume> Company { get; set; }
        //----------------------------------------------------------------------------------------------------

        public enum ResourcesTypeEnum
        {
            //0
            [Display(Name = "جنسیت")]
            Gender,
            //1
            [Display(Name = "وضعیت شغلی")]
            JobStatus,
            //2
            [Display(Name = "سطح")]
            Level,
            //3
            [Display(Name = "وضعیت تاهل")]
            MaritalStatuse,
            //4
            [Display(Name = "نوع امتیاز")]
            PointType,
            //5
            [Display(Name = "وضعیت خدمت")]
            SolderStatus,
            //6
            [Display(Name = "زبان")]
            Language,
            //8
            [Display(Name = "سوابق تحصیلی")]
            EducationalRecord = 8,
            //9
            [Display(Name = "دانشگاه")]
            University = 9,
            //10
            [Display(Name = "سمت")]
            JobPosition = 10,
            //11
            [Display(Name = "شرکت")]
            Company = 11,
            //[Display(Name = "وضعیت سلامت")]
            //HealthStatus = 12
        }

    }
}