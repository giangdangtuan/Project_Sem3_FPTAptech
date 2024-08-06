using System;
using System.Collections.Generic;

namespace Italy_Recipe_Page.Models;

public partial class BillDetail
{
    public int? BillId { get; set; }

    public int? UserId { get; set; }

    public int? PackId { get; set; }

    public DateOnly? StartDate { get; set; }

    public virtual Bill? Bill { get; set; }

    public virtual Pack? Pack { get; set; }

    public virtual User? User { get; set; }
}
