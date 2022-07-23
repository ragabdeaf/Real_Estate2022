using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult Index(int govId = 0, int regId = 0)
        {
            var e = User.Identity?.Name;
            var postFav = _context.postFavs.Where(p => p.email == e).ToList().Count();

            var posts = new List<Post>();

            if (govId == 0 && regId == 0)
            {
                posts = _context.post.Include(w => w.Images).Where(i => i.Images != null && i.Images.Count() > 0).OrderByDescending(i => i.id).Take(40).ToList();
                //applicationDbContext = await _context.post.Include(p => p.govarnate).Include(p => p.region).ToListAsync();
            }
            else if (govId == 0 && regId != 0)
            {
                posts = _context.post.Include(w => w.Images).Where(i => i.Images != null && i.Images.Count() > 0 && i.regionId == regId).OrderByDescending(i => i.id).Take(40).ToList();
                //applicationDbContext = await _context.post.Where(p => p.regionId == regId).Include(p => p.govarnate).Include(p => p.region).ToListAsync();
            }
            else if (govId != 0 && regId == 0)
            {
                posts = _context.post.Include(w => w.Images).Where(i => i.Images != null && i.Images.Count() > 0 && i.govarnateId == govId).OrderByDescending(i => i.id).Take(40).ToList();
                //applicationDbContext = await _context.post.Where(p => p.govarnateId == govId).Include(p => p.govarnate).Include(p => p.region).ToListAsync();
            }
            else if (govId != 0 && regId != 0)
            {
                posts = _context.post.Include(w => w.Images).Where(i => i.Images != null && i.Images.Count() > 0 && i.govarnateId == govId && i.regionId == regId).OrderByDescending(i => i.id).Take(40).ToList();
                //applicationDbContext = await _context.post.Where(p => p.govarnateId == govId && p.regionId == regId).Include(p => p.govarnate).Include(p => p.region).ToListAsync();
            }



            ViewBag.govarnateId = new SelectList(_context.govarnate, "id", "Name");
            ViewBag.regionId = new SelectList(_context.region, "id", "name");

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