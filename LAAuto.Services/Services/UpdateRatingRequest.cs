namespace LAAuto.Services.Services
{
    /// <summary>
    /// Represents a request for updating a rating.
    /// </summary>
    public class UpdateRatingRequest
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
        /// Gets or sets the value.
        /// </summary>
        public int Value { get; set; }
    }
}