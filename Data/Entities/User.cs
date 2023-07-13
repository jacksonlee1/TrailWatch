using System;
using System.Collections.Generic;

namespace Data;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Region> Regions { get; set; } = new List<Region>();

    public virtual ICollection<Trail> Trails { get; set; } = new List<Trail>();
}
