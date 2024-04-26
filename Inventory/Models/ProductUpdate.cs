using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Models;

public struct ProductUpdate

{
    [Required]

   public int Id {get; set;}
    public string? Name { get; set; }
    
    public string? Description { get; set; }

    public int? CategoryID { get; set; }

    public int? StockQuantity { get; set; }

    public int? Price { get; set; }

    


    
}
