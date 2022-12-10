namespace LAAuto.Services.CategoryServices
{
    public interface ICategoryServiceService
    {
        Task<List<CategoryService>> ListCategoriesAsync(Guid? categoryId = null, Guid? serviceId = null);
    }
}
