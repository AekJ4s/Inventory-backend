using System;
using System.Collections.Generic;

namespace Inventory.Models;

public partial class Login
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }
}
