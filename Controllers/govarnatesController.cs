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
    public class govarnatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public govarnatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: govarnates
        public async Task<IActionResult> Index()
        {
              return _context.govarnate != null ? 
                          View(await _context.govarnate.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.govarnate'  is null.");
        }

        // GET: govarnates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.govarnate == null)
            {
                return NotFound();
            }

            var govarnate = await _context.govarnate
                .FirstOrDefaultAsync(m => m.id == id);
            if (govarnate == null)
            {
                return NotFound();
            }

            return View(govarnate);
        }

        // GET: govarnates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: govarnates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name")] govarnate govarnate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(govarnate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(govarnate);
        }

        // GET: govarnates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.govarnate == null)
            {
                return NotFound();
            }

            var govarnate = await _context.govarnate.FindAsync(id);
            if (govarnate == null)
            {
                return NotFound();
            }
            return View(govarnate);
        }

        // POST: govarnates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name")] govarnate govarnate)
        {
            if (id != govarnate.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(govarnate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!govarnateExists(govarnate.id))
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
            return View(govarnate);
        }

        // GET: govarnates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.govarnate == null)
            {
                return NotFound();
            }

            var govarnate = await _context.govarnate
                .FirstOrDefaultAsync(m => m.id == id);
            if (govarnate == null)
            {
                return NotFound();
            }

            return View(govarnate);
        }

        // POST: govarnates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.govarnate == null)
            {
                return Problem("Entity set 'ApplicationDbContext.govarnate'  is null.");
            }
            var govarnate = await _context.govarnate.FindAsync(id);
            if (govarnate != null)
            {
                _context.govarnate.Remove(govarnate);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool govarnateExists(int id)
        {
          return (_context.govarnate?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
