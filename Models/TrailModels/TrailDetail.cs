using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Models.PostModels;

namespace Models.TrailModels
{
    public class TrailDetail
    {
    public int Id{get;set;}
    public int? AdminId { get; set; }

    public int? RegionId { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

  

    public TrailStatus Status { get; set; }

    public DateTime? LastUpdate { get; set; }

    public ICollection<PostDetail> Posts = new List<PostDetail>();
    }
}