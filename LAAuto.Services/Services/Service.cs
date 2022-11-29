using LAAuto.Services.Appointments;
using LAAuto.Services.Categories;
using LAAuto.Services.Users;

namespace LAAuto.Services.Services
{
    public class Service
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string Location { get; set; } = null!;

        public TimeOnly OpenTime { get; set; }

        public TimeOnly CloseTime { get; set; }

        public double AverageRating { get; set; }

        public User User { get; set; }

        public ICollection<Category> Categories { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
