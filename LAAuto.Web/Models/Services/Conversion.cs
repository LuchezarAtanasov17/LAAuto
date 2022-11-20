using SERVICES_SERVICES = LAAuto.Services.Services;
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
                AverageRating = source.AverageRating,
                User = WEB_USERS.Conversion.ConvertUser(source.User),
            };

            return target;
        }
    }
}
