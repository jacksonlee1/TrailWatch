using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.PostModels
{
    public class PostDetail
    {
         public int Id { get; set; }
         public string Title { get; set; } = null!;

    public int? Type { get; set; }

    public string? Content { get; set; }

    public DateTime? Date { get; set; }
    }
}