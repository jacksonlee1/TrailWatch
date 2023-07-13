using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.CommentModels
{
    public class CommentCreate
    {
        public int? PostId { get; set; }

        public int? UserId { get; set; }

        public string Content { get; set; } = null!;
    }
}