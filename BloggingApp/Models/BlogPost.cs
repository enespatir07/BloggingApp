namespace BloggingApp.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Foreign Key
        public int AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}