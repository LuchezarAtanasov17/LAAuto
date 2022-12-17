using System.ComponentModel.DataAnnotations;

namespace LAAuto.Web.Models.Categories
{
    /// <summary>
    /// Represents a request for creating a category.
    /// </summary>
    public class CreateCategoryRequest
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string Name { get; set; }
    }
}
