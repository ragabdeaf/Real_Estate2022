using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Real_Estate.Data;
using Real_Estate.Models;
using System.Diagnostics;

namespace Real_Estate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> fav()
        {
            var e = User.Identity?.Name;
            var postFav = await _context.postFavs.Include(p=>p.Post).Include(p=>p.Post.Images).Where(p => p.email == e).ToListAsync();
            return postFav != null ? View(postFav) : Problem("Entity set 'ApplicationDbContext.post'  is null.");
        }

        public IActionResult Index()
        {
            var e = User.Identity?.Name;
            var postFav = _context.postFavs.Where(p => p.email == e).ToList().Count();

            var posts = _context.post.Include(w => w.Images).Where(i => i.Images != null && i.Images.Count() > 0).OrderByDescending(i => i.id).Take(40).ToList();
            return View(posts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}