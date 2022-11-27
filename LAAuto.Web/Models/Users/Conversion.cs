using SERVICES_USERS = LAAuto.Services.Users;

namespace LAAuto.Web.Models.Users
{
    public class Conversion
    {
        public static UserViewModel ConvertUser(SERVICES_USERS.User source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var target = new UserViewModel()
            {
                Id = source.Id,
                UserName = source.UserName,
                Email = source.Email,
                FirstName = source.FirstName,
                LastName = source.LastName,
                PhoneNumber = source.PhoneNumber,
            };

            return target;
        }
    }
}
