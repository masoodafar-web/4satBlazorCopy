using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using newFace.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace newFace.Server.Areas.Identity.Pages.Account
{
    public class VerifyPhoneNumberModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public VerifyPhoneNumberModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [BindProperty(SupportsGet = true)]
        public VerifyPhoneNumberViewModel Input { get; set; }

        

        public async Task OnGetAsync(string PhoneNumber, string UserId, bool? ForgotPassword)
        {
            Input.PhoneNumber = PhoneNumber;
            Input.UserId = UserId;
            Input.ForgotPassword = ForgotPassword;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            IdentityResult result = new IdentityResult();
            if (!string.IsNullOrEmpty(Input.UserId))
            {
                var user = await _userManager.FindByIdAsync(Input.UserId);

                //فعلا تا پنل اس ام اس فعال شود
                Input.Code =await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);

                result = await _userManager.ChangePhoneNumberAsync(user, Input.PhoneNumber, Input.Code);

            }
            else
            {

                var user =await _userManager.GetUserAsync(User);

                //فعلا تا پنل اس ام اس فعال شود
                Input.Code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);

                result = await _userManager.ChangePhoneNumberAsync(user, Input.PhoneNumber, Input.Code);
                Input.UserId = user.Id;
            }

            if (result.Succeeded)
            {
                var Appuser = await _userManager.FindByIdAsync(Input.UserId);
                if (Appuser != null)
                {
                    if (Input.ForgotPassword == true)
                    {
                        string CodeToken = await _userManager.GeneratePasswordResetTokenAsync(Appuser);

                        return RedirectToPage("./ResetPassword",new { code = CodeToken, PhoneNumber = Input.PhoneNumber });

                    }
                    await _signInManager.SignInAsync(Appuser, isPersistent: false);
                }


               return LocalRedirect(Url.Content("~/"));

            }
            else
            {
                ViewData["message"] = "کد تایید اشباه است!!";
                return Page();
            }
        }
    }
}
