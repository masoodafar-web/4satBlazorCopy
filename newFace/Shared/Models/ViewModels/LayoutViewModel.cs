using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.ViewModels
{
    public class LayoutViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserNikName { get; set; }
        public string UserPoint { get; set; }
        public string UserCreditWallet { get; set; }
        public string UserCredit { get; set; }
        public int CartCount { get; set; }
        public int unSeenCount { get; set; }
        public int BillCount { get; set; }
        public int ProductsCount { get; set; }
        public int ShareholderListCount { get; set; }
        public int CountFaved { get; set; }
        public int MySentGifts { get; set; }
        public int MyRecievedGifts { get; set; }
        public string FullName { get; set; }
        public string UserImg { get; set; }
        public string UserInfoComplatePecent { get; set; }
        public List<string> SkilList { get; set; }
        public string basicinfoCOMPLT { get; set; }
        public string SkillCOMPLT { get; set; }
        public string EducationalRecordCOMPLT { get; set; }
        public string JobResumeCOMPLT { get; set; }
        public string WorkSampleCOMPLT { get; set; }
        public string socialnetworkCOMPLT { get; set; }
        public int AdviceVisionCount { get; set; }
        public int UnSeencommentCount { get; set; }
    }
}