using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
using PokemonStore.Utils;
using System;
using System.Collections.Generic;
using PokemonStore.Models;
using System.Threading.Tasks;

namespace PokemonStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        AppDbContext _db;
        public ShoppingCartController(AppDbContext context)
        {
            _db = context;
        }
        public ActionResult ClearShoppingCart()
        {
            HttpContext.Session.Remove(SessionVars.ShoppingCart); // clear out current cart
            HttpContext.Session.SetString(SessionVars.Message, "Cart Cleared"); // clear out current cart once order has been placed
            return Redirect("/Home");
        }

        // Add the order, pass the session variable info to the db
        public ActionResult AddOrder()
        {
            // they can't add a order if they're not logged on
            if (HttpContext.Session.GetString(SessionVars.User) == null)
            {
                return Redirect("/Login");
            }
            ShoppingCartModel model = new ShoppingCartModel(_db);
            int retVal = -1;
            string retMessage = "";
            try
            {
                Dictionary<string, object> trayItems = HttpContext.Session.GetObject<Dictionary<string, object>>(SessionVars.ShoppingCart);
                retVal = model.AddOrder(trayItems,HttpContext.Session.GetString(SessionVars.User));
                if (retVal > 0) // Tray Added
                {
                    retMessage = "Order " + retVal + " Created!";
                }
                else // problem
                {
                    retMessage = "Order not added, try again later";
                }
            }
            catch (Exception ex) // big problem
            {
                retMessage = "Order was not created, try again later! - " + ex.Message;
            }
            HttpContext.Session.Remove(SessionVars.ShoppingCart); // clear out current tray once persisted
            HttpContext.Session.SetString(SessionVars.Message, retMessage);
            return Redirect("/Home");
        }

        public IActionResult List()
        {
            if (HttpContext.Session.GetString(SessionVars.User) == null)
            {
                return Redirect("/Login");
            }
            return View("List");
        }
        [Route("[action]")]
        public IActionResult GetCarts()
        {
            ShoppingCartModel model = new ShoppingCartModel(_db);
            return Ok(model.GetCarts(HttpContext.Session.GetString(SessionVars.User)));
        }

        //[Route("[action]/{tid:int}")]
        //public IActionResult GetCartDetails(int tid)
        //{
        //    ShoppingCartModel model = new ShoppingCartModel(_db);
        //    return Ok(model.GetCartDetails(tid, HttpContext.Session.GetString(SessionVars.User)));
        //}

        [Route("[action]/{tid:int}")]
        public async Task<IActionResult> GetCartDetailsAsync(int tid)
        {
            ShoppingCartModel model = new ShoppingCartModel(_db);
            List<ViewModels.ShoppingCartViewModel> details = await model.GetCartDetailsAsync(tid,
            HttpContext.Session.GetString(SessionVars.User));
            return Ok(details);
        }
    }
}