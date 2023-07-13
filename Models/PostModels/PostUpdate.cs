using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.PostModels
{
    public class PostUpdate
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public int? Type { get; set; }

        public string? Content { get; set; }
    }
}