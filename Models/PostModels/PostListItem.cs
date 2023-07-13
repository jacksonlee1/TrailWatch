using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.PostModels
{
    public class PostListItem
    {
            public string Title { get; set; } = null!;
    public int? Type { get; set; }

     public DateTime? Date { get; set; }
    }
}