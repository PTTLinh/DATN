using System;
using System.Collections.Generic;

namespace K21CNT2_2110900055_DATN.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? Description { get; set; }

    public int? CategoryId { get; set; }

    public int? Price { get; set; }

    public string? Image { get; set; }

    public int? UnitslnStock { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
