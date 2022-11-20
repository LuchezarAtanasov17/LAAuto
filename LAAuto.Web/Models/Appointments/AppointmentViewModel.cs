
using LAAuto.Services.Categories;
using LAAuto.Services.Services;
using LAAuto.Services.Users;
using LAAuto.Web.Models.Categories;
using LAAuto.Web.Models.Services;
using LAAuto.Web.Models.Users;

namespace LAAuto.Web.Models.Appointments
{
    public class AppointmentViewModel
    {
        public Guid Id { get; set; }

        public Guid ServiceId { get; set; }

        public Guid UserId { get; set; }

        public Guid CategoryId { get; set; }

        //TODO: Validation with error messages
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? Description { get; set; }

        public ServiceViewModel Service { get; set; }
        public UserViewModel User { get; set; }
        public CategoryViewModel Category { get; set; }
    }
}
