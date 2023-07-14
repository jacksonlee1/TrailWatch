using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Data;

public partial class User:IdentityUser<int>
{
  

    public string UserEmail { get; set; } = null!;
    public override string Email => UserEmail;
    public string Username { get; set; } = null!;
    public override string UserName => Username;
    public string Password { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Region> Regions { get; set; } = new List<Region>();

    public virtual ICollection<Trail> Trails { get; set; } = new List<Trail>();
}
