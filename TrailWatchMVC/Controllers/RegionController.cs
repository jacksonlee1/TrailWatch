using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.RegionModels;
using Services.RegionServices;

namespace TrailWatchMVC.Controllers
{
    [Route("[controller]")]
    public class Region : Controller
    {
        private readonly ILogger<Region> _logger;
        private readonly IRegionService _region;

        public Region(ILogger<Region> logger, IRegionService region)
        {
            _logger = logger;
            _region = region;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _region.GetAllRegionsAsync();
            return View(model);
        }
        [HttpGet("Create")]
         public IActionResult Create(){
            return View();
         }
        [HttpPost("Create")]
        public async Task<IActionResult> Create([Bind("Name,Type")]RegionCreate c){
            var model = await _region.AddRegionAsync(c);
            if(model)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}