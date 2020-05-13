using System.ComponentModel.DataAnnotations;

namespace newFace.Shared.Models.Resource
{
    public static class Enums
    {
        public enum IsEditType
        {
            JobResume = 1,
            EducationalRecord = 2,
            WorkSample = 4,
            Skill = 5
        }
        public enum Statue
        {
            //0
            Failure = 0,
            //1
            Success = 1,
            //2
            AccessDenied = 2,
            //3
            NullList = 3,
            //4
            Pending = 4,
            //5
            Repetitive = 5,
            //6
            Null = 6,
            //7
            LockedOut = 7,
            //8
            NotlastVersion = 8,
            //9
            NotLastVersionForce = 9,
            //10
            NotFound = 10
        }

        public enum CrudStatue
        {
            //0
            Failure,
            //1
            Success,
            //2
            Repetitive,
            //3
            NotFound,
            //4
            //Pending
        }

        public enum SmsType
        {
            RegisterConfirmationCode,
            RegisterConfirmationCodeResend,
            ForgotPasswordConfirmationCode
        }

        public enum ConfirmationType
        {
            //0
            Register,
            //1
            ForgotPassword,
            //2
            ResendCode
        }

        public enum EditProfileType
        {
            //0
            Register,
            //1
            Edit
        }

        public enum PagesListType
        {
            //0
            PagesList,
            //1
            FavouritesList,
            //2
            SeenList,
            //3
            NearestList
        }

        public enum CartLvl
        {
            //0
            Cart,
            //1
            Invoice
        }

        public enum DeviceType
        {
            //0
            Site,
            //1
            Android,
            //2
            Ios
        }

        public enum FactorType
        {
            //0
            Purchase,
            //1
            Sale
        }
        public enum VisionStatus
        {
            //پیشنهاد
            Advice = 1,
            //انتخاب شده
            Selected = 2,
            //تمام شده
            Finished = 3,
        }
        public enum StatusTypeQuestion
        {
            //0
            [Display(Name = "سوال عادی")]

            Normal,
            //1
            [Display(Name = "سوال تعیین سطح")]

            Rating
        }

        public enum ReturnFrom
        {
            //0
            [Display(Name = "سایت")]
            Site,

            //1
            [Display(Name = "اپلیکیشن")]
            Application

        }
        public enum JobStatus
        {
            //0
            [Display(Name = "شاغل")]

            Employed = 0,
            //1
            [Display(Name = "بیکار")]
            Unemployed = 1
        }
        public enum SolderStatus
        {

            [Display(Name = "خدمت نرفته ام")]
            NotGone = 0,

            [Display(Name = "معافیت اعصاب و روان")]
            ExemptionRed = 1,

            [Display(Name = "معافیت پزشکی")]
            ExemptionMedical = 2,

            [Display(Name = "معافیت تحصیلی")]
            ExemptionEducational = 3,

            [Display(Name = "معافیت ایثارگری")]
            ExemptionSacrifice = 4,

            [Display(Name = "معافیت موارد خاص")]
            ExemptionSpecial = 5,

            [Display(Name = "معافیت کفالت")]
            ExemptionGuarantee = 6,

            [Display(Name = "دارای کارت پایان خدمت")]
            Ihave = 7,
        }
        public enum HealthStatus
        {

            [Display(Name = "سالم")]
            Healthy = 0,

            [Display(Name = "معلول")]
            disabled = 1,
        }
        public enum Gender
        {

            [Display(Name = "مرد")]
            Men = 0,

            [Display(Name = "زن")]
            Women = 1,

            [Display(Name = "دیگر")]
            Other = 2,
        }
        public enum EducationalStatus
        {

            [Display(Name = "سیکل")]
            Cycle = 0,

            [Display(Name = "دیپلم")]
            Diploma = 1,

            [Display(Name = "کاردانی")]
            Associate = 2,

            [Display(Name = "کارشناسی")]
            Masters = 3,

            [Display(Name = "دکتری حرفه‌ای")]
            Doctor = 4,

            [Display(Name = "دکتری تخصصی")]
            PHD = 5,

            [Display(Name = "فوق تخصص")]
            Specialty = 6,

        }

        public enum MaritalStatuse
        {

            [Display(Name = "متاهل")]
            Marital = 0,

            [Display(Name = "مجرد")]
            Single = 1,

        }
        public enum changetype
        {
            //0
            Add = 0,
            //1
            Remove = 1,
            //2
            NotFound = 2
        }
        public enum ProductSearchType
        {
            //0
            [Display(Name = "جدید")]
            NewProducts,
            //1
            [Display(Name = "پر فروش")]
            BestSellerProducts,
            //2
            [Display(Name = "پیشنهادی")]
            SuggestionProducts,
            //3
            [Display(Name = "مرتبط")]
            RelatedProducts,
            //3
            [Display(Name = "محصولات من")]
            MyProducts,
        }
        public enum NotifiType
        {
            ChatNewMessage = 1,
            Postlike = 2,
            PostComment = 3,
            UserFollow = 4
        }
        public enum ProductType
        {
            //0
            [Display(Name = "کتاب")]
            Book,
            //1
            [Display(Name = "دوره")]
            Course,
            //2
            [Display(Name = "آزمون")]
            Exam
        }
        public enum GiftType
        {
            [Display(Name = "هدیه ارسال شده")]
            GiftSend = 0,

            [Display(Name = "هدیه دریافت شده")]
            GiftRecive = 1
        }

    }
}
