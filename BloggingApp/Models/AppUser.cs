namespace BloggingApp.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;  // In production, store a hashed password, never plain text.

        // Additional user info
        public string? FullName { get; set; }
        public DateTime DateJoined { get; set; } = DateTime.UtcNow;

        // Navigation property: a user can have many blog posts
        public ICollection<BlogPost>? BlogPosts { get; set; }
    }
}