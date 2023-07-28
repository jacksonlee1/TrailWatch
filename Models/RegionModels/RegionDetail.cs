using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Models.PostModels;
using Models.TrailModels;

namespace Models.RegionModels
{
    public class RegionDetail
    {
        public int RegionId { get; set; } 
        public string Name { get; set; } = null!;

        public string Type { get; set; } = null!;

        public int AdminId { get; set; }

        public ICollection<PostDetail> Posts { get; set; } = new List<PostDetail>();

        public ICollection<TrailDetail> Trails { get; set; } = new List<TrailDetail>();
    }
}