using blog_backend.Data;
using blog_backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace blog_backend.Repositories.Interfaces.Tags
{
    public class TagRepository : ITagRepository
    {
        private readonly AppDbContext _db;
        public TagRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            _db.Tags.Add(tag);
            await _db.SaveChangesAsync();
            return tag;
        }

        public async Task<List<Tag>> GetAllAsync()
        {
            return await _db.Tags.OrderByDescending(x => x.CreatedAt).ToListAsync();
        }

        public async Task<Tag?> GetByIdAsync(int id)
        {
            return await _db.Tags.FindAsync(id).AsTask();
        }

        public async Task<Tag?> GetBySlugAsync(string slug)
        {
            return await _db.Tags.FirstOrDefaultAsync(x => x.Slug == slug);
        }

        public async Task<bool> SlugExistsAsync(string slug, int? excludeId = null)
        {
            var query = _db.Tags.AsQueryable();
            if (excludeId.HasValue)
            {
               query = query.Where(x => x.Id != excludeId.Value);
            }

            return await query.AnyAsync(x => x.Slug == slug);
        }

        public async Task<Tag> UpdateAsync(Tag tag)
        {
           _db.Tags.Update(tag);
            await _db.SaveChangesAsync();
            return tag;
        }

        public async Task DeleteAsync(Tag tag)
        {
            _db.Tags.Remove(tag);
            await _db.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            return DeleteAsync(new Tag { Id = id });
        }
    }
}
