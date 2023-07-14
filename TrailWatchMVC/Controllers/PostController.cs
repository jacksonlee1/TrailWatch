using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.PostModels;
using Services.PostServices;

namespace TrailWatchMVC.Controllers
{
     [Route("[controller]")]
    public class Post : Controller
    {
        private readonly ILogger<Region> _logger;
        private readonly IPostService _posts;

        public Post(ILogger<Region> logger,IPostService posts)
        {
            _logger = logger;
            _posts = posts;
        }
        [HttpGet("{id}")]
        public IActionResult Index(int id)
        {
            var model = _posts.GetPostsByTrailIdAsync(id);
            return View(model);
        }
      
         public IActionResult Create(){
            return View();
         }

          [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Type")]PostCreate c){
            var model = await _posts.AddPostAsync(c);
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