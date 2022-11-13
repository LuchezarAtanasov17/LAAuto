using LAAuto.Services.Categories;
using LAAuto.Services.Clients;
using LAAuto.Services.Services;

namespace LAAuto.Services.Appointments
{
    public class Appointment
    {
        public Guid Id { get; set; }

        public Guid ServiceId { get; set; }

        public Guid ClientId { get; set; }

        public Guid CategoryId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? Description { get; set; }

        public Service Service { get; set; }
        public Client Client { get; set; }
        public Category Category { get; set; }
    }
}
