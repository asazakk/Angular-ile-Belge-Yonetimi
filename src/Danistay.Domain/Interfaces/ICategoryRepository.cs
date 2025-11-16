using Danistay.Domain.Entities;

namespace Danistay.Domain.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetTopLevelCategoriesAsync();
        Task<IEnumerable<Category>> GetSubCategoriesAsync(int parentCategoryId);
        Task<Category?> GetWithProductsAsync(int categoryId);
        Task<IEnumerable<Category>> GetActiveCategoriesAsync();
    }
}
