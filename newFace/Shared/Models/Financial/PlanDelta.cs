using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.Financial
{
    public class PlanDelta : BaseEntity
    {
        [Display(Name = "عنوان")]
        public string Subject { get; set; }
        //------------------------------------------------

        [Display(Name = "درصد")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فقط عدد وارد کنید")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [Range(0, 100, ErrorMessage = "{0} باید عددی بین 0 تا 100 باشد")]
        public int PercentDelta { get; set; }

        //-------------------------------------------------

        [Display(Name = " لول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int LevelDelta { get; set; }
        //----------------------------------------------------------//

        [Display(Name = "الحاق به پلنها")]
        public int? GeneologyPlanId { get; set; }
        [ForeignKey("GeneologyPlanId")]
        public GeneologyPlan GeneologyPlan { get; set; }
        //----------------------------------------------------------//

        [Display(Name = "الحاق به دلتا")]
        public int? PlanDeltaId { get; set; }
        [ForeignKey("PlanDeltaId")]
        public PlanDelta PlanDeltas { get; set; }


    }
}