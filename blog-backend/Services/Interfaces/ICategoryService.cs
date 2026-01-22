using blog_backend.Domain.Entities;
using blog_backend.DTOs.Categories;

namespace blog_backend.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryResponseDto>> GetAllAsync();
        Task<CategoryResponseDto> GetByIdAsync(int id);
        Task<CategoryResponseDto> GetBySlugAsync(string slug);

        Task<CategoryResponseDto> CreateAsync(CategoryRequestDto dto);
        Task<CategoryResponseDto> UpdateAsync(int id, CategoryRequestDto dto);

        Task DeleteAsync(int id);
    }
}
