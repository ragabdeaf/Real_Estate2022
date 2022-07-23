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
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            List<Post> applicationDbContext = new List<Post>();

            applicationDbContext = await _context.post.Include(p => p.govarnate).Include(p => p.region).ToListAsync();


            return applicationDbContext != null ? View(applicationDbContext) : Problem("Entity set 'ApplicationDbContext.post'  is null.");
        }
        public async Task<IActionResult> removefav(int postId)
        {
            Post? post = new Post();
            post = await _context.post.SingleOrDefaultAsync(p => p.id == postId);

            var e = User.Identity?.Name;
            var postFav = await _context.postFavs.SingleOrDefaultAsync(p => p.postId == postId && p.email == e);

            if (postFav != null)
            {
                _context.postFavs.Remove(postFav);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Details), new { id = postId });
        }
        public async Task<IActionResult> cancelRequest(int postId)
        {
            Post? post = new Post();
            post = await _context.post.SingleOrDefaultAsync(p => p.id == postId);

            var e = User.Identity?.Name;
            var postReq = await _context.PostRequests.SingleOrDefaultAsync(p => p.postId == postId && p.custEmail == e);

            if (postReq != null)
            {
                _context.PostRequests.Remove(postReq);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Details), new { id = postId });
        }

        public async Task<IActionResult> addtofav(int postId)
        {
            Post? post = new Post();
            post = await _context.post.SingleOrDefaultAsync(p => p.id == postId);

            var postFav = new PostFav();
            postFav.Post = post;
            postFav.postId = postId;
            postFav.email = User.Identity?.Name;

            _context.Add(postFav);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = postId });
        }

        public async Task<IActionResult> request(int postId)
        {
            Post? post = new Post();
            post = await _context.post.SingleOrDefaultAsync(p => p.id == postId);

            var postReq = new postRequest();
            postReq.post = post;
            postReq.postId = postId;
            postReq.custEmail = User.Identity?.Name;

            _context.Add(postReq);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = postId });
        }

        public async Task<IActionResult> accept(int id)
        {
            postRequest? postReq = new postRequest();
            postReq = await _context.PostRequests.Include(p => p.post).SingleOrDefaultAsync(p => p.id == id);
            if (postReq != null)
            {
                var t = await _context.PostRequests.Where(p => p.postId == postReq.postId && p.isAccept == 1).ToListAsync();

                if (t != null && t.Count > 0)
                {
                    return Problem("لا يمكن قبول الطلب حيث تم قبول طلب اخر علي نفس الاعلان");
                }
                postReq.isAccept = 1;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(showRequest), new { postId = postReq.post?.id });
            }
            return Problem("ألبيان غير موجود");
        }

        public async Task<IActionResult> showRequest(int postId)
        {
            var reqs = await _context.PostRequests.Where(p => p.postId == postId).ToListAsync();
            return reqs != null ? View(reqs) : Problem("Entity set 'ApplicationDbContext.post'  is null.");

        }


        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.post == null)
            {
                return NotFound();
            }

            var post = await _context.post
                .Include(p => p.Images)
                .FirstOrDefaultAsync(m => m.id == id);
            if (post == null)
            {
                return NotFound();
            }
            var e = User.Identity?.Name;
            var postFav = await _context.postFavs.SingleOrDefaultAsync(p => p.postId == id && p.email == e);
            var postReq = await _context.PostRequests.FirstOrDefaultAsync(p => p.postId == id && p.custEmail == e);
            //var postAcc = await _context.PostRequests.FirstOrDefaultAsync(p => p.postId == id && p.custEmail == e);

            if (postFav != null)
            {
                post.isFav = 1;
            }
            else
            {
                post.isFav = 0;
            }

            if (postReq != null)
            {
                post.isAccepted = postReq.isAccept;
                post.isReq = 1;
            }
            else
            {
                post.isReq = 0;
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
        public async Task<IActionResult> Create(Post post)
        {
            if (true)
            {
                var r = await _context.region.FindAsync(post.regionId);
                var g = await _context.govarnate.FindAsync(post.govarnateId);

                post.govarnate = g;
                post.region = r;


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
                return RedirectToAction("index", "home");
            }
            ViewData["govarnateId"] = new SelectList(_context.govarnate, "id", "Name", post.govarnateId);
            ViewData["regionId"] = new SelectList(_context.region, "id", "name", post.regionId);
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
            ViewData["govarnateId"] = new SelectList(_context.govarnate, "id", "Name", post.govarnateId);
            ViewData["regionId"] = new SelectList(_context.region, "id", "name", post.regionId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,email,phone,Discription,SellerType,Category,PropertyType,PostFor,govarnateId,regionId,Street,Area,Rooms,PathRoom,Price")] Post post)
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
            ViewData["govarnateId"] = new SelectList(_context.govarnate, "id", "Name", post.govarnateId);
            ViewData["regionId"] = new SelectList(_context.region, "id", "name", post.regionId);
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
                .Include(p => p.govarnate)
                .Include(p => p.region)
                .FirstOrDefaultAsync(m => m.id == id);
            if (post == null)
            {
                return NotFound();
            }


            if (post != null)
            {
                _context.post.Remove(post);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("index", "home");

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
