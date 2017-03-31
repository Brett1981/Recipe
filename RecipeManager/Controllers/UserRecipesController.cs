using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using RecipeManager.Models;
using Microsoft.AspNet.Identity;
namespace RecipeManager.Controllers
{
    [Authorize]
    public class UserRecipesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserRecipes
        public async Task<ActionResult> Index()
        {
            var currentUserId = User.Identity.GetUserId();
            ViewBag.UserName = db.Users.Find(currentUserId).FullName;
            var userRecipes = db.UserRecipes.Include(u => u.Recipe).Include(u => u.User);
            ViewBag.RecipeID = new SelectList(db.Recipes, "RecipeID", "Name", null);
            return View(await userRecipes.ToListAsync());
        }
        public ActionResult GetUserRecipes()
        {

            var currentUserId = User.Identity.GetUserId();
            var userRecipes = db.UserRecipes.Where(u => u.UserID == currentUserId).ToList();
            List<UserRecipeModel> userRecipeModeList = new List<UserRecipeModel>();
            foreach (var urg in userRecipes)
            {
                var ingredients = db.Ingredients.Where(i => i.RecipeID == urg.RecipeID).ToList();

                var ingList = (from ing in ingredients select new RecipeIngredient { IngredientID = ing.IngredientID, IngredientName = ing.Name, isChecked = false }).ToList();

                var userIngredients = db.UserIngredients.Where(i => i.UserRecipeID == urg.UserRecipeID ).ToList();
                foreach (var ui in userIngredients)
                {
                    ingList.FirstOrDefault(ing => ing.IngredientID == ui.IngredientID).isChecked = true;
                }
                userRecipeModeList.Add(new UserRecipeModel { RecipeName = urg.Recipe.Name, RecipeID = urg.RecipeID, RecipeIngredients = ingList });
            }

            return Json(userRecipeModeList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddUserRecipes(int? RecipeID)
        {
            var currentUserId = User.Identity.GetUserId();
            if (RecipeID == null) return Json("RecipeId not supplied", JsonRequestBehavior.AllowGet);
            var urecipe = db.UserRecipes.FirstOrDefault(u => u.RecipeID == RecipeID && u.UserID == currentUserId);
            if (urecipe != null) return Json("Already Added", JsonRequestBehavior.AllowGet);

           
            var ur = new UserRecipe { UserID = currentUserId, RecipeID = RecipeID.Value };
            db.UserRecipes.Add(ur);
            db.SaveChanges();
            return Json("Added", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult addUpateUserIngredients(int RecipeID, int IngredientID, bool isChecked)
        {
            var currentUserId = User.Identity.GetUserId();
            var urecipe = db.UserRecipes.FirstOrDefault(u => u.RecipeID == RecipeID && u.UserID == currentUserId);
            if (urecipe != null)
            {
                var uIng = db.UserIngredients.FirstOrDefault(u => u.UserRecipeID == urecipe.UserRecipeID && u.IngredientID == IngredientID);
                if (urecipe != null && !isChecked)
                {
                    db.UserIngredients.Remove(uIng);
                }
                else {
                    db.UserIngredients.Add(new UserIngredient {IngredientID=IngredientID,UserRecipeID= urecipe.UserRecipeID });

                }
                db.SaveChanges();
            }


            //var ur = new UserRecipe { UserID = currentUserId, RecipeID = RecipeID };
            //db.UserRecipes.Add(ur);
            //db.SaveChanges();
            return Json("Added", JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
