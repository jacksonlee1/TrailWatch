using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.TrailModels
{
    public class TrailListItem
    {
    public string Name { get; set; } = null!;

    public int Status { get; set; }

    public DateTime? LastUpdate { get; set; }
    }
}