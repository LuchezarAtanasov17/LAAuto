

using LAAuto.Services.Users;
using LAAuto.Web.Models.Users;

namespace LAAuto.Web.Models.Services
{
    public class ServiceViewModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string Location { get; set; } = null!;

        public TimeOnly OpenTime { get; set; }

        public TimeOnly CloseTime { get; set; }

        public double AverageRating { get; set; }

        public UserViewModel User { get; set; }
    }
}
