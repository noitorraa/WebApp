﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppWithServer.Model;

public partial class User
{
    public int IdUser { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool? Role { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
