using LAAuto.Services.Services;
using ENTITIES = LAAuto.Entities.Models;
using SERVICES_IMPL_USERS = LAAuto.Services.Impl.Users;

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
                Image = source.Image,
                User = SERVICES_IMPL_USERS.Conversion.ConvertUser(source.User),
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
                Image = source.Image,
            };

            return target;
        }
    }
}
