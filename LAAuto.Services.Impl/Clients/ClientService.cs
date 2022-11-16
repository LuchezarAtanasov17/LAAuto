using LAAuto.Entities.Data;
using LAAuto.Services.Clients;
using Microsoft.EntityFrameworkCore;

namespace LAAuto.Services.Impl.Clients
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _context;

        public ClientService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Client>> ListCategoriesAsync()
        {
            var entities = await _context.Clients.ToListAsync();

            var clients = entities
                .Select(Conversion.ConvertClients)
                .ToList();

            return clients;
        }

        public async Task<Client> GetCategoryAsync(Guid id)
        {
            var entity = await _context.Clients
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
            {
                throw new ObjectNotFoundException($"Could not find client with ID {id}.");
            }

            var client = Conversion.ConvertClients(entity);

            return client;
        }

        public async Task CreateClientService(CreateClientRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var entity = Conversion.ConvertClients(request);
            
            await _context.Clients.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClientService(Guid id, UpdateClientRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var entity = await _context.Clients.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
            {
                throw new ObjectNotFoundException($"Could not find client with ID {id}.");
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
