using System;
using System.Collections.Generic;

namespace  Data;

public partial class Post
{
    public int Id { get; set; }

    public int? TrailId { get; set; }

    public int? RegionId { get; set; }

    public int? UserId{get;set;}

    public string Title { get; set; } = null!;

    public int? Type { get; set; }

    public string? Content { get; set; }

    public DateTime? Date { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Region? Region { get; set; }

    public virtual Trail? Trail { get; set; }
    public virtual UserEntity User{get;set;}

    public string? FileName{get;set;}
}
