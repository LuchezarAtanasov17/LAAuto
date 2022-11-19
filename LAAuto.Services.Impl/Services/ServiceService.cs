using LAAuto.Entities.Data;
using LAAuto.Services.Ratings;
using LAAuto.Services.Services;
using Microsoft.EntityFrameworkCore;

namespace LAAuto.Services.Impl.Services
{
    public class ServiceService : IServiceService
    {
        private readonly ApplicationDbContext _context;
        private readonly IRatingService _ratingService;

        public ServiceService(ApplicationDbContext context, IRatingService ratingService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _ratingService = ratingService ?? throw new ArgumentNullException(nameof(ratingService));
        }

        public async Task<List<Service>> ListServicesAsync()
        {
            var entities = await _context.Services.ToListAsync();

            var services = entities
                .Select(Conversion.ConvertService)
                .ToList();

            return services;
        }

        public async Task<Service> GetServiceAsync(Guid id)
        {
            var entity = await _context.Services.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
            {
                throw new ObjectNotFoundException($"Could not find service with ID {id}.");
            }

            var service = Conversion.ConvertService(entity);

            service.AverageRating = await CalculateAverageServiceRating(id);

            return service;
        }

        public async Task CreateServiceAsync(CreateServiceRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var entity = Conversion.ConvertService(request);

            await _context.Services.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateServiceAsync(Guid id, UpdateServiceRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var entity = await _context.Services
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
            {
                throw new ObjectNotFoundException($"Could not find service with ID {id}.");
            }

            entity.UserId = request.OwnerId;
            entity.Name = request.Name;
            entity.OpenTime = request.OpenTime;
            entity.CloseTime = request.CloseTime;
            entity.Location = request.Location;
            entity.Description = request.Description;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteServiceAsync(Guid id)
        {
            var entity = await _context.Services
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
            {
                throw new ObjectNotFoundException($"Could not find service with ID {id}");
            }

            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        private async Task<double> CalculateAverageServiceRating(Guid serviceId)
        {
            var filter = new Filter()
            {
                ServiceId = serviceId
            };

            var ratings = await _ratingService.ListRatingsAsync(filter);

            double average = ratings.Average(x => x.Value);

            return average;
        }
    }
}
