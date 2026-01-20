namespace blog_backend.DTOs.Categories
{
    public class CategoryResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
