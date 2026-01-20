using blog_backend.Domain;
using blog_backend.Domain.Entities;
using blog_backend.DTOs.Categories;
using blog_backend.Repositories.Interfaces;
using blog_backend.Services.Interfaces;

namespace blog_backend.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<CategoryResponseDto>> GetAllAsync()
        {
            var items = await _repo.GetAllAsync();
            return items.Select(MapToResponse).ToList();
        }

        public async Task<CategoryResponseDto> GetByIdAsync(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) throw new Exception("Category not found");
            return MapToResponse(item);
        }

        public async Task<CategoryResponseDto> GetBySlugAsync(string slug)
        {
            var item = await _repo.GetBySlugAsync(slug);
            if (item == null) throw new Exception("Category not found");
            return MapToResponse(item);
        }

        public async Task<CategoryResponseDto> CreateAsync(CategoryRequestDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Slug = dto.Slug,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _repo.AddAsync(category);
            return MapToResponse(created);
        }

        public async Task<CategoryResponseDto> UpdateAsync(int id, CategoryRequestDto dto)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null) throw new Exception("Category not found");

            category.Name = dto.Name;
            category.Slug = dto.Slug;

            var updated = await _repo.UpdateAsync(category);
            return MapToResponse(updated);
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null) throw new Exception("Category not found");

            await _repo.DeleteAsync(category);
        }

        private static CategoryResponseDto MapToResponse(Category c) => new()
        {
            Id = c.Id,
            Name = c.Name,
            Slug = c.Slug,
            CreatedAt = c.CreatedAt
        };
    }
}
