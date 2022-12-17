using LAAuto.Web.Models.Categories;
using LAAuto.Web.Models.Services;

namespace LAAuto.Web.Models.Appointments
{
    /// <summary>
    /// Represents a request for updating an appointment.
    /// </summary>
    public class UpdateAppointmentRequest
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the service ID.
        /// </summary>
        public Guid ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the category ID.
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the start date hour.
        /// </summary>
        public int StartDateHour { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        public ServiceViewModel? Service { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public CategoryViewModel? Category { get; set; }
    }
}
