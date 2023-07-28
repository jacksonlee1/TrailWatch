using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.CommentModels;
using Services.CommentServices;

namespace TrailWatchMVC.Controllers
{
    [Route("[controller]")]
    public class CommentController : Controller
    {
        private readonly ILogger<CommentController> _logger;
        private readonly ICommentService _comment;

        public CommentController(ILogger<CommentController> logger,ICommentService comment)
        {
            _comment = comment;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> New(CommentCreate model){
            var res = await _comment.AddCommentAsync(model);
            return res? RedirectToAction("Index","Post",new{id=model.PostId}):NotFound();
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(CommentUpdate model){
            var res = await _comment.UpdateCommentAsync(model);
            return res? RedirectToAction("Index","Post",new{id=model.Id}):NotFound();
        }

         [HttpGet("Delete")]
        public async Task<IActionResult> Delete(int id){
            var res = await _comment.DeleteCommentByIdAsync(id);
            return res? Ok():NotFound();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}