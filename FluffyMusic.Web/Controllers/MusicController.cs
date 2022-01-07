using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FluffyMusic.Web.OAuth.Discord;
using System.Linq;
using System.Security.Claims;

namespace FluffyMusic.Web.Controllers
{
    [Authorize(AuthenticationSchemes = DiscordOAuthDefaults.AuthenticationScheme)]
    public class MusicController : Controller
    {
        public IActionResult Index()
        {
            string userId = GetClaimValByType(ClaimTypes.NameIdentifier);
            string userAvatar = GetClaimValByType(DiscordClaimTypes.Avatar);
            string discriminator = GetClaimValByType(DiscordClaimTypes.Discriminator);
            var avatarUrl = string.Format("{0}avatars/{1}/{2}.jpg",
                DiscordOAuthDefaults.DiscordImgUrl,
                userId,
                userAvatar
                );

            ViewBag.UserAvatarUrl = avatarUrl;
            ViewBag.UserDiscriminator = discriminator;
            return View();
        }

        private string GetClaimValByType(string claimType)
        {
            return User.Claims.First(u => u.Type == claimType).Value;
        }
    }
}
