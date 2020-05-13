using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.ViewModels
{
    public class SkillViewModel
    {

        public int Id { get; set; }

        //------------------------------------------------------------
        [Display(Name = "درصد")]
        [Range(minimum: 0, maximum: 100, ErrorMessage = "درصد باید بین 0 تا 100 باشد")]
        public float Percent { get; set; }
        //------------------------------------------------------------
        public bool IsUpdate { get; set; }
        //------------------------------------------------------------

        public bool Lvl1 { get; set; }
        //------------------------------------------------------------

        public bool Lvl2 { get; set; }
        //------------------------------------------------------------

        public bool Lvl3 { get; set; }
        //------------------------------------------------------------

        public bool Lvl4 { get; set; }
        //------------------------------------------------------------

        [Display(Name = "نوع مهارت")]
        public SkillType SkillType { get; set; }
        //------------------------------------------------------------

        [Display(Name = "دسته بندی")]
        [Range(1, Int32.MaxValue, ErrorMessage = "لطفا مهارت خود را انتخاب کنید")]
        public int CategoryId { get; set; }

        [Display(Name = "دسته بندی")]
        public string CategoryName { get; set; }

        //------------------------------------------------------------
        [Display(Name = "کاربر")]
        public string UserId { get; set; }

        //------------------------------------------------------------
        [Display(Name = "امتیاز")]
        public long Credit { get; set; }
        //------------------------------------------------------------
        public Category Category { get; set; }
        public int LevelId { get; set; }
        [Display(Name = "سطح شما")]

        public string LevelName { get; set; }
        public Level Level { get; set; }
        public bool IsPassRatingExam { get; set; }

    }
}