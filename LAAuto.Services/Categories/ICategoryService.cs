namespace LAAuto.Services.Categories
{
    /// <summary>
    /// Provides access to categories.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Lists the categories.
        /// </summary>
        /// <param name="serviceId">the service ID</param>
        /// <returns>collection of categories</returns>
        Task<List<Category>> ListCategoriesAsync(Guid? serviceId = null);

        /// <summary>
        /// Gets a category with specified ID.
        /// </summary>
        /// <param name="id">the category ID</param>
        /// <returns>a category</returns>
        Task<Category> GetCategoryAsync(Guid id);

        /// <summary>
        /// Creates a category.
        /// </summary>
        /// <param name="request"></param>
        Task CreateCategoryAsync(CreateCategoryRequest request);

        /// <summary>
        /// Deletes a category with specified ID.
        /// </summary>
        /// <param name="id"></param>
        Task DeleteCategoryAsync(Guid id);
    }
}
