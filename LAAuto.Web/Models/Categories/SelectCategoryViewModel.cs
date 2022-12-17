namespace LAAuto.Web.Models.Categories
{
    /// <summary>
    /// Represents a select category view model.
    /// </summary>
    public class SelectCategoryViewModel
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the is selected value.
        /// </summary>
        public bool IsSelected { get; set; }
    }
}
