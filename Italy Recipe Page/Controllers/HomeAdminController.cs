using Italian_Restaurant_1.Models;
using Italy_Recipe_Page.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Italian_Restaurant_1.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        ItalyContext db = new ItalyContext();
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
        // Category content
        [Route("category")]
        public IActionResult Category()
        {
            var category = db.Categories.ToList();
            return View(category);
        }

		[Route("bill")]
		public IActionResult Bill()
		{
			var bill = db.Bills.ToList();
			return View(bill);
		}
		// Recipes list content
		[Route("recipeslist")]
        public IActionResult RecipesList(int? page)
        {
            int pageSize = 4;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var recipeslist = db.Recipes.AsNoTracking().OrderBy(x => x.RecipeId);
            PagedList<Recipe> lst = new PagedList<Recipe>(recipeslist, pageNumber, pageSize);
            return View(lst);
        }
        // Add Recipe
        [Route("createrecipe")]
        [HttpGet]
        public IActionResult CreateRecipe()
        {
            ViewBag.UserId = new SelectList(db.Users.ToList(), "UserId", "Username");
            ViewBag.Category = new SelectList(db.Categories.ToList(), "CatId", "CatName");
            return View();
        }
        [Route("createrecipe")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRecipe(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                db.Recipes.Add(recipe);
                db.SaveChanges();
                return RedirectToAction("recipeslist");
            }
            return View(recipe);
        }
        // Update recipe
        [Route("updateRecipe")]
        [HttpGet]
        public IActionResult UpdateRecipe(int RecipeId)
        {
            ViewBag.UserId = new SelectList(db.Users.ToList(), "UserId", "Username");
            ViewBag.Category = new SelectList(db.Categories.ToList(), "CatId", "CatName");
            var recipe = db.Recipes.Find(RecipeId);
            return View(recipe);
        }
        [Route("updateRecipe")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateRecipe(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recipe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("recipeslist");
            }
            return View(recipe);
        }
        // Delete recipe
        [Route("deleteRecipe")]
        [HttpGet]
        public IActionResult DeleteRecipe(int recipeId)
        {
            TempData["Message"] = "";
            var recipeDetail = db.RecipeDetails.Where(x => x.RecipeId == recipeId).ToList();
            if (recipeDetail.Count() > 0)
            {
                TempData["Message"] = "Can't delete this recipe";
                return RedirectToAction("recipeslist");
            }
            db.Remove(db.Recipes.Find(recipeId));
            db.SaveChanges();
            TempData["Message"] = "Delete success";
            return RedirectToAction("recipeslist");
        }

        // Tips list content
        [Route("tipsList")]
        public IActionResult TipsList(int? page)
        {
            int pageSize = 4;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var tipslist = db.Tips.AsNoTracking().OrderBy(x => x.TipId);
            PagedList<Tip> lst = new PagedList<Tip>(tipslist, pageNumber, pageSize);
            return View(lst);
        }
        // Add Tip
        [Route("createTip")]
        [HttpGet]
        public IActionResult CreateTip()
        {
            ViewBag.UserId = new SelectList(db.Users.ToList(), "UserId", "Username");
            return View();
        }
        [Route("createTip")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTip(Tip tip)
        {
            if (ModelState.IsValid)
            {
                db.Tips.Add(tip);
                db.SaveChanges();
                return RedirectToAction("tipsList");
            }
            return View(tip);
        }
        // Update tip
        [Route("updateTip")]
        [HttpGet]
        public IActionResult UpdateTip(int TipId)
        {
            ViewBag.UserId = new SelectList(db.Users.ToList(), "UserId", "Username");
            var tip = db.Tips.Find(TipId);
            return View(tip);
        }
        [Route("updateTip")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateTip(Tip tip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("tipsList");
            }
            return View(tip);
        }
        // Delete tip
        [Route("deleteTip")]
        [HttpGet]
        public IActionResult DeleteTip(int tipId)
        {
            TempData["Message"] = "";
            db.Remove(db.Tips.Find(tipId));
            db.SaveChanges();
            TempData["Message"] = "Delete success";
            return RedirectToAction("tipsList");
        }
        // Test content
        [Route("testsList")]
        public IActionResult TestsList(int? page)
        {
            int pageSize = 4;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var testslist = db.Tests.AsNoTracking().OrderBy(x => x.TestId);
            PagedList<Test> lst = new PagedList<Test>(testslist, pageNumber, pageSize);
            return View(lst);
        }
        // Create Test
        [Route("createTest")]
        [HttpGet]
        public IActionResult CreateTest()
        {
            return View();
        }
        [Route("createTest")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTest(Test test)
        {
            if (ModelState.IsValid)
            {
                db.Tests.Add(test);
                db.SaveChanges();
                return RedirectToAction("testsList");
            }
            return View(test);
        }
        // Update test
        [Route("updateTest")]
        [HttpGet]
        public IActionResult UpdateTest(int TestId)
        {
            var test = db.Tips.Find(TestId);
            return View(test);
        }
        [Route("updateTest")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateTest(Test test)
        {
            if (ModelState.IsValid)
            {
                db.Entry(test).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("testsList");
            }
            return View(test);
        }
        // Delete tip
        [Route("deleteTest")]
        [HttpGet]
        public IActionResult DeleteTest(int TestId)
        {
            TempData["Message"] = "";
            db.Remove(db.Tests.Find(TestId));
            db.SaveChanges();
            TempData["Message"] = "Delete success";
            return RedirectToAction("testsList");
        }
        // TestDetail content
        [Route("testDetail")]
        public IActionResult TestDetail(int TestId)
        {
            List<TestDetail> lsttest = db.TestDetails.Where(x => x.TestId == TestId).ToList();
            return View(lsttest);
        }

        [Route("profile")]
        public IActionResult Profile()
        {
            var userInfo = db.Users.ToList();
            return View(userInfo);
        }
        [Route("commentsList")]
        public IActionResult CommentsList(int? page)
        {
            int pageSize = 4;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var commentslist = db.Comments.AsNoTracking().OrderBy(x => x.Id);
            PagedList<Comment> lst = new PagedList<Comment>(commentslist, pageNumber, pageSize);
            return View(lst);
        }
    }
}
