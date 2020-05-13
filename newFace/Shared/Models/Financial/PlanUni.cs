using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.Financial
{
    public class PlanUni : BaseEntity
    {

        //-----------------------------------------------
        [Display(Name = "ماکسیمم لول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int MaxLevel { get; set; }
        //-----------------------------------------------


        public List<GeneologyPlan> GeneologyPlan { get; set; }


        //-----------------------------------------------

        //[Display(Name = "پلن جنولوژی")]
        //public int GeneologyPlanId { get; set; }

        //[ForeignKey("GeneologyPlanId")]
        //public GeneologyPlan GeneologyPlan { get; set; }

        //-----------------------------------------------

        public List<PlanUniLevel> PlanUniLevels { get; set; }
    }
}