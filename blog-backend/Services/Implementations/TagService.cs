using blog_backend.Domain.Entities;
using blog_backend.DTOs.Categories;
using blog_backend.DTOs.Tags;
using blog_backend.Repositories.Interfaces.Tags;
using blog_backend.Services.Interfaces;

namespace blog_backend.Services.Implementations
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        public async Task DeleteAsync(int id)
        {
            var tag = await _tagRepository.GetByIdAsync(id);
            if(tag == null) throw new Exception("Tag not found");

            await _tagRepository.DeleteAsync(tag);

        }

        public async Task<List<TagResponseDto>> GetAllAsync()
        {
            var items = await _tagRepository.GetAllAsync();
            return items.Select(MapToResponse).ToList();
        }

        public async Task<TagResponseDto> GetByIdAsync(int id)
        {
            var items = await _tagRepository.GetByIdAsync(id);
            if(items == null) throw new Exception("Tag not found");
            return MapToResponse(items);
        }

        public async Task<TagResponseDto?> getBySlugAsync(string slug)
        {
            var items = await _tagRepository.GetBySlugAsync(slug);
            if(items == null) throw new Exception("Tag not found");
            return MapToResponse(items);
        }

        private static TagResponseDto MapToResponse(Tag tag) => new()
        {
            Id = tag.Id,
            Name = tag.Name,
            Slug = tag.Slug,
            CreatedAt = tag.CreatedAt
        };

        public async Task<TagResponseDto?> CreateAsync(TagRequestDto dto)
        {
            var tag = new Tag
            {
                Name = dto.Name,
                Slug = dto.Slug,
                CreatedAt = DateTime.UtcNow
            };
            var created = await _tagRepository.AddAsync(tag);
            return MapToResponse(created);
        }

        public async Task<TagResponseDto?> UpdateAsync(int id, TagRequestDto dto)
        {
            var tag = await _tagRepository.GetByIdAsync(id);
            if (tag == null) throw new Exception("Tag not found");

            tag.Name = dto.Name;
            tag.Slug = dto.Slug;
            var updated = await _tagRepository.UpdateAsync(tag);
            return MapToResponse(updated);
        }
    }
}
