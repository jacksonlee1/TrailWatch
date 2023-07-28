using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Models.PostModels
{
    public class PostCreate
    {
    

    public int? TrailId { get; set; }

    public int? RegionId { get; set; }

    public string Title { get; set; } = null!;

    public int? Type { get; set; }

    public string? Content { get; set; }

    public IFormFile? Image{get;set;}

    public string? FileName{get;set;}
    }
}