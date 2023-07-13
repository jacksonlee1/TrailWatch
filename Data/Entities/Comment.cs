﻿using System;
using System.Collections.Generic;

namespace Data;

public partial class Comment
{
    public int Id { get; set; }

    public int? PostId { get; set; }

    public int? UserId { get; set; }

    public string Content { get; set; } = null!;

    public virtual Post? Post { get; set; }

    public virtual User? User { get; set; }
}
