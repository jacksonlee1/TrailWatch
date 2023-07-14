using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.TrailModels;
using Services.TrailServices;

namespace TrailWatchMVC.Controllers
{
    [Route("[controller]")]
    public class TrailController : Controller
    {
        private readonly ILogger<TrailController> _logger;

        private readonly ITrailService _trail;
        public TrailController(ILogger<TrailController> logger,ITrailService trail)
        {
            _logger = logger;
            _trail = trail;
        }
     
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
       [HttpGet("{id}")]
        public async Task<IActionResult> Trails(int id){
             var model = await _trail.GetTrailsByRegionIdAsync(id);
             return View(model);
        }

         public IActionResult Create(){
            return View();
         }

        [HttpPost]
        public async Task<IActionResult> Create(TrailCreate req)
        {
            var model = await _trail.AddTrailAsync(req);
            return View(model);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}