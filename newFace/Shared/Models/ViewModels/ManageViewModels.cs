

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace newFace.Shared.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        [Remote("IsDuplicatePhoneNumber", "Account", ErrorMessage = "شماره موبایل وارد شده تکراری میباشد")]

        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, ErrorMessage = " {0}باید بیشتر از {2} کاراکتر باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور جدید")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Display(Name = "تایید کلمه عبور جدید")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "کلمه عبور با تایید کلمه عبور تطابق ندارد.")]

        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, ErrorMessage = " {0}باید بیشتر از {2} کاراکتر باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور جاری")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, ErrorMessage = " {0}باید بیشتر از {2} کاراکتر باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور جدید")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Display(Name = "تایید کلمه عبور جدید")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "کلمه عبور با تایید کلمه عبور تطابق ندارد.")]
        public string ConfirmPassword { get; set; }
    }


    public class AddPhoneNumberViewModel
    {
        [Display(Name = "تلفن همراه")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(0, float.MaxValue, ErrorMessage = "لطفا شماره معتبر وارد کنید")]
        [StringLength(maximumLength: 11, MinimumLength = 11, ErrorMessage = "شماره تلفن باید 11 رقم باشد")]
        [Remote("IsDuplicatePhoneNumber1", "Account", ErrorMessage = "شماره موبایل وارد شده تکراری میباشد")]

        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Display(Name = "تلفن همراه")]
        [DisplayFormat(NullDisplayText = "....")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(0, float.MaxValue, ErrorMessage = "لطفا شماره معتبر وارد کنید")]
        [StringLength(maximumLength: 11, MinimumLength = 11, ErrorMessage = "شماره تلفن باید 11 رقم باشد")]
        //[Remote("IsDuplicatePhoneNumber", "Account", ErrorMessage = "شماره موبایل وارد شده تکراری میباشد")]

        public string PhoneNumber { get; set; }
        public string UserId { get; set; }
        public bool? ForgotPassword { get; set; }
        public string Token { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<SelectListItem> Providers { get; set; }
    }

}