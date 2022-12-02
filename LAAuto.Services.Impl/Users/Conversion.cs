using LAAuto.Services.Users;
using ENTITIES = LAAuto.Entities.Models;
using SERVICES_IMPL_SERVICES = LAAuto.Services.Impl.Services;
using SERVICES_IMPL_APPOINTMENTS = LAAuto.Services.Impl.Appointments;

namespace LAAuto.Services.Impl.Users
{
    public static class Conversion
    {
        public static User ConvertUser(ENTITIES.User source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var target = new User()
            {
                UserName = source.UserName,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Email = source.Email,
                PhoneNumber = source.PhoneNumber,
            };

            return target;
        }
    }
}
