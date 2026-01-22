using blog_backend.Domain.Entities;
using blog_backend.DTOs.Tags;

namespace blog_backend.Services.Interfaces
{
    public interface ITagService
    {
        Task<List<TagResponseDto>> GetAllAsync();
        Task<TagResponseDto> GetByIdAsync(int id);
        Task<TagResponseDto?> getBySlugAsync(string slug);
        Task<TagResponseDto?> CreateAsync(TagRequestDto dto);
        Task<TagResponseDto?> UpdateAsync(int id, TagRequestDto dto);
        Task DeleteAsync(int id);
    }
}
