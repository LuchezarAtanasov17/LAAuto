using LAAuto.Services.Appointments;
using LAAuto.Services.Categories;
using LAAuto.Services.Users;

namespace LAAuto.Services.Services
{
    /// <summary>
    /// Represents a service.
    /// </summary>
    public class Service
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
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
        /// Gets or sets the average rating.
        /// </summary>
        public double AverageRating { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        public ICollection<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the appointments.
        /// </summary>
        public ICollection<Appointment> Appointments { get; set; }
    }
}
