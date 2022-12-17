using LAAuto.Services.Services;
using ENTITIES = LAAuto.Entities.Models;

namespace LAAuto.Services.Impl.Services
{
    /// <summary>
    /// Represents a conversion class for converting service models.
    /// </summary>
    public static class Conversion
    {
        /// <summary>
        /// Converts an entity service to service service.
        /// </summary>
        /// <param name="source">the source</param>
        /// <returns>service service</returns>
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
            };

            return target;
        }


        /// <summary>
        /// Converts the service request to entity model.
        /// </summary>
        /// <param name="source">the source</param>
        /// <returns>entity model</returns>
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

        /// <summary>
        /// Converts the service request to entity mode.
        /// </summary>
        /// <param name="source">the source</param>
        /// <returns>entity model</returns>
        public static ENTITIES.Rating ConvertRating(UpdateRatingRequest source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var target = new ENTITIES.Rating()
            {
                ServiceId = source.ServiceId,
                UserId = source.UserId,
                Value = source.Value,
            };

            return target;
        }
    }
}
