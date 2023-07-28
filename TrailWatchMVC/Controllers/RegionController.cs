using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.RegionModels;
using Services.RegionServices;
using TrailWatchMVC.Models;

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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost("Create"),ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Type")] RegionCreate c)
        {

            var model = await _region.AddRegionAsync(c);
            if (model)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id) => View(new RegionVM{ Region =  await _region.GetRegionByIdAsync(id)});
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
         [HttpGet("Delete/{id}")]
   
        public async Task<IActionResult> Delete(int id){
           return await _region.DeleteRegionByIdAsync(id)?RedirectToAction("Index","Profile"):NotFound();
        
           
        }
        [HttpGet("Update")]
        public IActionResult Update(){
            return View();
        }
         [HttpPost("Update")]
    [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([Bind("Title,Type,Content,TrailId,RegionId")]RegionUpdate c){
       
         
            return await _region.UpdateRegionAsync(c)?RedirectToAction("Detail","Region",new{id=c.Id}):NotFound();
        
           
        }
    }
}