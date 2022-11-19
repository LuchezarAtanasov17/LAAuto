using LAAuto.Entities.Data;
using LAAuto.Services.Users;
using Microsoft.EntityFrameworkCore;

namespace LAAuto.Services.Impl.Users
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<User>> ListUsersAsync()
        {
            var entities = await _context.Users.ToListAsync();

            var users = entities
                .Select(Conversion.ConvertUser)
                .ToList();

            return users;
        }

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

        public async Task CreateUserAsync(CreateUserRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var entity = Conversion.ConvertUser(request);
            
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(Guid id, UpdateUserRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var entity = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
            {
                throw new ObjectNotFoundException($"Could not find a user with ID {id}.");
            }

            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.PhoneNumber = request.PhoneNumber;
            entity.UserName = request.UserName;
            entity.NormalizedUserName = request.UserName.ToUpper();

            await _context.SaveChangesAsync();
        }
    }
}
