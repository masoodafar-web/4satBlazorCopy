using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models
{
    public class Point: BaseEntity
    {
       
        public string UserId { get; set; }
        //----------------------------------------------------------------------------------------------------
        public long Count { get; set; }
        //----------------------------------------------------------------------------------------------------
        //ForeignKey
        [JsonIgnore]
        [Display(Name = "نوع امتیاز")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int PointTypeId { get; set; }

        [ForeignKey("PointTypeId")]
        public virtual PointType PointType { get; set; }
        //----------------------------------------------------------------------------------------------------

    }
}