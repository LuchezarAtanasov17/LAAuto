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
                User = WEB_USERS.Conversion.ConvertUser(source.User),
                Service = WEB_SERVICES.Conversion.ConvertService(source.Service),
            };

            return target;
        }

        public static SERVICES_APPOINTMENTS.CreateAppointmentRequest ConvertCreateAppointmentRequest(CreateAppointmentRequest source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            SERVICES_APPOINTMENTS.CreateAppointmentRequest target = new SERVICES_APPOINTMENTS.CreateAppointmentRequest()
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

        public static SERVICES_APPOINTMENTS.UpdateAppointmentRequest ConvertUpdateAppointmentRequest(UpdateAppointmentRequest source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            SERVICES_APPOINTMENTS.UpdateAppointmentRequest target = new SERVICES_APPOINTMENTS.UpdateAppointmentRequest()
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
