using FluffyMusic.Web.OAuth.Discord;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FluffyMusic.Web.Controllers
{
    [Authorize(AuthenticationSchemes = DiscordOAuthDefaults.AuthenticationScheme)]
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToActionPermanent("Index", "Music");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToActionPermanent("Index", "Home");
        }
    }
}
