using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrailWatchMVC.Controllers
{
     [Route("[controller]")]
    public class Post : Controller
    {
        private readonly ILogger<Region> _logger;
        private readonly IPostService _posts;

        public Region(ILogger<Region> logger,IPostService posts)
        {
            _logger = logger;
            _posts = posts;
        }

        public IActionResult Index()
        {
            var model = _posts.GetAllPostsAsync();
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}