using LAAuto.Services.Clients;
using ENTITIES = LAAuto.Entities.Models;

namespace LAAuto.Services.Impl.Clients
{
    public static class Conversion
    {
        public static Client ConvertClients(ENTITIES.Client source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var target = new Client()
            {
                FirstName = source.FirstName,
                LastName = source.LastName,
                Email = source.Email,
                PhoneNumber = source.PhoneNumber
            };

            return target;
        }

        public static ENTITIES.Client ConvertClients(CreateClientRequest source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var target = new ENTITIES.Client()
            {
                UserName = source.UserName,
                FirstName = source.FirstName,
                LastName = source.LastName,
                PhoneNumber = source.PhoneNumber,
                Email = source.Email,
                NormalizedUserName = source.UserName.ToUpper()
            };

            return target;
        }
    }
}
