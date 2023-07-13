using System;
using System.Collections.Generic;

namespace Data;

public partial class Trail
{
    public int Id { get; set; }

    public int? AdminId { get; set; }

    public int? RegionId { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int Difficulty { get; set; }

    public int Status { get; set; }

    public DateTime? LastUpdate { get; set; }

    public virtual User? Admin { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual Region? Region { get; set; }
}
