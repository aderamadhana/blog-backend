using blog_backend.Data;
using blog_backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace blog_backend.Repositories.Interfaces
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _db;
        public CategoryRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<List<Category>> GetAllAsync()
        {
            return await _db.Categories.OrderByDescending(x => x.CreatedAt).ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _db.Categories.FindAsync(id);
        }

        public async Task<Category?> GetBySlugAsync(string slug)
        {
            return await _db.Categories.FirstOrDefaultAsync(x => x.Slug == slug);
        }

        public async Task<bool> SlugExistsAsync(string slug, int? excludeId = null)
        {
            var query = _db.Categories.AsQueryable();
            if (excludeId.HasValue)
            {
                query = query.Where(x => x.Id != excludeId.Value);
            }
            return await query.AnyAsync(x => x.Slug == slug);
        }

        public async Task<Category> AddAsync(Category category)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            _db.Categories.Update(category);
            await _db.SaveChangesAsync();
            return category;
        }

        public async Task DeleteAsync(Category category)
        {
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
        }
    }
}
