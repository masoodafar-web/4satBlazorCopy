using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.Financial
{
    public class PlanBinary : BaseEntity
    {

        //------------------------------------------------

        [Display(Name = "تعداد مجموعه")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فقط عدد وارد کنید")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public int NumberOfSets { get; set; }

        //-------------------------------------------------
        [Display(Name = "مبلغ تعادل")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فقط عدد وارد کنید")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public long AmountBalanceBinary { get; set; }
        //------------------------------------------------

        [Display(Name = "مبلغ کارمزد")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فقط عدد وارد کنید")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public long AmountWageBinary { get; set; }
        //------------------------------------------------

        [Display(Name = "تعداد دفعات پرداخت")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فقط عدد وارد کنید")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public int FrequencyOfPayments { get; set; }

        //-------------------------------------------------
        [Display(Name = "مبلغ فلش")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فقط عدد وارد کنید")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public long FlashAmount { get; set; }
        //------------------------------------------------

        //[Display(Name = "روش محاسبه")]
        //[Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        //public CalcMethod CalcMethod { get; set; }
        //-----------------------------------------------

        public List<GeneologyPlan> GeneologyPlan { get; set; }

    }
}