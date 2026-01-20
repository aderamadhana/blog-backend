using blog_backend.Domain;
using blog_backend.Domain.Entities;

namespace blog_backend.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<Category?> GetBySlugAsync(string slug);

        Task<bool> SlugExistsAsync(string slug, int? excludeId = null);

        Task<Category> AddAsync(Category category);
        Task<Category> UpdateAsync(Category category);
        Task DeleteAsync(Category category);
    }
}
