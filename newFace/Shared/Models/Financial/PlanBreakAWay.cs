using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.Financial
{
    public class PlanBreakAWay : BaseEntity
    {

        //-----------------------------------------------
        [Display(Name = "تعداد شرط")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int NumberOfConditions { get; set; }
        //-----------------------------------------------


        public List<GeneologyPlan> GeneologyPlan { get; set; }
        public List<PlanBreakAWayLevel> PlanBreakAWayLevels { get; set; }

    }
}