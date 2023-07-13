using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.RegionModels
{
    public class RegionCreate
    {
        public int? AdminId { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;
    }
}