using LAAuto.Entities.Data;
using LAAuto.Services.Appointments;
using LAAuto.Services.Categories;
using LAAuto.Services.Ratings;
using LAAuto.Services.Services;
using Microsoft.EntityFrameworkCore;
using ENTITIES = LAAuto.Entities.Models;

namespace LAAuto.Services.Impl.Services
{
    public class ServiceService : IServiceService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppointmentService _appointmentService;
        private readonly ICategoryService _categoryService;
        private readonly IRatingService _ratingService;

        public ServiceService(
            ApplicationDbContext context,
             IAppointmentService appointmentService,
            ICategoryService categoryService,
            IRatingService ratingService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _appointmentService = appointmentService ?? throw new ArgumentNullException(nameof(appointmentService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _ratingService = ratingService ?? throw new ArgumentNullException(nameof(ratingService));
        }

        public async Task<List<Service>> ListServicesAsync(string? categoryFilter = null)
        {
            var entities = await _context.Services
                .Include(x => x.User)
                .Include(x => x.CategoryServices)
                .Include(x => x.Appointments)
                .ToListAsync();

            var services = entities
                 .Select(Conversion.ConvertService)
                 .ToList();

            if (categoryFilter is not null)
            {
                services = services
                    .Where(x => x.Categories
                        .Any(c => c.Name == categoryFilter))
                    .ToList();
            }

            foreach (var service in services)
            {
                service.AverageRating = await CalculateAverageServiceRatingAsync(service.Id);
                service.Appointments = await _appointmentService.ListAppointmentsAsync(service.Id);
                service.Categories = await _categoryService.ListCategoriesAsync(serviceId: service.Id);
            }

            return services;
        }

        public async Task<List<Service>> ListMyServicesAsync(Guid userId)
        {
            var entities = await _context.Services
                .Include(x => x.User)
                .Include(x => x.CategoryServices)
                .Include(x => x.Appointments)
                .Where(x => x.UserId == userId)
                .ToListAsync();

            List<Service> services = entities
                .Select(Conversion.ConvertService)
                .ToList();

            return services;
        }

        public async Task<Service> GetServiceAsync(Guid id)
        {
            var entity = await _context.Services
                .Include(x => x.User)
                .Include(x => x.CategoryServices)
                .Include(x => x.Appointments)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
            {
                throw new ObjectNotFoundException($"Could not find service with ID {id}.");
            }

            var service = Conversion.ConvertService(entity);

            //service.AverageRating = await CalculateAverageServiceRatingAsync(id);

            service.Appointments = await _appointmentService.ListAppointmentsAsync(service.Id);
            service.Categories = await _categoryService.ListCategoriesAsync(serviceId: service.Id);

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

            foreach (var category in request.Categories)
            {
                var categoryService = new ENTITIES.CategoryService
                {
                    Id = Guid.NewGuid(),
                    ServiceId = entity.Id,
                    CategoryId = category.Id,
                };

                await _context.CategoryServices.AddAsync(categoryService);
            }

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

            entity.Name = request.Name;
            entity.OpenTime = request.OpenTime;
            entity.CloseTime = request.CloseTime;
            entity.Location = request.Location;
            entity.Description = request.Description;

            var categoryServices = _context.CategoryServices.Where(x => x.ServiceId == entity.Id);
            _context.CategoryServices.RemoveRange(categoryServices);

            foreach (var category in request.Categories)
            {
                var categoryService = new ENTITIES.CategoryService
                {
                    Id = Guid.NewGuid(),
                    ServiceId = entity.Id,
                    CategoryId = category.Id,
                };

                await _context.CategoryServices.AddAsync(categoryService);
            }
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
     
        public async Task CancelAppointmentAsync(Guid id)
        {
            var service = await _context.Services
                .FirstOrDefaultAsync(x => x.Id == id);

            if (service is null)
            {
                throw new ObjectNotFoundException($"Could not find service with ID {id}");
            }
        }

        private async Task<double> CalculateAverageServiceRatingAsync(Guid serviceId)
        {
            var filter = new Filter()
            {
                ServiceId = serviceId
            };

            var ratings = await _ratingService.ListRatingsAsync(filter);

            double average = ratings.Count > 0
                ? ratings.Average(x => x.Value)
                : 0;

            return average;
        }
    }
}
