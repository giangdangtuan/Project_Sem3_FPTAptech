namespace Italy_Recipe_Page.Models;
public class FullRecipe
{
	public Recipe Recipe { get; set; }
	public List<Ingredient> Ingredients { get; set; }
	public List<Recipe> Recipes { get; set; }
}
