namespace LAAuto.Services.Categories
{
    public interface ICategoryService
    {
        Task<List<Category>> ListCategoriesAsync(Guid? serviceId = null);

        Task<Category> GetCategoryAsync(Guid id);

        Task CreateCategoryAsync(CreateCategoryRequest request);

        Task DeleteCategoryAsync(Guid id);
    }
}
