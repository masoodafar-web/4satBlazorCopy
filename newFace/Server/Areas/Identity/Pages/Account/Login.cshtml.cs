using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using newFace.Shared.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using newFace.Shared.Models.Resource;
using newFace.Shared.Repositories.Resource;
using newFace.Shared.Models.Financial;
using newFace.Shared.Repositories.Generic;

namespace newFace.Server.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly ISendRepository _sendRepository;
        private readonly ICommissionRepository _commissionRepository;
        private IUnitOfWork _unitOfWork;

        public LoginModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger, ISendRepository sendRepository, ICommissionRepository commissionRepository, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _sendRepository = sendRepository;
            _commissionRepository = commissionRepository;
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public LoginViewModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }


        public async Task OnGetAsync(string message,string returnUrl = null)
        {
            ErrorMessage = message;
            if (!User.Identity.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    //split And Find Referal UserId
                    var returnUrlSplit = returnUrl.Split(new string[] { "referalId=" }, StringSplitOptions.None);
                    var refralUserId = returnUrlSplit.Count() > 1 ? returnUrlSplit[1] : null;

                    if (refralUserId == "0")
                    {
                        var AdminUserId = _userManager.GetUsersInRoleAsync("1").Result.FirstOrDefault()?.Id;
                        if (AdminUserId != null)
                        {

                            var coockie = _commissionRepository.CachReferralCode(AdminUserId);
                            if (coockie.Statue == Enums.Statue.Success)
                            {

                            }
                        }
                    }
                    else if (refralUserId != null)
                    {
                        var refraluerId = int.Parse(refralUserId);
                        refralUserId = _unitOfWork.UserGeneologyGR.FirstOrDefault(p => p.Id == refraluerId && p.Geneologytype.SystemType == SystemType.ForsatReagent && p.Geneologytype.Type == GeneologyTypeEnum.ForsatReagent)?.UserId;

                        var coockie = _commissionRepository.CachReferralCode(refralUserId);
                        if (coockie.Statue == Enums.Statue.Success)
                        {

                        }
                    }
                }


            }
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);


            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                ApplicationUser user = _userManager.Users.FirstOrDefault(u => u.PhoneNumber == Input.UserName || u.UserName == Input.UserName || u.Email == Input.UserName);
                if (user==null)
                {
                    ModelState.AddModelError("Input.UserName", "کاربری با این نام کاربری یا ایمیل یا شماره موبایل وجود ندارد");
                    return Page();
                }
                var result = await _signInManager.PasswordSignInAsync(user.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    HttpContext.Response.Cookies.Append("UserName4sat", user.PhoneNumber);
                    HttpContext.Response.Cookies.Append("Password4sat", Input.Password);
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }

                if (result.IsNotAllowed)
                {
                    if (!user.PhoneNumberConfirmed)
                    {
                        ModelState.AddModelError("code", "موبایل شما تایید نشده است");
                        var code = _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
                        var message = new IdentityMessage
                        {
                            Destination = user.PhoneNumber,
                            Body = " کد امنیتی شما در اپلیکیشن فرصت: " + code
                        };
                        _sendRepository.Sms(message.Destination, message.Body);
                        return RedirectToPage("./VerifyPhoneNumber", new { PhoneNumber = user.PhoneNumber, UserId = user.Id });
                    }
                    else
                    {
                        return RedirectToPage("./ConfirmEmail", new { PhoneNumber = user.PhoneNumber, UserId = user.Id });

                    }

                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError("", "ورود با خطا مواجه شد اطالاعات ورود به ویژه کلمه عبور را بررسی کنید ");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
