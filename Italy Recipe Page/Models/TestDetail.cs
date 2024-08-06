using System;
using System.Collections.Generic;

namespace Italy_Recipe_Page.Models;

public partial class TestDetail
{
    public int? TestId { get; set; }

    public int? UserId { get; set; }

    public string? Description { get; set; }

    public bool? TickWinner { get; set; }

    public virtual Test? Test { get; set; }

    public virtual User? User { get; set; }
}
