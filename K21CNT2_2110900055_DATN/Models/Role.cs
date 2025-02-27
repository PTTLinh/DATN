using System;
using System.Collections.Generic;

namespace K21CNT2_2110900055_DATN.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
