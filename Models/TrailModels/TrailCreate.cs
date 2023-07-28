using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;

namespace Models.TrailModels
{
    public class TrailCreate
    {
        
    public int? AdminId { get; set; }

    public int? RegionId { get; set; }

    public string Name { get; set; } = null!;

    public TrailType Type { get; set; }

    public int Difficulty { get; set; }

    public TrailStatus Status { get; set; }
    }
}