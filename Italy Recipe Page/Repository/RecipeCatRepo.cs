using Italy_Recipe_Page.Models;

namespace Italy_Recipe_Page.Repository
{
	public class RecipeCatRepo : RecipeCategoryInterface
	{
		private readonly ItalyContext _context;

		public RecipeCatRepo(ItalyContext context)
		{
			_context = context;
		}

		Category RecipeCategoryInterface.Add(Category category)
		{
			_context.Categories.Add(category);
			_context.SaveChanges();
			return category;
		}
		Category RecipeCategoryInterface.Update(Category category)
		{
			_context.Update(category);
			_context.SaveChanges();
			return category;
		}
		Category RecipeCategoryInterface.Delete(int catID)
		{
			throw new NotImplementedException();
		}
		Category RecipeCategoryInterface.Get(int catID)
		{
			return _context.Categories.Find(catID);
		}

		IEnumerable<Category> RecipeCategoryInterface.GetAllCategory()
		{
			return _context.Categories;
		}
	}
}
