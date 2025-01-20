using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BloggingApp.Models;

namespace BloggingApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly BloggingContext _context;

        public BlogController(BloggingContext context)
        {
            _context = context;
        }

        // GET: Blog/Index
        // Main feed. Possibly show all or filter. 
        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            {
                return RedirectToAction("Login", "Account");
            }
            
            
            var posts = await _context.BlogPosts
                .Include(p => p.AppUser)  // So we can show author's name
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
            
            return View(posts);
        }

        // GET: Blog/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var blogPost = await _context.BlogPosts
                .Include(p => p.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (blogPost == null)
                return NotFound();

            return View(blogPost);
        }

        // GET: Blog/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPost blogPost)
        {
            // In a real app, you'd get the current user from session or Identity
            var userIdString = HttpContext.Session.GetString("UserId"); 
            if (string.IsNullOrEmpty(userIdString))
                return RedirectToAction("Login", "Account");

            int userId = int.Parse(userIdString);

            blogPost.AppUserId = userId;
            blogPost.CreatedAt = DateTime.UtcNow;
            _context.Add(blogPost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Blog/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost == null)
                return NotFound();

            // Optional: check if current user owns the post
            return View(blogPost);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlogPost editedPost)
        {
            if (!ModelState.IsValid)
            {
                return View(editedPost);
            }

            // 1. Fetch the existing post from the database
            var existingPost = await _context.BlogPosts
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existingPost == null)
            {
                return NotFound();
            }

            // 2. (Optional) Verify the current user is the owner of the post
            // If you’re storing user info in Session:
            // int currentUserId = int.Parse(HttpContext.Session.GetString("UserId"));
            // if (existingPost.AppUserId != currentUserId)
            // {
            //     return Forbid(); // or Unauthorized() - to prevent editing posts not owned
            // }

            try
            {
                // 3. Update only the fields you want to change
                existingPost.Title = editedPost.Title;
                existingPost.Content = editedPost.Content;
                existingPost.UpdatedAt = DateTime.UtcNow;

                // Preserve the existing foreign key
                // existingPost.AppUserId stays the same. Don’t overwrite it with editedPost.AppUserId

                // 4. Save changes
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.BlogPosts.Any(e => e.Id == editedPost.Id))
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


        // POST: Blog/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost != null)
            {
                _context.BlogPosts.Remove(blogPost);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
