using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
using PokemonStore.Utils;
using Microsoft.AspNet.Authorization;

namespace PokemonStore.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString(SessionVars.LoginStatus) == null)
            {
                HttpContext.Session.SetString(SessionVars.LoginStatus, "not logged in");
            }
            if (HttpContext.Session.GetString(SessionVars.LoginStatus) == "not logged in")
            {
                HttpContext.Session.SetString(SessionVars.Message, "Login now to place an order.");
            }
            ViewBag.Status = HttpContext.Session.GetString(SessionVars.LoginStatus);
            ViewBag.Message = HttpContext.Session.GetString(SessionVars.Message);
            return View();
        }
    }
}