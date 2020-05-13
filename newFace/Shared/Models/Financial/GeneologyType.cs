using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace newFace.Shared.Models.Financial
{


    public class GeneologyType : BaseEntity
    {

        [Display(Name = "عنوان نوع ژنولوژی")]
        public string Title { get; set; }
        //----------------------------------------------------------//
        [Display(Name = "ردیف درآمدی")]
        public RowType RowType { get; set; }
        //----------------------------------------------------------//
        [Display(Name = "نوع ژنولوژی")]
        public GeneologyTypeEnum Type { get; set; }
        //----------------------------------------------------------//
        [Display(Name = "زمان محاسبه")]
        public CalculationTime CalculationTime { get; set; }

        //----------------------------------------------------------//
        [Display(Name = "نوع سیستم")]
        public SystemType SystemType { get; set; }

        //----------------------------------------------------------//
        public List<UserGeneology> UserGeneologies { get; set; }
        public List<GeneologyPlan> GeneologyPlan { get; set; }

    }


    public enum RowType
    {
        //0
        [Display(Name = "درآمد")]
        Income,

        //1
        [Display(Name = "فروش")]
        Sell
    }

    public enum CalculationTime
    {
        //0
        [Display(Name = "لحظه فروش")]
        MomentOfSale,

        //1
        [Display(Name = "ماهانه")]
        Monthly
    }

    public enum GeneologyTypeEnum
    {
        //0
        [Display(Name = "مدیر توسعه بیمه")]
        DevelopmentManagerInsurance,

        //1
        [Display(Name = "مدیر آموزش بیمه")]
        DirectorOfEducationInsurance,

        //2
        [Display(Name = "مدیر توسعه بانک")]
        DevelopmentManagerBank,

        //3
        [Display(Name = "مدیر آموزش بانک")]
        DirectorOfEducationBank,

        //4
        [Display(Name = "مدیر توسعه بورس")]
        DevelopmentManagerExchange,

        //5
        [Display(Name = "مدیر آموزش بورس")]
        DirectorOfEducationExchange,
        
        //6
        [Display(Name = "سهامدار فرصت")]
        ForsatShareholder,

        //7
        [Display(Name = "معرف فرصت")]
        ForsatReagent,

       
    }

    public enum SystemType
    {
        //0
        [Display(Name = "بیمه")]
        Insurance,

        //1
        [Display(Name = "بانک")]
        Bank,

        //2
        [Display(Name = "بورس")]
        Exchange,

        //3
        [Display(Name = "سهامدار فرصت")]
        ForsatShareholder,

        //4
        [Display(Name = "معرف فرصت")]
        ForsatReagent,


    }
}
