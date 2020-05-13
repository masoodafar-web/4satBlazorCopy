using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.Financial
{
    public class PlanUniLevel : BaseEntity
    {
        //-----------------------------------------------
        public int PlanUniId { get; set; }

        [ForeignKey("PlanUniId")]
        public virtual PlanUni PlanUnis { get; set; }
        //------------------------------------------------

        [Display(Name = "شماره سطح")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فقط عدد وارد کنید")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public int LevelNumber { get; set; }
        //------------------------------------------------

        [Display(Name = "درصد")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فقط عدد وارد کنید")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [Range(0,100 , ErrorMessage = "{0} باید عددی بین 0 تا 100 باشد")]
        public int Percent { get; set; }

    }
}