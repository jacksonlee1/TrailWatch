using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.PostModels;
using Services.PostServices;
using Services.TrailServices;

namespace TrailWatchMVC.Controllers
{
     [Route("[controller]")]
    public class Post : Controller
    {
        private readonly ILogger<Region> _logger;
        private readonly IPostService _posts;
        private readonly ITrailService _trail;

        public Post(ILogger<Region> logger,IPostService posts, ITrailService trail)
        {
            _logger = logger;
            _posts = posts;
            _trail = trail;
        }
     

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Type,Content,TrailId,RegionId,Image")]PostCreate c){
           Console.WriteLine("trail ID: " + c.TrailId);
           if(c.TrailId != null){
            return await _posts.AddPostToTrailAsync(c)?RedirectToAction("Detail","Trail",new{id=c.TrailId}):NotFound();
           }
           if(c.RegionId != null){
            return await _posts.AddPostToTrailAsync(c)?RedirectToAction("Detail","Region",new{id=c.RegionId}):NotFound();
           }
           return NotFound(c.TrailId);
           
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var model =  await _posts.GetPostByIdAsync(id);
            if(model is null) return NotFound();
            return View(model);
        }
         [HttpGet("Delete/{id}")]
   
        public async Task<IActionResult> Delete(int id){
           return await _posts.DeletePostByIdAsync(id)?RedirectToAction("Index","Profile"):NotFound();
        
           
        }
        [HttpGet("Update")]
        public IActionResult Update(){
            return View();
        }
         [HttpPost("Update")]
    [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([Bind("Title,Type,Content,TrailId,RegionId")]PostUpdate c){
       
           if(c.RegionId is null){
            return await _posts.UpdatePostAsync(c)?RedirectToAction("Detail","Trail",new{id=c.TrailId}):NotFound();
           }
           if(c.TrailId is null){
            return await _posts.UpdatePostAsync(c)?RedirectToAction("Detail","Region",new{id=c.RegionId}):NotFound();
           }
           return NotFound();
           
        }
        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}