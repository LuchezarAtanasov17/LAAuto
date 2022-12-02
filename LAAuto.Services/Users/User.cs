using LAAuto.Services.Appointments;
using LAAuto.Services.Ratings;
using LAAuto.Services.Services;

namespace LAAuto.Services.Users
{
    public class User
    {
        public Guid Id { get; set; }

        public string UserName { get; set; } = null!;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }
    }
}
