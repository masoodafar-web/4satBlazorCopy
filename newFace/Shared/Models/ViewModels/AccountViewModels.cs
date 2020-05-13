using newFace.Shared.Models.Resource;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using newFace.Shared.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace newFace.Shared.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [EmailAddress(ErrorMessage = "ایمیل را با فرمت exam@exam.exam وارد کنید.")]
        [Display(Name = "ایمیل")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Email { get; set; }
        [Display(Name = "تلفن همراه")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(0, float.MaxValue, ErrorMessage = "لطفا شماره معتبر وارد کنید")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فقط عدد وارد کنید")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "لطفا شماره موبایل را صحیح وارد کنید")]
        [StringLength(maximumLength: 11, MinimumLength = 11, ErrorMessage = "شماره تلفن باید 11 رقم باشد")]
        [Remote("IsDuplicatePhoneNumber", "Account", ErrorMessage = "شماره موبایل وارد شده تکراری میباشد")]
        public string PhoneNumber { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "کد ")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "مرا به یاد داشته باشد این مرورگر؟")]
        public bool RememberBrowser { get; set; }
        [Display(Name = "مرا به خاطر بسپار؟")]
        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Display(Name = "نام کاربری")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        [StringLength(100, ErrorMessage = " {0}باید بیشتر از {2} کاراکتر باشد", MinimumLength = 6)]

        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار؟")]
        public bool RememberMe { get; set; }

        public bool IsStandalon { get; set; }
    }

    public class RegisterViewModel
    {
        [Display(Name = "نام کامل")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FullName { get; set; }
        [Display(Name = "کد ملی")]
        [DisplayFormat(NullDisplayText = "....")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فقط عدد وارد کنید")]
        [StringLength(maximumLength: 10, MinimumLength = 10, ErrorMessage = "کد ملی باید 10 رقم باشد")]
        public string NationalCode { get; set; }

        [EmailAddress(ErrorMessage = "ایمیل را با فرمت exam@exam.exam وارد کنید.")]
        [Display(Name = "ایمیل")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Email { get; set; }
        [Display(Name = "تلفن همراه")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(0, float.MaxValue, ErrorMessage = "لطفا شماره معتبر وارد کنید")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فقط عدد وارد کنید")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "لطفا شماره موبایل را صحیح وارد کنید")]
        [StringLength(maximumLength: 11, MinimumLength = 11, ErrorMessage = "شماره تلفن باید 11 رقم باشد")]
        //[Remote("IsDuplicatePhoneNumber", "Account", ErrorMessage = "شماره موبایل وارد شده تکراری میباشد")]

        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, ErrorMessage = " {0}باید بیشتر از {2} کاراکتر باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Display(Name = "تایید کلمه عبور")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "کلمه عبور با تایید کلمه عبور تطابق ندارد.")]
        public string ConfirmPassword { get; set; }
    }

    public class ProfileEditViewModel
    {
        public string UserId { get; set; }
        public string SecurityStamp { get; set; }
        [Display(Name = "نام کامل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FullName { get; set; }
        [EmailAddress(ErrorMessage = "ایمیل را با فرمت exam@exam.exam وارد کنید.")]
        [Display(Name = "ایمیل")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Email { get; set; }
        [Display(Name = "تلفن همراه")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //[Range(0, float.MaxValue, ErrorMessage = "لطفا شماره معتبر وارد کنید")]
        [StringLength(maximumLength: 11, MinimumLength = 11, ErrorMessage = "شماره تلفن باید 11 رقم باشد")]
        [Remote("IsDuplicatePhoneNumber", "Account", ErrorMessage = "شماره موبایل وارد شده تکراری میباشد")]

        public string PhoneNumber { get; set; }

        [Display(Name = "تلفن ثابت")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Display(Name = "نام مستعار")]
        public string NickName { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }
        [Display(Name = "کد ملی")]
        [DisplayFormat(NullDisplayText = "....")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا فقط عدد وارد کنید")]
        [StringLength(maximumLength: 10, MinimumLength = 10, ErrorMessage = "کد ملی باید 10 رقم باشد")]
        public string NationalCode { get; set; }
        [Display(Name = " شهر")]
        public int? CityId { get; set; }
        public virtual City City { get; set; }

        [Display(Name = "استان")]
        public int? ProvinceId { get; set; }

        [Display(Name = "کشور")]
        public int? CountryId { get; set; }

        [Display(Name = "نام کشور")]
        public string CountryName { get; set; }
        [Display(Name = "نام استان")]
        public string ProvinceName { get; set; }
        [Display(Name = "نام شهر")]
        public string CityName { get; set; }
        //------------------------------------------------------------------------------------------------------------------
        [Display(Name = "جنسیت")]
        public Enums.Gender? GenderId { get; set; }

        [Display(Name = "جنسیت")]
        [DisplayFormat(NullDisplayText = "ثبت نشده است")]
        public string GenderName { get; set; }

        //-------------------------------------------------------------------------------------------------------------------
        [Display(Name = "وضعیت خدمت")]

        public Enums.SolderStatus? SolderStatusId { get; set; }

        [Display(Name = "وضعیت خدمت")]
        [DisplayFormat(NullDisplayText = "ثبت نشده است")]
        public string SolderStatusName { get; set; }

        //-------------------------------------------------------------------------------------------------------------------
        [Display(Name = "وضعیت شغلی")]

        public Enums.JobStatus? JobStatusId { get; set; }

        [Display(Name = "وضعیت شغلی")]
        [DisplayFormat(NullDisplayText = "ثبت نشده است")]
        public string JobStatusName { get; set; }

        //-------------------------------------------------------------------------------------------------------------------

        [Display(Name = "وضعیت تاهل")]
        public Enums.MaritalStatuse? MaritalStatusId { get; set; }

        [Display(Name = "وضعیت تاهل")]
        [DisplayFormat(NullDisplayText = "ثبت نشده است")]
        public string MaritalStatusName { get; set; }

        //---------------------------------------------------------------
        [Display(Name = "عکس پروفایل")]
        [DataType(DataType.Upload)]
        public string Img { get; set; }
        //---------------------------------------------------------------
        [Display(Name = "آدرس")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        //---------------------------------------------------------------
        [Display(Name = "تاریخ تولد")]
        public string BirthDate { get; set; }
        //---------------------------------------------------------------
        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "رمز عبور و تکرار آن تطابق ندارد")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "درباره من")]
        public string AboutMe { get; set; }

        [Display(Name = "امتیاز")]
        public double Point { get; set; }

        [Display(Name = "اعتبار")]
        public double Credit { get; set; }

        [Display(Name = "وضعیت سلامت")]
        public Enums.HealthStatus? HealthStatusId { get; set; }

        [Display(Name = "وضعیت سلامت")]
        [DisplayFormat(NullDisplayText = "ثبت نشده است")]
        public string HealthStatusName { get; set; }
        public int FavoriteCount { get; set; }

        public bool IsFavorite { get; set; }
    }

    public class UserInfoViewModel:Result
    {
        public SocialNetworkViewModel SocialNetworkViewModel { get; set; }
        public ProfileEditViewModel ProfileEditViewModel { get; set; }
        public List<SkillViewModel> UserSkills { get; set; }
        public int UserFavedCount { get; set; }
    }
    public class SocialNetworkViewModel
    {

        [Key]
        public string UserId { get; set; }
        public string SecurityStamp { get; set; }

        [Display(Name = "واتساپ")]

        public string WhatsApp { get; set; }
        [Display(Name = "گوگل پلاس")]

        public string GooglePlus { get; set; }
        [Display(Name = "وبسایت")]

        public string WebSite { get; set; }
        [Display(Name = "گیت هاب")]

        public string GitHub { get; set; }
        [Display(Name = "لینکدین")]

        public string LinkedIn { get; set; }
        [Display(Name = "اینستاگرام")]

        public string Instageram { get; set; }
        [Display(Name = "تلگرام")]

        public string Telegram { get; set; }
        [Display(Name = "تویتر")]

        public string Twitter { get; set; }

    }
    public class ResetPasswordViewModel
    {
        [Display(Name = "تلفن همراه")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(0, float.MaxValue, ErrorMessage = "لطفا شماره معتبر وارد کنید")]
        [StringLength(maximumLength: 11, MinimumLength = 11, ErrorMessage = "شماره تلفن باید 11 رقم باشد")]

        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, ErrorMessage = " {0}باید بیشتر از {2} کاراکتر باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Display(Name = "تایید کلمه عبور")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "کلمه عبور با تایید کلمه عبور تطابق ندارد.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Display(Name = "تلفن همراه")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(0, float.MaxValue, ErrorMessage = "لطفا شماره معتبر وارد کنید")]
        [StringLength(maximumLength: 11, MinimumLength = 11, ErrorMessage = "شماره تلفن باید 11 رقم باشد")]

        public string phoneNumber { get; set; }
    }
}
