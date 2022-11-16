using LAAuto.Entities.Data;
using LAAuto.Services.Owners;
using Microsoft.EntityFrameworkCore;

namespace LAAuto.Services.Impl.Owners
{
    public class OwnerService : IOwnerService
    {
        private readonly ApplicationDbContext _context;

        public OwnerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Owner>> ListOwnersAsync()
        {
            var entities = await _context.Owners.ToListAsync();

            var owners = entities
                .Select(Conversion.ConvertOwner)
                .ToList();

            return owners;
        }

        public async Task<Owner> GetOwnerAsync(Guid id)
        {
            var entity = await _context.Owners
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
            {
                throw new ObjectNotFoundException($"Could not find owner with ID: {id}");
            }

            var owner = Conversion.ConvertOwner(entity);

            return owner;
        }

        public async Task CreateOwnerAsync(CreateOwnerRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var entity = Conversion.ConvertOwner(request);

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOwnerAsync(Guid id, UpdateOwnerRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var entity = await _context.Owners
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
            {
                throw new ObjectNotFoundException($"Could not find owner with ID {id}");
            }

            entity.UserName = request.Username;
            entity.NormalizedUserName = request.Username.ToUpper();
            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.PhoneNumber = request.PhoneNumber;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteOwnerAsync(Guid id)
        {
            var entity = _context.Owners
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
            {
                throw new ObjectNotFoundException($"Could not find owner with ID {id}");
            }

            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

    }
}
