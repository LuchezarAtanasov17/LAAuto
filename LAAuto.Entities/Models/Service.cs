using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LAAuto.Entities.Models
{
    /// <summary>
    /// Represents a service.
    /// </summary>
    public class Service
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [StringLength(50)]
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
        [StringLength(80)]
        public string Location { get; set; } = null!;

        /// <summary>
        /// Gets or sets the open time.
        /// </summary>
        [Required]
        public TimeOnly OpenTime { get; set; }

        /// <summary>
        /// Gets or sets the close time.
        /// </summary>
        [Required]
        public TimeOnly CloseTime { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        /// <summary>
        /// Gets or sets the appointments.
        /// </summary>
        public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();

        /// <summary>
        /// Gets or sets the ratings.
        /// </summary>
        public ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();

        /// <summary>
        /// Gets or sets the category services.
        /// </summary>
        public ICollection<CategoryService> CategoryServices { get; set; } = new HashSet<CategoryService>();
    }
}
