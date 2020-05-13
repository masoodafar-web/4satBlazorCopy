using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.Financial
{
    public class PlanBreakAWayLevel : BaseEntity
    {
        //-----------------------------------------------
        [Display(Name = "شناسه پلن")]

        public int PlanBreakAWayId { get; set; }
        [ForeignKey("PlanBreakAWayId")]
        public virtual PlanBreakAWay PlanBreakAWay { get; set; }
        //------------------------------------------------

        [Display(Name = "درصد")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فقط عدد وارد کنید")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [Range(0,100 , ErrorMessage = "{0} باید عددی بین 0 تا 100 باشد")]
        public int PercentBreakAWay { get; set; }
        //------------------------------------------------

        [Display(Name = "مبلغ")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فقط عدد وارد کنید")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public long AmountBreakAWay { get; set; }
        //------------------------------------------------

        [Display(Name = "روش محاسبه")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public CalcMethod CalcMethod { get; set; }

    }
    public enum CalcMethod
    {
        //0
        [Display(Name = "خود")]
        Self,

        //1
        [Display(Name = "خود و مجموعه")]
        SelfAndCollection
    }
}