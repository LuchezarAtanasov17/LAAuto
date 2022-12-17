using LAAuto.Web.Models.Categories;
using System.ComponentModel.DataAnnotations;

namespace LAAuto.Web.Models.Services
{
    /// <summary>
    /// Represents a request for updating a service.
    /// </summary>
    public class UpdateServiceRequest
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the category IDs.
        /// </summary>
        public List<Guid>? CategoryIds { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        [Required]
        [StringLength(80, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        public string Location { get; set; } = null!;

        /// <summary>
        /// Gets or sets the open time.
        /// </summary>
        [Required]
        public string OpenTime { get; set; }

        /// <summary>
        /// Gets or sets the close time.
        /// </summary>
        [Required]
        public string CloseTime { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        public List<SelectCategoryViewModel> Categories { get; set; }
    }
}
