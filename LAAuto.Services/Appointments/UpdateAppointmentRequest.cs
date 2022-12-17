namespace LAAuto.Services.Appointments
{
    /// <summary>
    /// Represents request for updating an appointment.
    /// </summary>
    public class UpdateAppointmentRequest
    {
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
        /// Gets or sets the description.
        /// </summary>
        public string? Description { get; set; }
    }
}
