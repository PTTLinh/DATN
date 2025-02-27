using System;
using System.Collections.Generic;

namespace K21CNT2_2110900055_DATN.Models;

public partial class Shipper
{
    public int ShipperId { get; set; }

    public string? ShipperName { get; set; }

    public string? Phone { get; set; }

    public DateTime? ShipDate { get; set; }
}
