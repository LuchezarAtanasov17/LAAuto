using LAAuto.Services.Services;
using ENTITIES = LAAuto.Entities.Models;

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
                OwnerId = source.OwnerId,
                Name = source.Name,
                OpenTime = source.OpenTime,
                CloseTime = source.CloseTime,
                Location = source.Location,
                Description = source.Description,
                // TODO: Owner = 
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
                OwnerId = source.OwnerId,
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
