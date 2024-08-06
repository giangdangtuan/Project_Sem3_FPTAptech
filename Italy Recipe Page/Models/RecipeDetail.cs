using System;
using System.Collections.Generic;

namespace Italy_Recipe_Page.Models;

public partial class RecipeDetail
{
    public int? RecipeId { get; set; }

    public int? IngredientId { get; set; }

    public virtual Ingredient? Ingredient { get; set; }

    public virtual Recipe? Recipe { get; set; }
}
