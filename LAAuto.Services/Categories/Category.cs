namespace LAAuto.Services.Categories
{
    /// <summary>
    /// Represents a category.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; } = null!;
    }
}
