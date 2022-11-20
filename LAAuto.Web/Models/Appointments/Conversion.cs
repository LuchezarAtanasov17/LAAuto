using SERVICES_APPOINTMENTS = LAAuto.Services.Appointments;
using WEB_CATEGORIES = LAAuto.Web.Models.Categories;
using WEB_SERVICES = LAAuto.Web.Models.Services;
using WEB_USERS = LAAuto.Web.Models.Users;

namespace LAAuto.Web.Models.Appointments
{
    public class Conversion
    {
        public static AppointmentViewModel ConvertAppointment(SERVICES_APPOINTMENTS.Appointment source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var target = new AppointmentViewModel()
            {
                Id = source.Id,
                CategoryId = source.CategoryId,
                UserId = source.UserId,
                ServiceId = source.ServiceId,
                Description = source.Description,
                StartDate = source.StartDate,
                EndDate = source.EndDate,
                Category = WEB_CATEGORIES.Conversion.ConvertCategory(source.Category),
                Service = WEB_SERVICES.Conversion.ConvertService(source.Service),
                User = WEB_USERS.Conversion.ConvertUser(source.User),
            };

            return target;
        }

        public static CreateAppointmentRequest ConvertCreateAppointmentRequest(SERVICES_APPOINTMENTS.CreateAppointmentRequest source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            CreateAppointmentRequest target = new CreateAppointmentRequest()
            {
                CategoryId = source.CategoryId,
                UserId = source.UserId,
                ServiceId = source.ServiceId,
                Description = source.Description,
                StartDate = source.StartDate,
                EndDate = source.EndDate
            };

            return target;
        }

        public static UpdateAppointmentRequest ConvertUpdateAppointmentRequest(SERVICES_APPOINTMENTS.UpdateAppointmentRequest source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            UpdateAppointmentRequest target = new UpdateAppointmentRequest()
            {
                CategoryId = source.CategoryId,
                UserId = source.UserId,
                ServiceId = source.ServiceId,
                Description = source.Description,
                StartDate = source.StartDate,
                EndDate = source.EndDate
            };

            return target;
        }
    }
}
