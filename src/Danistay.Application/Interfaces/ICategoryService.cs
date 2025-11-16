using Danistay.Application.DTOs.Products;

namespace Danistay.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<IEnumerable<CategoryDto>> GetTopLevelCategoriesAsync();
        Task<IEnumerable<CategoryDto>> GetSubCategoriesAsync(int parentCategoryId);
        Task<CategoryDto?> GetCategoryByIdAsync(int id);
        Task<CategoryDto> CreateCategoryAsync(CategoryDto categoryDto, int currentUserId);
        Task<CategoryDto?> UpdateCategoryAsync(CategoryDto categoryDto, int currentUserId);
        Task<bool> DeleteCategoryAsync(int id, int currentUserId);
    }
}
