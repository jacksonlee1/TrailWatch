using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.TrailModels
{
    public class TrailUpdate
    {
    public int Id { get; set; }

    public int? AdminId { get; set; }

    public int? RegionId { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int Difficulty { get; set; }

    public int Status { get; set; }
    }
}