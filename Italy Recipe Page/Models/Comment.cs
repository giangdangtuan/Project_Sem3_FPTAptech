using System;
using System.Collections.Generic;

namespace Italy_Recipe_Page.Models;

public partial class Comment
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? RecipeId { get; set; }

    public int? TipId { get; set; }

    public string? Description { get; set; }

    public virtual Recipe? Recipe { get; set; }

    public virtual Tip? Tip { get; set; }

    public virtual User? User { get; set; }
}
