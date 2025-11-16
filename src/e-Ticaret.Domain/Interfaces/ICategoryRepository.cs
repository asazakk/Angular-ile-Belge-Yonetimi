using e_Ticaret.Domain.Entities;

namespace e_Ticaret.Domain.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetTopLevelCategoriesAsync();
        Task<IEnumerable<Category>> GetSubCategoriesAsync(int parentCategoryId);
        Task<Category?> GetWithProductsAsync(int categoryId);
        Task<IEnumerable<Category>> GetActiveCategoriesAsync();
    }
}
