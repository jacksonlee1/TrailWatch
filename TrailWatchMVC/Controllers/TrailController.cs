using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.TrailModels;
using Services.PostServices;
using Services.TrailServices;

namespace TrailWatchMVC.Controllers
{
    [Route("[controller]")]
    public class TrailController : Controller
    {
        private readonly ILogger<TrailController> _logger;

        private readonly ITrailService _trail;
        private readonly IPostService _posts;
        public TrailController(ILogger<TrailController> logger,ITrailService trail,IPostService posts)
        {
            _logger = logger;
            _trail = trail;
            _posts = posts;
        }
     
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

   [HttpGet("Posts/{id}")]
        public async Task<IActionResult> Posts(int? id)
        {
            var model = await _posts.GetPostsByTrailIdAsync(id??0);
            return View(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id){
            var model= await _trail.GetTrailByIdAsync(id);
            return (model is null)?NotFound():View(model);
        }

       [HttpGet("Region/Trails/{id}")]
        public async Task<IActionResult> Trails(int id){
             var model = await _trail.GetTrailsByRegionIdAsync(id);
             return View(model);
        }
        [HttpGet("Create/{id}")]
         public IActionResult Create(){
            return View();
         }

        [HttpPost("Create/{id}")]
        public async Task<IActionResult> Create(int id,TrailCreate req)
        {
            req.RegionId = id;
            var model = await _trail.AddTrailAsync(req);
            
            return model?RedirectToAction("Detail","Region",new{id = req.RegionId}):NotFound();
        }

        [HttpGet("Status/{id}")]
        public IActionResult Update(){
            
            return View();
        }

        [HttpPost("Status/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("Status")] TrailUpdate req)
        {   
            req.Id=id;
            var model = await _trail.UpdateTrailAsync(req);
            if(model)return RedirectToAction("Posts",new {id=id});
            return NotFound();
        }
        
        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}