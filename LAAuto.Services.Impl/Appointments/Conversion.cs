using LAAuto.Services.Appointments;
using ENTITIES = LAAuto.Entities.Models;
using SERVICES_IMPL_SERVICES = LAAuto.Services.Impl.Services;
using SERVICES_IMPL_USERS = LAAuto.Services.Impl.Users;

namespace LAAuto.Services.Impl.Appointments
{
    public static class Conversion
    {
        public static Appointment ConvertAppointment(ENTITIES.Appointment source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var target = new Appointment()
            {
                Id = source.Id,
                CategoryId = source.CategoryId,
                ServiceId = source.ServiceId,
                UserId = source.UserId,
                StartDate = source.StartDate,
                EndDate = source.EndDate,
                Description = source.Description,
                User = SERVICES_IMPL_USERS.Conversion.ConvertUser(source.User),
                Service = SERVICES_IMPL_SERVICES.Conversion.ConvertService(source.Service)
            };

            return target;
        }

        public static ENTITIES.Appointment ConvertAppointment(CreateAppointmentRequest source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var target = new ENTITIES.Appointment()
            {
                ServiceId = source.ServiceId,
                CategoryId = source.CategoryId,
                UserId = source.UserId,
                StartDate = source.StartDate,
                EndDate = source.EndDate,
                Description = source.Description,
            };

            return target;
        }
    }
}
