using LAAuto.Services.Appointments;
using LAAuto.Services.Services;

namespace LAAuto.Services.Categories
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public ICollection<Service> Services { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
