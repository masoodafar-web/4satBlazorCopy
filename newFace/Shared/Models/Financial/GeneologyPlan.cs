using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace newFace.Shared.Models.Financial
{


    public class GeneologyPlan : BaseEntity
    {
        [Display(Name = "عنوان")]
        public string Subject { get; set; }
        //----------------------------------------------------------//

        [Display(Name = "نوع ژنولوژی")]
        public int GeneologyTypeId { get; set; }
        [ForeignKey("GeneologyTypeId")]
        public GeneologyType GeneologyType { get; set; }
        //----------------------------------------------------------//
        [Display(Name = "درصد تاثیر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Percent { get; set; }
        //----------------------------------------------------------//

        [Display(Name = "پلن یونی")]
        public int? PlanUniId { get; set; }
        [ForeignKey("PlanUniId")]
        public PlanUni PlanUni { get; set; }

        //----------------------------------------------------------//

        public List<PlanDelta> PlanDelta { get; set; }


        //----------------------------------------------------------//

        [Display(Name = "پلن فرار پله")]
        public int? PlanBreakAWayId { get; set; }
        [ForeignKey("PlanBreakAWayId")]
        public PlanBreakAWay PlanBreakAWay { get; set; }

        //----------------------------------------------------------//

        [Display(Name = "پلن تعادلی")]
        public int? PlanBinaryId { get; set; }
        [ForeignKey("PlanBinaryId")]
        public PlanBinary PlanBinary { get; set; }

        //-----------------------------------------------------------//

        [Display(Name = "پلن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public PlanEnum Plan { get; set; }
    }


    public enum PlanEnum
    {
        //0
        [Display(Name = "یونی")]
        Uni,

        //1
        [Display(Name = "دلتا")]
        Delta,

        //2
        [Display(Name = "فرار پله")]
        BreakAWay,

        //3
        [Display(Name = "تعادلی")]
        Binary
    }

}
