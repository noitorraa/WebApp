using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppWithServer.Model;

public partial class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int GoodsId { get; set; }

    public int Count { get; set; }
    public User User { get; set; } = null!;
    public Good Good { get; set; } = null!;
}
