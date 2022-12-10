using SERVICES_RATINGS = LAAuto.Services.Ratings;
using WEB_SERVICES = LAAuto.Web.Models.Services;
using WEB_USERS = LAAuto.Web.Models.Users;

namespace LAAuto.Web.Models.Ratings
{
    public class Conversion
    {
        public static RatingViewModel ConvertRating(SERVICES_RATINGS.Rating source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var target = new RatingViewModel()
            {
                Id = source.Id,
                ServiceId = source.ServiceId,
                UserId = source.UserId,
                Value = source.Value,
                Service = WEB_SERVICES.Conversion.ConvertService(source.Service),
                User = WEB_USERS.Conversion.ConvertUser(source.User),
            };

            return target;
        }

        public static UpdateRatingRequest ConvertUpdateRatingViewModel(SERVICES_RATINGS.UpdateRatingRequest source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var target = new UpdateRatingRequest()
            {
                Value = source.Value,
            };

            return target;
        }
    }
}
