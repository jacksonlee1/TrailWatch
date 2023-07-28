using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.RegionModels
{
    public class RegionListItem
    {
        public int Id{get;set;}
        public string Name { get; set; } = null!;

        public string Type { get; set; } = null!;

        public int NumTrails{get;set;}
    
    }
}