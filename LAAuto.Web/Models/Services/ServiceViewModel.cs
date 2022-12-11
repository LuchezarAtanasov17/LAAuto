using LAAuto.Web.Models.Appointments;
using LAAuto.Web.Models.Categories;
using LAAuto.Web.Models.Users;
using System.Drawing;

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

        public IFormFile? Image { get; set; }



        public UserViewModel User { get; set; }

        public ICollection<CategoryViewModel> Categories { get; set; }

        public ICollection<AppointmentViewModel> Appointments { get; set; }
    }
}
