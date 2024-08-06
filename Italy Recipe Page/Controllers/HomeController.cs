using Italy_Recipe_Page.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using X.PagedList;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Italian_Restaurant_1.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		ItalyContext db = new ItalyContext();
		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		//[Authentication]
		public IActionResult Index(int? page)
		{
			int pageSize = 5;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			
			var ListRecipe = db.Recipes.AsNoTracking().Where(x => x.RecipeRole.Equals("0") || x.RecipeRole.Equals("1")).OrderBy(x => x.RecipeName);
			PagedList<Recipe> lst = new PagedList<Recipe>(ListRecipe, pageNumber, pageSize);
			return View(lst);
		}

		public IActionResult contest()
		{
			return View();
		}

		public IActionResult RecipeDetail(int maSp)
		{
			int x = 0;
			var Recipe = db.Recipes.SingleOrDefault(x => x.RecipeId == maSp);
			List<Ingredient> res = (from t in db.Ingredients join t2 in db.RecipeDetails on t.Id equals t2.IngredientId where t2.RecipeId == maSp select t).ToList();
			var ListRecipe = db.Recipes.AsNoTracking().OrderBy(x => x.RecipeName).ToList();
			List<Comment> ListComment = (from t in db.Comments join t2 in db.Recipes on t.RecipeId equals t2.RecipeId where t2.RecipeId == maSp select t).ToList();
			var viewModel = new RecipeViewModel
			{
				Recipe = Recipe,
				Ingredients = res,
				Recipes = ListRecipe,
				Comments = ListComment
			};
			if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserRole")))
			{
				x = 0;
			}
			else
			{
				x = Int32.Parse(HttpContext.Session.GetString("UserRole"));
			}

			if (x >= Int32.Parse(Recipe.RecipeRole))
			{
				return View(viewModel);
			}
			return View("~/Views/Shared/NotificationRole.cshtml");

		}

		public IActionResult CategoryRecipe(int maLoai, int? page)
		{
			int pageSize = 5;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var ListRecipe = db.Recipes.Where(x => x.Category == maLoai).OrderBy(x => x.RecipeName).AsNoTracking().OrderBy(x => x.RecipeName);
			PagedList<Recipe> lst = new PagedList<Recipe>(ListRecipe, pageNumber, pageSize);
			return View(lst);

		}

		public IActionResult LiveTagSearch(string search)
		{

			List<Recipe> res = (
			from t in db.Recipes
			where t.RecipeName.Contains(search)
			select t
			).ToList();

			return View(res);
		}

		public IActionResult Tips(int? page)
		{
			int pageSize = 3;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var ListTips = db.Tips.AsNoTracking().OrderBy(x => x.TipId);
			PagedList<Tip> lst = new PagedList<Tip>(ListTips, pageNumber, pageSize);
			return View(lst);
		}

		public IActionResult faq()
		{
			return View();
		}

		public IActionResult shop(int? page)
		{

            int pageSize = 3;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var ListPack = db.Packs.AsNoTracking().OrderBy(x => x.PackId);
			PagedList<Pack> lst = new PagedList<Pack>(ListPack, pageNumber, pageSize);
			return View(lst);
    }


		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
