using Italy_Recipe_Page.Models;
using Italy_Recipe_Page.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Italian_Restaurant_1.ViewComponents
{
	public class CategoryViewComponent : ViewComponent
	{
		private readonly RecipeCategoryInterface _CategoryRecipe;
		public CategoryViewComponent(RecipeCategoryInterface CategoryRecipe)
		{
			_CategoryRecipe = CategoryRecipe;
		}
	
		public IViewComponentResult Invoke()
		{
			var category = _CategoryRecipe.GetAllCategory().OrderBy(x=>x.CatId);
			return View(category);
		}
	}
}
