using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;

namespace Models.RegionModels
{
    public class RegionDetail
    {
      public int RegionId{get;set;}
        public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

      public  User? Admin { get; set; }

    public  ICollection<Post> Posts { get; set; } = new List<Post>();

    public  ICollection<Trail> Trails { get; set; } = new List<Trail>();
    }
}