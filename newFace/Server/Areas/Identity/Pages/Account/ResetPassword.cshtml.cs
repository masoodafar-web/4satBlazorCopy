using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using newFace.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace newFace.Server.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ResetPasswordModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty(SupportsGet = true)]
        public ResetPasswordViewModel Input { get; set; }



        public IActionResult OnGet(string PhoneNumber, string message = "", string code = null)
        {
           ViewData["message"] = message;
            if (code == null)
            {
                return BadRequest("برای تغییر کلمه عبور کد صحیحی وارد نشده است");
            }
            else
            {
                Input = new ResetPasswordViewModel()
                {
                    Code = code,
                    PhoneNumber = PhoneNumber
                };
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            ApplicationUser user = _userManager.Users.FirstOrDefault(w => w.PhoneNumber == Input.PhoneNumber);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToPage("./ResetPassword", new { code = Input.Code, message = "کاربری با این شماره موبایل یافت نشد!!" });
            }
            IdentityResult result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {
                //return RedirectToAction("ResetPasswordConfirmation", "Account", new { Message = "u" });
                return RedirectToPage("./Login", new { message = "عملیات مربوط به کلمه عبور با موفقیت انجام شد ,اکنون وارد شوید." });
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }

    }
}
