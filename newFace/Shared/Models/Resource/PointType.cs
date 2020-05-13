using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models
{
    public class PointType: BaseEntity
    {

        [Display(Name = "عنوان")]
        public string Name { get; set; }
        //----------------------------------------------------------------------------------------------------
    }
}