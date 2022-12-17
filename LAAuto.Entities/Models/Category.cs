using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LAAuto.Entities.Models
{
    /// <summary>
    /// Represents a category.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the appointments.
        /// </summary>
        public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();

        /// <summary>
        /// Gets or sets the category services.
        /// </summary>
        public ICollection<CategoryService> CategoryServices { get; set; } = new HashSet<CategoryService>();
    }
}
