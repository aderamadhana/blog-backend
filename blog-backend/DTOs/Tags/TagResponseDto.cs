namespace blog_backend.DTOs.Tags
{
    public class TagResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
