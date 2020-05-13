using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;
using newFace.Shared.Repositories.Resource;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace newFace.Server.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISendRepository _sendRepository;

        public ForgotPasswordModel(UserManager<ApplicationUser> userManager, ISendRepository sendRepository)
        {
            _userManager = userManager;
            _sendRepository = sendRepository;
        }

        [BindProperty]
        public ForgotPasswordViewModel Input { get; set; }

     

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
               
                ApplicationUser user = _userManager.Users.FirstOrDefault(w => w.PhoneNumber == Input.phoneNumber);

                if (user == null)
                {
                    ModelState.AddModelError("Input.phoneNumber", "شماره موبایل یافت نشد");
                    return Page();
                }
                else
                {
                    var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, Input.phoneNumber);
                    var message = new IdentityMessage
                    {
                        Destination = Input.phoneNumber,
                        Body = " کد امنیتی شما در اپلیکیشن فرصت: " + code
                    };
                    _sendRepository.Sms(message.Destination, message.Body);
                }
                return RedirectToPage("./VerifyPhoneNumber", new { PhoneNumber = user.PhoneNumber, UserId = user.Id, ForgotPassword = true });

            }
            ModelState.AddModelError("", string.Join("; ", ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage)));

            return Page();
        }
    }
}
