using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LAAuto.Entities.Models
{
    /// <summary>
    /// Represents a rating.
    /// </summary>
    public class Rating
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
        /// Gets or sets the service ID.
        /// </summary>
        [Required]
        public Guid ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [Required]
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        public Service Service { get; set; }
    }
}
