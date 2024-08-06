using System;
using System.Collections.Generic;

namespace Italy_Recipe_Page.Models;

public partial class Tip
{
    public int TipId { get; set; }

    public string? Description { get; set; }

    public string? TipName { get; set; }

    public int? UserId { get; set; }

    public string? TipRole { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User? User { get; set; }
}
