using System;
using System.Collections.Generic;

namespace OnlineShopping.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int? LoginId { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual User? Login { get; set; }
}
