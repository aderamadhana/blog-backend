namespace blog_backend.Domain.Entities
{
    public class Tag
    {
        public int Id { set; get; }
        public String Name { set; get; } = null!;
        public String Slug { set; get; } = null!;
        public DateTime CreatedAt { set; get; } = DateTime.UtcNow;
    }
}
