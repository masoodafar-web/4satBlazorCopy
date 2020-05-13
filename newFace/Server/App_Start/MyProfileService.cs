using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using newFace.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace newFace.Server.App_Start
{
    public class ProfileService : IProfileService
    {
        protected UserManager<ApplicationUser> _userManager;

        public ProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //>Processing
            var user = await _userManager.GetUserAsync(context.Subject);

            var securityStampClaims = new List<Claim>
        {
            new Claim("SecurityStamp", user.SecurityStamp),
        };
            var userNameClaims = new List<Claim>
            {
                new Claim("UserName", user.UserName),
            };
            var userIdClaims = new List<Claim>
            {
                new Claim("UserId", user.Id),
            };
            context.IssuedClaims.AddRange(securityStampClaims);
            context.IssuedClaims.AddRange(userIdClaims);
            context.IssuedClaims.AddRange(userNameClaims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            //>Processing
            var user = await _userManager.GetUserAsync(context.Subject);

            context.IsActive = (user != null) && user.Active;
        }
    }
}
