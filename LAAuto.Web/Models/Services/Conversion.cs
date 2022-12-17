using SERVICES_SERVICES = LAAuto.Services.Services;
using WEB_CATEGORY = LAAuto.Web.Models.Categories;
using WEB_USERS = LAAuto.Web.Models.Users;

namespace LAAuto.Web.Models.Services
{
    /// <summary>
    /// Represents a conversion class for converting web models.
    /// </summary>
    public class Conversion
    {
        /// <summary>
        /// Converts a service service to web service.
        /// </summary>
        /// <param name="source"></param>
        /// <returns>web model</returns>
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
                UpdateRatingRequest = new UpdateRatingRequest
                {
                    ServiceId = source.Id,
                    Value = (int)Math.Floor(source.AverageRating),
                    AverageRating = Math.Round(source.AverageRating, 2),
                },
                Categories = source.Categories
                    .Select(WEB_CATEGORY.Conversion.ConvertCategory)
                    .ToHashSet()
            };

            return target;
        }

        /// <summary>
        /// Converts a web request to service request.
        /// </summary>
        /// <param name="source"></param>
        /// <returns>service model</returns>
        public static SERVICES_SERVICES.CreateServiceRequest ConvertService(CreateServiceRequest source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var target = new SERVICES_SERVICES.CreateServiceRequest
            {
                UserId = source.UserId,
                Name = source.Name,
                Description = source.Description,
                OpenTime = TimeOnly.Parse(source.OpenTime),
                CloseTime = TimeOnly.Parse(source.CloseTime),
                Location = source.Location,
            };

            return target;
        }

        /// <summary>
        /// Converts a web request to service request.
        /// </summary>
        /// <param name="source"></param>
        /// <returns>service model</returns>
        public static SERVICES_SERVICES.UpdateServiceRequest ConvertService(UpdateServiceRequest source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var target = new SERVICES_SERVICES.UpdateServiceRequest
            {
                Id = source.Id,
                Name = source.Name,
                Description = source.Description,
                OpenTime = TimeOnly.Parse(source.OpenTime),
                CloseTime = TimeOnly.Parse(source.CloseTime),
                Location = source.Location,
            };

            return target;
        }

        /// <summary>
        /// Converts a web request to service request.
        /// </summary>
        /// <param name="source"></param>
        /// <returns>service model</returns>
        public static SERVICES_SERVICES.UpdateRatingRequest ConvertUpdateRating(UpdateRatingRequest source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var target = new SERVICES_SERVICES.UpdateRatingRequest
            {
                ServiceId = source.ServiceId,
                UserId = source.UserId,
                Value = source.Value,
            };

            return target;
        }
    }
}
