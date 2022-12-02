using SERVICES_SERVICES = LAAuto.Services.Services;
using WEB_CATEGORY = LAAuto.Web.Models.Categories;
using WEB_APPOINTMENT = LAAuto.Web.Models.Appointments;
using WEB_USERS = LAAuto.Web.Models.Users;


namespace LAAuto.Web.Models.Services
{
    public class Conversion
    {
        public static ServiceViewModel ConvertService(SERVICES_SERVICES.Service source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var target = new ServiceViewModel()
            {
                Id = source.Id,
                UserId = source.UserId,
                Name = source.Name,
                Description = source.Description,
                OpenTime = source.OpenTime,
                CloseTime = source.CloseTime,
                Location = source.Location,
                User = WEB_USERS.Conversion.ConvertUser(source.User),
                Categories = source.Categories
                    .Select(WEB_CATEGORY.Conversion.ConvertCategory)
                    .ToHashSet(),
                //Appointments = source.Appointments
                //    .Select(WEB_APPOINTMENT.Conversion.ConvertAppointment)
                //    .ToHashSet(),
                
                //AverageRating = source.AverageRating,
            };

            return target;
        }

        public static SERVICES_SERVICES.CreateServiceRequest ConvertService(CreateServiceRequest source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var target = new SERVICES_SERVICES.CreateServiceRequest
            {
                Name = source.Name,
                Description = source.Description,
                OpenTime = new TimeOnly(),
                CloseTime = new TimeOnly(),
                Location = source.Location,
            };

            return target;
        }

        public static SERVICES_SERVICES.UpdateServiceRequest ConvertService(UpdateServiceRequest source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var target = new SERVICES_SERVICES.UpdateServiceRequest
            {
                UserId = source.UserId,
                Name = source.Name,
                Description = source.Description,
                OpenTime = source.OpenTime,
                CloseTime = source.CloseTime,
                Location = source.Location,
            };

            return target;
        }
    }
}
