using Microsoft.AspNetCore.Mvc;
using BloggingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly BloggingContext _context;

        public AccountController(BloggingContext context)
        {
            _context = context;
        }

        
        
        
        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AppUser user)
        {
            if (!ModelState.IsValid)
                return View(user);

            // Hash the password (if using a hashing mechanism)
            user.PasswordHash = user.PasswordHash; // Replace with real hashing in production
            user.DateJoined = DateTime.UtcNow;

            _context.AppUsers.Add(user);
            await _context.SaveChangesAsync();

            // Optionally, log the user in immediately
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("Username", user.Username);

            return RedirectToAction("Index", "Blog");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _context.AppUsers
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }

            // Check password (again: do real hashing in production)
            if (user.PasswordHash != password)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }

            // If valid, sign in with cookie or session...
            // For example, using session:
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("Username", user.Username);

            return RedirectToAction("Index", "Blog");
        }

        public IActionResult Logout()
        {
            // Clear the session
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
