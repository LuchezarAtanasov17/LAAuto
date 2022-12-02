﻿using LAAuto.Entities.Data;
using LAAuto.Services.Ratings;
using LAAuto.Services.Services;
using Microsoft.EntityFrameworkCore;

namespace LAAuto.Services.Impl.Services
{
    public class ServiceService : IServiceService
    {
        private readonly ApplicationDbContext _context;
        private readonly IRatingService _ratingService;

        public ServiceService(
            ApplicationDbContext context,
            IRatingService ratingService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
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

            entity.UserId = request.UserId;
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
        public async Task MakeAppointmentAsync(Guid id, Guid currentUserId)
        {
            var entity = await _context.Services
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var service = Conversion.ConvertService(entity);

            var appointment = service.Appointments.FirstOrDefault(x => x.UserId == currentUserId);

            // appointment.
        }

        public async Task CancelAppointmentAsync(Guid id)
        {
            var service = await _context.Services
                .FirstOrDefaultAsync(x => x.Id == id);

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }
        }

        private async Task<double> CalculateAverageServiceRatingAsync(Guid serviceId)
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
