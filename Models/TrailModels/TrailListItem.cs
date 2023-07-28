using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;

namespace Models.TrailModels
{
    public class TrailListItem
    {
    public int Id {get;set;}
    public string Name { get; set; } = null!;

    public TrailStatus Status { get; set; }

    public DateTime? LastUpdate { get; set; }
    }
}