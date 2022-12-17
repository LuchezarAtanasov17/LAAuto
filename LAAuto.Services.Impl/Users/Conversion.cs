using LAAuto.Services.Users;
using ENTITIES = LAAuto.Entities.Models;

namespace LAAuto.Services.Impl.Users
{
    /// <summary>
    /// Represents a conversion class for converting service models.
    /// </summary>
    public static class Conversion
    {
        /// <summary>
        /// Converts an entity user to service user.
        /// </summary>
        /// <param name="source">the source</param>
        /// <returns>service user</returns>
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

        /// <summary>
        /// Converts the service user to entity model.
        /// </summary>
        /// <param name="source">the source</param>
        /// <returns>entity model</returns>
        public static ENTITIES.User ConvertUser(User source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var target = new ENTITIES.User()
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
