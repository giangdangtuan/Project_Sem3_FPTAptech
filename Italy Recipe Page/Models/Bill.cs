using System;
using System.Collections.Generic;

namespace Italy_Recipe_Page.Models;

public partial class Bill
{
    public int BillId { get; set; }

    public decimal? Total { get; set; }
}
