using System;
using System.Collections.Generic;

namespace OnlineShopping.Models;

public partial class Address
{
    public int AddId { get; set; }

    public int? LoginId { get; set; }

    public string FullAddress { get; set; } = null!;

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public int Pincode { get; set; }

    public virtual User? Login { get; set; }
}
