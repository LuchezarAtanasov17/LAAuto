using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LAAuto.Entities.Models
{
    /// <summary>
    /// Represents an user.
    /// </summary>
    public class User : IdentityUser<Guid>
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [StringLength(30)]
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [StringLength(30)]
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets the appointments.
        /// </summary>
        public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();

        /// <summary>
        /// Gets or sets the services.
        /// </summary>
        public ICollection<Service> Services { get; set; } = new HashSet<Service>();

        /// <summary>
        /// Gets or sets the ratings.
        /// </summary>
        public ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();
    }
}
