using LAAuto.Services.Users;
using ENTITIES = LAAuto.Entities.Models;

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
