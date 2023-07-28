using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Models.UserModels;

namespace Models.PostModels
{
    public class PostDetail
    {
         public int Id { get; set; }
         public string Title { get; set; } = null!;

    public int? Type { get; set; }

    public string? Content { get; set; }
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public DateTime? Date { get; set; }
    public UserDetail? User{get;set;}
    public int RegionId{get;set;}
    public int TrailId{get;set;}
    }
}