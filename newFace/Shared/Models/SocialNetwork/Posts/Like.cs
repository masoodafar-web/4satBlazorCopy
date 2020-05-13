using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models
{
    public class Like: BaseEntity
    {
        [Display(Name = "کاربر")]

        public string UserId { get; set; }
        //----------------------------------------------------------------------------------------------------
        [Display(Name = "پست")]

        public int PostId { get; set; }
        //----------------------------------------------------------------------------------------------------
        [Display(Name = "تاریخ تغییر")]

        public DateTime Date { get; set; }
        //----------------------------------------------------------------------------------------------------
        [Display(Name = "لایک هست؟")]

        public bool IsLike { get; set; }
        //----------------------------------------------------------------------------------------------------
        [Display(Name = "تعداد (معمولا یک)")]
        public int Count { get; set; }
        //----------------------------------------------------------------------------------------------------

        [Display(Name = "امتیاز کاربر")]

        public long Point { get; set; }

        [Display(Name ="امتیاز پست")]
        public int Rate { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser Users { get; set; }

    }
}