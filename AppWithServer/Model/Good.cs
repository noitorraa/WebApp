using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppWithServer.Model;

public partial class Good
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Discription { get; set; }

    public bool InStock { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
