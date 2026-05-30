using System;
using System.Collections.Generic;

namespace OnlineShopping.Models;

public partial class CartItem
{
    public int CartItemId { get; set; }

    public int? CartId { get; set; }

    public int? PrdId { get; set; }

    public int? Qty { get; set; }

    public virtual Cart? Cart { get; set; }

    public virtual Product? Prd { get; set; }
}
