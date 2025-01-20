using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BloggingApp.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BloggingApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly BloggingContext _context;

        public ProfileController(BloggingContext context)
        {
            _context = context;
        }

        // GET: /Profile/Index
        public async Task<IActionResult> Index(string success)
        {
            // Check if UserId is stored in session
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
                return RedirectToAction("Login", "Account");

            if (!int.TryParse(userIdString, out int userId))
                return RedirectToAction("Login", "Account");

            // Optionally display success messages
            if (!string.IsNullOrEmpty(success))
            {
                TempData["SuccessMessage"] = success;
            }

            // Retrieve the user and related blog posts
            var user = await _context.AppUsers
                .Include(u => u.BlogPosts)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // GET: /Profile/Edit
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
                return RedirectToAction("Login", "Account");

            if (!int.TryParse(userIdString, out int userId))
                return RedirectToAction("Login", "Account");

            var user = await _context.AppUsers.FindAsync(userId);
            if (user == null)
                return NotFound();

            // Map AppUser to the EditProfileViewModel
            var viewModel = new EditProfileViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email
            };

            return View(viewModel);
        }

        // POST: /Profile/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProfileViewModel model)
        {
            // Validate the view model
            if (!ModelState.IsValid)
            {
                // Log model state errors (optional)
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"ModelState Error: {error.ErrorMessage}");
                }

                // Return the view with the current model data and validation errors
                return View(model);
            }

            // Ensure session still has a valid user ID
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
                return RedirectToAction("Login", "Account");

            if (!int.TryParse(userIdString, out int userId))
                return RedirectToAction("Login", "Account");

            // Fetch the user from the database
            var user = await _context.AppUsers.FindAsync(userId);
            if (user == null)
                return NotFound();

            // Update the user's editable fields
            user.FullName = model.FullName;
            user.Email = model.Email;

            try
            {
                // Mark the entity as modified and save changes
                _context.AppUsers.Update(user);
                await _context.SaveChangesAsync();

                // Log success (optional)
                Console.WriteLine($"User {userId} updated successfully.");
            }
            catch (DbUpdateException ex)
            {
                // Log the exception (optional)
                Console.WriteLine($"Error updating user: {ex.Message}");

                // Add error message to the ModelState to display in the view
                ModelState.AddModelError("", "An error occurred while updating your profile. Please try again.");
                return View(model);
            }

            // Redirect to the profile index with a success message
            return RedirectToAction(nameof(Index), new { success = "Profile updated successfully." });
        }
    }

    /// <summary>
    /// ViewModel for editing user profile details.
    /// </summary>
    public class EditProfileViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }
    }
}
