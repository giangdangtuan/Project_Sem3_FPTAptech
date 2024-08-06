using System;
using System.Collections.Generic;

namespace Italy_Recipe_Page.Models;

public partial class Category
{
    public int CatId { get; set; }

    public string? CatName { get; set; }

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
