using newFace.Shared.Models.Resource;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.Advice
{
    public class FinancialAdvice : BaseEntity
    {
        [Display(Name = "شروع بازه سنی")]
        public int? AgeStart { get; set; }

        [Display(Name = "پایان بازه سنی")]
        public int? AgeEnd { get; set; }

        //----------------------------------------------------
        [Display(Name = "وضعیت خدمت")]
        public Enums.SolderStatus? SolderStatusId { get; set; }
        //----------------------------------------------------
        [Display(Name = "وضعیت تحصیلی")]
        public Enums.EducationalStatus? EducationalStatusId { get; set; }
        //----------------------------------------------------
        [Display(Name = "وضعیت سلامت")]
        public Enums.HealthStatus? HealthStatusId { get; set; }
        //----------------------------------------------------
        [Display(Name = "وضعیت شغلی")]
        public Enums.JobStatus? JobStatusId { get; set; }
        //----------------------------------------------------
        [Display(Name = "شروع بازه زمان آزاد")]
        public int? FreeTimeStart { get; set; }

        [Display(Name = "پایان بازه زمان آزاد")]
        public int? FreeTimeEnd { get; set; }
        //----------------------------------------------------
        [Display(Name = "شروع بازه مبلغ پس انداز")]
        public long? AmountOfSavingsStart { get; set; }

        [Display(Name = "پایان بازه مبلغ پس انداز")]
        public long? AmountOfSavingsEnd { get; set; }
        //----------------------------------------------------
        [Display(Name = "شروع بازه درآمد ماهانه")]
        public long? IncomeStart { get; set; }

        [Display(Name = "پایان بازه درآمد ماهانه")]
        public long? IncomeEnd { get; set; }
        //----------------------------------------------------
        [Display(Name = " شروع بازه سرمایه اولیه")]
        public long? InitialInvestmentStart { get; set; }

        [Display(Name = "پایان بازه سرمایه اولیه")]
        public long? InitialInvestmentEnd { get; set; }
        //----------------------------------------------------
        [Display(Name = "شروع بازه هدف درآمدی")]
        public long? EarningsGoalStart { get; set; }

        [Display(Name = " پایان بازه هدف درآمدی")]
        public long? EarningsGoalEnd { get; set; }
        //---------------------------------------------------

        [Display(Name = "شروع بازه زمانی ماهانه")]
        public long? MonthlyIntervalStart { get; set; }

        [Display(Name = " پایان بازه زمانی ماهانه")]
        public long? MonthlyIntervalEnd { get; set; }
        //---------------------------------------------------
        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        //---------------------------------------------------
    }
}