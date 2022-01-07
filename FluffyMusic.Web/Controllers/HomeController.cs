using Microsoft.AspNetCore.Mvc;

namespace FluffyMusic.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToActionPermanent("Index", "Music");
            }

            return View();
        }
    }
}
