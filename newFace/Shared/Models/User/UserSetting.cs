using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.User
{
    public class UserSetting: BaseEntity
    {
        [Display(Name = "کاربر")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        //------------------------نمایش اعلان ها----------------------------
        [Display(Name = "نمایش اعلان مشاوره")]
        public bool AdviceNotification { get; set; }


        //---------------------آموزش های سایت------------------------------
        [Display(Name = "نمایش آموزش استفاده از صفحه اول سایت")]
        public bool IntroFirstPage { get; set; }
        [Display(Name = "نمایش آموزش استفاده از مشاوره")]
        public bool IntroAdvice { get; set; }
        [Display(Name = "نمایش آموزش استفاده از پروفایل")]
        public bool IntroProfile { get; set; }
        [Display(Name = "نمایش آموزش استفاده از اهداف")]
        public bool IntroGoals { get; set; }
        [Display(Name = "نمایش آموزش استفاده از فروشگاه")]
        public bool IntroShop { get; set; }

        //--------------------------------------------------------------
    }
}