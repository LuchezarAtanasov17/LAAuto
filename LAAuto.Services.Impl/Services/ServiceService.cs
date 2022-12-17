using LAAuto.Entities.Data;
using LAAuto.Services.Categories;
using LAAuto.Services.Services;
using Microsoft.EntityFrameworkCore;
using ENTITIES = LAAuto.Entities.Models;
using SERVICES_IMPL_APPOINTMENTS = LAAuto.Services.Impl.Appointments;
using SERVICES_IMPL_USERS = LAAuto.Services.Impl.Users;

namespace LAAuto.Services.Impl.Services
{
    public class ServiceService : IServiceService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategoryService _categoryService;

        public ServiceService(
            ApplicationDbContext context,
            ICategoryService categoryService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        public async Task<List<Service>> ListServicesAsync(Guid? userId = null)
        {
            var entities = await _context.Services
                .Include(x => x.User)
                .Include(x => x.Appointments)
                .Include(x => x.Ratings)
                .ToListAsync();

            if (userId != null)
            {
                entities = entities.Where(x => x.UserId == userId).ToList();
            }

            var services = new List<Service>();
            foreach (var entity in entities)
            {
                var service = Conversion.ConvertService(entity);

                service.User = SERVICES_IMPL_USERS.Conversion.ConvertUser(entity.User);
                service.Appointments = entity.Appointments.Select(SERVICES_IMPL_APPOINTMENTS.Conversion.ConvertAppointment).ToList();
                service.Categories = await _categoryService.ListCategoriesAsync(entity.Id);
                service.AverageRating = CalculateAverageServiceRating(entity.Ratings);

                services.Add(service);
            }

            return services;
        }

        public async Task<Service> GetServiceAsync(Guid id)
        {
            var entity = await _context.Services
                .Include(x => x.User)
                .Include(x => x.Appointments)
                .Include(x => x.Ratings)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
            {
                throw new ObjectNotFoundException($"Could not find service with ID {id}.");
            }

            var service = Conversion.ConvertService(entity);

            service.Categories = await _categoryService.ListCategoriesAsync(entity.Id);
            service.Appointments = entity.Appointments.Select(SERVICES_IMPL_APPOINTMENTS.Conversion.ConvertAppointment).ToList();
            service.User = SERVICES_IMPL_USERS.Conversion.ConvertUser(entity.User);
            service.AverageRating = CalculateAverageServiceRating(entity.Ratings);

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

            foreach (var categoryId in request.CategoryIds)
            {
                var categoryService = new ENTITIES.CategoryService
                {
                    Id = Guid.NewGuid(),
                    ServiceId = entity.Id,
                    CategoryId = categoryId,
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

        public async Task UpdateServiceRatingAsync(UpdateRatingRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var entities = await _context.Ratings
                .Where(x => x.UserId == request.UserId && x.ServiceId == request.ServiceId)
                .ToListAsync();

            _context.Ratings.RemoveRange(entities);

            var rating = Conversion.ConvertRating(request);

            await _context.AddAsync(rating);

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

        private static double CalculateAverageServiceRating(ICollection<ENTITIES.Rating> ratings)
        {
            double average = ratings.Count > 0
                ? ratings.Average(x => x.Value)
                : 0;

            return average;
        }
    }
}
