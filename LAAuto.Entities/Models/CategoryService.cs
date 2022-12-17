using System.ComponentModel.DataAnnotations.Schema;

namespace LAAuto.Entities.Models
{
    /// <summary>
    /// Represents a category service.
    /// </summary>
    public class CategoryService
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the category ID.
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the service ID.
        /// </summary>
        public Guid ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        public Service Service { get; set; }
    }
}
