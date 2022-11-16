using LAAuto.Services.Appointments;
using ENTITIES = LAAuto.Entities.Models;
using SERVICES_IMPL_CATEGORIES = LAAuto.Services.Impl.Categories;
using SERVICES_IMPL_CLIENTS = LAAuto.Services.Impl.Clients;
using SERVICES_IMPL_SERVICES = LAAuto.Services.Impl.Services;

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
                ClientId = source.ClientId,
                StartDate = source.StartDate,
                EndDate = source.EndDate,
                Description = source.Description,
                Category = SERVICES_IMPL_CATEGORIES.Conversion.ConvertCategory(source.Category),
                Client = SERVICES_IMPL_CLIENTS.Conversion.ConvertClients(source.Client),
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
                ClientId = source.ClientId,
                StartDate = source.StartDate,
                EndDate = source.EndDate,
                Description = source.Description,
            };

            return target;
        }
    }
}
