using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Models.PostModels;
using Models.RegionModels;
using Models.UserModels;

namespace TrailWatchMVC.Models
{
    public class RegionVM
    {
     public UserDetail Admin{get;set;} = new();
     public PostCreate postCreate{get;set;} = new();

     public RegionDetail Region{get;set;} = new();

     

    }
}