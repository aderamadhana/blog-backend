namespace blog_backend.DTOs.Categories
{
    public class UpdateCategoryDto
    {
        public string Name { get; set; } = null!;
        public string? Slug { get; set; }
    }
}
