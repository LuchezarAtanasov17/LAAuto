using LAAuto.Services.Owners;
using ENTITIES = LAAuto.Entities.Models;

namespace LAAuto.Services.Impl.Owners
{
    public static class Conversion
    {
        public static Owner ConvertOwner(ENTITIES.Owner source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var target = new Owner()
            {
                Id = source.Id,
                Username = source.UserName,
                FirstName = source.FirstName,
                LastName = source.LastName,
                PhoneNumber = source.PhoneNumber,
            };

            return target;
        }

        public static ENTITIES.Client ConvertOwner(CreateOwnerRequest source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var target = new ENTITIES.Client()
            {
                UserName = source.Username,
                FirstName = source.FirstName,
                LastName = source.LastName,
                PhoneNumber = source.PhoneNumber,
                NormalizedUserName = source.Username.ToUpper(),
            };

            return target;
        }
    }
}
