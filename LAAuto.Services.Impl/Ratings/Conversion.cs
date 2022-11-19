using LAAuto.Services.Ratings;
using ENTITIES = LAAuto.Entities.Models;
using SERVICES_IMPL_SERVICES = LAAuto.Services.Impl.Services;
using SERVICES_IMPL_USERS = LAAuto.Services.Impl.Users;


namespace LAAuto.Services.Impl.Ratings
{
    public static class Conversion
    {
        public static Rating ConvertRating(ENTITIES.Rating source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var target = new Rating()
            {
                Id = source.Id,
                ClientId = source.UserId,
                ServiceId = source.ServiceId,
                Value = source.Value,
                Service = SERVICES_IMPL_SERVICES.Conversion.ConvertService(source.Service), 
                User = SERVICES_IMPL_USERS.Conversion.ConvertUser(source.User),
            };

            return target;
        }

        public static ENTITIES.Rating ConvertRating(UpdateRatingRequest source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var target = new ENTITIES.Rating()
            {
                UserId = source.ClientId,
                ServiceId = source.ServiceId,
                Value = source.Value,
            };

            return target;
        }
    }
}
