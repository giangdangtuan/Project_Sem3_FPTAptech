namespace Italy_Recipe_Page.Models;
using Italy_Recipe_Page.Models;
public class RecipeViewModel
{
	public Recipe Recipe { get; set; }
	public List<Ingredient> Ingredients { get; set; }

	public List<Recipe> Recipes { get; set; }

	public List<Comment> Comments { get; set; }
}
