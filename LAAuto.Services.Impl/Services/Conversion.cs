using LAAuto.Services.Services;
using ENTITIES = LAAuto.Entities.Models;
using SERVICES_IMPL_CATEGORY = LAAuto.Services.Impl.Categories;
using SERVICES_IMPL_APPOINTMENT = LAAuto.Services.Impl.Appointments;
using SERVICES_IMPL_USERS = LAAuto.Services.Impl.Users;
using LAAuto.Services.Users;

namespace LAAuto.Services.Impl.Services
{
    public static class Conversion
    {
        public static Service ConvertService(ENTITIES.Service source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var target = new Service()
            {
                Id = source.Id,
                UserId = source.UserId,
                Name = source.Name,
                OpenTime = source.OpenTime,
                CloseTime = source.CloseTime,
                Location = source.Location,
                Description = source.Description,
                User = SERVICES_IMPL_USERS.Conversion.ConvertUser(source.User),
                Categories = source.Categories
                    .Select(SERVICES_IMPL_CATEGORY.Conversion.ConvertCategory)
                    .ToHashSet(),
                //Appointments = source.Appointments
                //    .Select(SERVICES_IMPL_APPOINTMENT.Conversion.ConvertAppointment)
                //    .ToHashSet(),

                //TODO: AverageRating 
            };

            return target;
        }

        public static ENTITIES.Service ConvertService(CreateServiceRequest source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var target = new ENTITIES.Service()
            {
                UserId = source.UserId,
                Name = source.Name,
                OpenTime = source.OpenTime,
                CloseTime = source.CloseTime,
                Location = source.Location,
                Description = source.Description,
            };

            return target;
        }
    }
}
