using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Real_Estate.Data;

namespace Real_Estate.Controllers
{
    public class Posts_oldController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Posts_oldController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var v = await _context.post.Include(x=>x.Images).ToListAsync();
              return _context.post != null ? 
                          View(v) :
                          Problem("Entity set 'ApplicationDbContext.post'  is null.");
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.post == null)
            {
                return NotFound();
            }

            var post = await _context.post.Include(x=>x.Images)
                .FirstOrDefaultAsync(m => m.id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["govarnateId"] = new SelectList(_context.govarnate, "id", "Name");

            ViewData["regionId"] = new SelectList(_context.region, "id", "name");
            return View();
        }

        private static string UploadFile(IFormFile ufile)
        {
            if (ufile != null && ufile.Length > 0)
            {
                var fileName = Path.GetFileName(ufile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ufile.CopyTo(fileStream);
                }
                return filePath;
            }
            return "";
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind]Post post)
        {

            if (ModelState.IsValid)
            {
                if (post.ImagesPath != null && post.ImagesPath.Count > 0)
                {
                    post.Images = new List<Image>();
                    foreach (IFormFile img in post.ImagesPath)
                    {
                        var up = UploadFile(img);
                        if (up != "")
                        {
                            post.Images.Add(new Image { path = up });
                        }
                    }
                }
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction("index","home");
            }

            ViewData["govarnateId"] = new SelectList(_context.govarnate, "id", "Name");

            ViewData["regionId"] = new SelectList(_context.region, "id", "name");
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.post == null)
            {
                return NotFound();
            }

            var post = await _context.post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,email,phone,Discription,SellerType,Category,PropertyType,PostFor,City,Region,Street,Area,Rooms,PathRoom,Price")] Post post)
        {
            if (id != post.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.post == null)
            {
                return NotFound();
            }

            var post = await _context.post
                .FirstOrDefaultAsync(m => m.id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.post == null)
            {
                return Problem("Entity set 'ApplicationDbContext.post'  is null.");
            }
            var post = await _context.post.FindAsync(id);
            if (post != null)
            {
                _context.post.Remove(post);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
          return (_context.post?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
