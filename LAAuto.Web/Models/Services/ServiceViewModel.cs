using LAAuto.Web.Models.Categories;
using LAAuto.Web.Models.Users;

namespace LAAuto.Web.Models.Services
{
    /// <summary>
    /// Represents a service view model.
    /// </summary>
    public class ServiceViewModel
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
        /// Gets or sets the update rating request.
        /// </summary>
        public UpdateRatingRequest UpdateRatingRequest { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public UserViewModel User { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        public ICollection<CategoryViewModel> Categories { get; set; }
    }
}
