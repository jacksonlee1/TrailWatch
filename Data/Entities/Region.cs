using System;
using System.Collections.Generic;

namespace Data;

public partial class Region
{
    public int Id { get; set; }

    public int? AdminId { get; set; }

    
public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;
    public virtual User? Admin { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Trail> Trails { get; set; } = new List<Trail>();
}
