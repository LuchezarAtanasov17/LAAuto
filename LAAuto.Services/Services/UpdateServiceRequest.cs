using LAAuto.Services.Categories;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LAAuto.Services.Services
{
    /// <summary>
    /// Represents a request for updating a service.
    /// </summary>
    public class UpdateServiceRequest
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public Guid Id{ get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [StringLength(300)]
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
        public TimeOnly OpenTime { get; set; }

        /// <summary>
        /// Gets or sets the close time.
        /// </summary>
        public TimeOnly CloseTime { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        public List<Category> Categories { get; set; }
    }
}
