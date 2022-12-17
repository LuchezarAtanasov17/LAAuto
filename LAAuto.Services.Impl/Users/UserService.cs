using LAAuto.Entities.Data;
using LAAuto.Services.Users;
using Microsoft.EntityFrameworkCore;

namespace LAAuto.Services.Impl.Users
{
    /// <summary>
    /// Represents an service service.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initialize a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public UserService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public async Task<List<User>> ListUsersAsync()
        {
            var entities = await _context.Users
                .ToListAsync();

            var users = entities
                .Select(Conversion.ConvertUser)
                .ToList();

            return users;
        }

        /// <inheritdoc/>
        public async Task<User> GetUserAsync(Guid id)
        {
            var entity = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
            {
                throw new ObjectNotFoundException($"Could not find a user with ID {id}.");
            }

            var user = Conversion.ConvertUser(entity);

            return user;
        }

        /// <inheritdoc/>
        public async Task DeleteUserAsync(Guid id)
        {
            var entity = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
            {
                throw new ObjectNotFoundException($"Could not find user with ID {id}");
            }

            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
