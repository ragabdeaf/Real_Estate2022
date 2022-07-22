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
    public class regionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public regionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: regions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.region.Include(r => r.govarnate);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: regions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.region == null)
            {
                return NotFound();
            }

            var region = await _context.region
                .Include(r => r.govarnate)
                .FirstOrDefaultAsync(m => m.id == id);
            if (region == null)
            {
                return NotFound();
            }

            return View(region);
        }

        // GET: regions/Create
        public IActionResult Create()
        {
            ViewData["govarnateId"] = new SelectList(_context.govarnate, "id", "Name");
            return View();
        }

        // POST: regions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,govarnateId")] region region)
        {
            var g = await _context.govarnate.FindAsync(region.id);
            region.govarnate = g;
            //if (ModelState.IsValid)
            if (true)
            {
                _context.Add(region);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["govarnateId"] = new SelectList(_context.govarnate, "id", "Name", region.govarnateId);
            return View(region);
        }

        // GET: regions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.region == null)
            {
                return NotFound();
            }

            var region = await _context.region.FindAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            ViewData["govarnateId"] = new SelectList(_context.govarnate, "id", "Name", region.govarnateId);
            return View(region);
        }

        // POST: regions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,govarnateId")] region region)
        {
            if (id != region.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(region);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!regionExists(region.id))
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
            ViewData["govarnateId"] = new SelectList(_context.govarnate, "id", "Name", region.govarnateId);
            return View(region);
        }

        // GET: regions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.region == null)
            {
                return NotFound();
            }

            var region = await _context.region
                .Include(r => r.govarnate)
                .FirstOrDefaultAsync(m => m.id == id);
            if (region == null)
            {
                return NotFound();
            }

            return View(region);
        }

        // POST: regions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.region == null)
            {
                return Problem("Entity set 'ApplicationDbContext.region'  is null.");
            }
            var region = await _context.region.FindAsync(id);
            if (region != null)
            {
                _context.region.Remove(region);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool regionExists(int id)
        {
          return (_context.region?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
