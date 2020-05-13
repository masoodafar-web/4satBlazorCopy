using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace newFace.Shared.Models.ViewModels
{
    public class UserBasicInfoViewModel
    {
        //public UserBasicInfoViewModel()
        //{
        //    Items = new List<UserBasicInfoViewModel>();
        //}

        public int Id { get; set; }

        public string UserId { get; set; }
        //----------------------------------------------------
        [Display(Name = "نام و نام خانوادگی")]
        public string FullName { get; set; }
        //----------------------------------------------------
        [Display(Name = "نام مستعار")]
        public string NickName { get; set; }
        //----------------------------------------------------
        [Display(Name = "امتیاز")]     
        public string Desc { get; set; }
        //----------------------------------------------------
        [Display(Name = "تصویر پروفایل")]
        public string Img { get; set; }
        //----------------------------------------------------
        public double Credit { get; set; }
        //---------------------------------------------------- شبکه اجتماعی ----------------------------------------------

        public string AboutMe { get; set; }


        //------------------------------------------------------------------------------------------------------------------

        //public List<UserBasicInfoViewModel> Items { get; set; }
    }
}