using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Real_Estate.Data;

namespace Real_Estate.Controllers
{
    [Authorize]
    public class PostTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PostTypes
        public async Task<IActionResult> Index()
        {
              return _context.PostType != null ? 
                          View(await _context.PostType.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.PostType'  is null.");
        }

        // GET: PostTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PostType == null)
            {
                return NotFound();
            }

            var postType = await _context.PostType
                .FirstOrDefaultAsync(m => m.id == id);
            if (postType == null)
            {
                return NotFound();
            }

            return View(postType);
        }

        // GET: PostTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PostTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name")] PostType postType)
        {

            if (ModelState.IsValid)
            {
                if (postType.name.Length < 2)
                {
                    ViewBag.error = "خانة الاسم لابد ان حرفين علي الاقل";
                    return View(postType);
                }
                _context.Add(postType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postType);
        }

        // GET: PostTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PostType == null)
            {
                return NotFound();
            }

            var postType = await _context.PostType.FindAsync(id);
            if (postType == null)
            {
                return NotFound();
            }
            return View(postType);
        }

        // POST: PostTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name")] PostType postType)
        {
            if (id != postType.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostTypeExists(postType.id))
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
            return View(postType);
        }

        // GET: PostTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PostType == null)
            {
                return NotFound();
            }

            var postType = await _context.PostType
                .FirstOrDefaultAsync(m => m.id == id);
            if (postType == null)
            {
                return NotFound();
            }

            return View(postType);
        }

        // POST: PostTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PostType == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PostType'  is null.");
            }
            var postType = await _context.PostType.FindAsync(id);
            if (postType != null)
            {
                _context.PostType.Remove(postType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostTypeExists(int id)
        {
          return (_context.PostType?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
