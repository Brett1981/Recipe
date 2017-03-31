using System.Web.Mvc;

namespace RecipeManager.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "UserRecipes", null);
        }
    }
}