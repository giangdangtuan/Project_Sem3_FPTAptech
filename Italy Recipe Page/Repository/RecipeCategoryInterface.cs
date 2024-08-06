using Italy_Recipe_Page.Models;
namespace Italy_Recipe_Page.Repository
{
	public interface RecipeCategoryInterface
	{
		Category Add(Category category);
		Category Update(Category category);
		Category Delete(int catID);
		Category Get(int catID);

		IEnumerable<Category> GetAllCategory();

	}
}
