using System;
using System.Collections.Generic;

namespace Italy_Recipe_Page.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Pass { get; set; }

    public string? Gmail { get; set; }

    public string? PhoneNo { get; set; }

    public string? Image { get; set; }

    public string? UserRole { get; set; }

    public string? RoleStatus { get; set; }

    public DateOnly? RoleEndDate { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();

    public virtual ICollection<Tip> Tips { get; set; } = new List<Tip>();
}
