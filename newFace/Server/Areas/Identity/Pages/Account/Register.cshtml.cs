using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using newFace.Shared.Models;
using newFace.Shared.Models.Financial;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.User;
using newFace.Shared.Repositories.Generic;
using newFace.Shared.Repositories.Resource;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace newFace.Server.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IUserRepository _userRepository;
        private readonly ISendRepository _sendRepository;
        private readonly ICommissionRepository _commissionRepository;
        private IUnitOfWork _unitOfWork;

        public RegisterModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ILogger<RegisterModel> logger, IUserRepository userRepository, ISendRepository sendRepository, ICommissionRepository commissionRepository, IUnitOfWork unitOfWork)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _userRepository = userRepository;
            _sendRepository = sendRepository;
            _commissionRepository = commissionRepository;
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public RegisterViewModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

    

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            #region RemoveFakeUsers
            var removeResult = _userRepository.RemoveFakeUsers();
            if (!removeResult)
            {
                ModelState.AddModelError("", "حذف کاربران فیک با خطا همراه بود");
                return Page();
            }
            #endregion

            returnUrl = returnUrl ?? Url.Content("~/");
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                #region CreateAsync
                if (_userManager.Users.Any(a => a.Email == Input.Email))
                {
                    ModelState.AddModelError("Email", @"ایمیل وارد شده تکراریست");
                    return Page();
                }
                ApplicationUser user = new ApplicationUser { UserName = Input.Email.Split('@')[0], Email = Input.Email, FullName = Input.FullName, Phone = Input.PhoneNumber, PhoneNumber = Input.PhoneNumber };
                user.Img = "/Content/img/default_logo.jpg";
                user.NickName = "نام مستعار";
                user.AboutMe = "درباره من";
                var result = await _userManager.CreateAsync(user, Input.Password);
                #endregion

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    #region ProjectSetting
                    var ProjectSetting = _unitOfWork.ProjectSettingGR.GetAll().FirstOrDefault();
                    if (ProjectSetting != null)
                    {
                        var Skill = new Skill
                        {
                            UserId = user.Id,
                            CategoryId = ProjectSetting.DefultCategoryId,
                            Percent = 0,
                            SkillType = SkillType.Adventitious,
                            Credit = 0
                        };
                        _unitOfWork.SkillGR.Add(Skill);
                    }
                    //Create User Setting
                    var setting = new UserSetting
                    {
                        UserId = user.Id,

                        IntroAdvice = false,
                        IntroFirstPage = false,
                        IntroGoals = false,
                        IntroProfile = false,
                        IntroShop = false,
                        AdviceNotification = true

                    };
                    _unitOfWork.UserSettingGR.Add(setting);
                    #endregion

                    #region Send sms Message
                    //Send sms Message
                    var code = _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
                    var message = new IdentityMessage
                    {
                        Destination = user.PhoneNumber,
                        Body = " کد امنیتی شما در اپلیکیشن فرصت: " + code
                    };
                    _sendRepository.Sms(message.Destination, message.Body);
                    #endregion

                    #region UserGeneology Add
                    var getrefid = _commissionRepository.ReadReferralCode();
                    int GeneologyTypeId = 0;
                    if (_unitOfWork.GeneologyTypeGR.Any(p => p.SystemType == SystemType.ForsatReagent && p.Type == GeneologyTypeEnum.ForsatReagent))
                        GeneologyTypeId = _unitOfWork.GeneologyTypeGR.FirstOrDefault(p => p.SystemType == SystemType.ForsatReagent && p.Type == GeneologyTypeEnum.ForsatReagent).Id;

                    UserGeneology ug = new UserGeneology
                    {
                        GeneologyTypeId = GeneologyTypeId,
                        UserId = user.Id

                    };

                    if (string.IsNullOrEmpty(getrefid))
                    {
                        ug.ParentId = null;
                    }
                    else
                    {
                        ug.ParentId = getrefid;
                    }

                    var genoAdd = _unitOfWork.UserGeneologyGR.Add(ug);
                    #endregion

                    return RedirectToPage("./VerifyPhoneNumber", new { PhoneNumber = user.PhoneNumber, UserId = user.Id });

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
