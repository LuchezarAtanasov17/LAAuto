using LAAuto.Entities.Data;
using LAAuto.Services.Ratings;
using Microsoft.EntityFrameworkCore;

namespace LAAuto.Services.Impl.Ratings
{
    public class RatingService : IRatingService
    {
        private readonly ApplicationDbContext _context;

        public RatingService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Rating>> ListRatingsAsync(Filter? filter = null)
        {
            var entities = await _context.Ratings
                .Include(x => x.User)
                .Include(x => x.Service)
                .ToListAsync();

            if (filter is not null)
            {
                if (filter.UserId is not null)
                {
                    entities = entities.Where(x => x.UserId == filter.UserId).ToList();
                }
                if (filter.ServiceId is not null)
                {
                    entities = entities.Where(x => x.ServiceId == filter.ServiceId).ToList();
                }
            }

            var ratings = entities
                .Select(Conversion.ConvertRating)
                .ToList();

            return ratings;
        }

        public async Task UpdateRatingAsync(Guid clientId, Guid serviceId, UpdateRatingRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var entities = await _context.Ratings
                .Where(x => x.UserId == clientId && x.ServiceId == serviceId)
                .ToListAsync();

            _context.Ratings.RemoveRange(entities);

            var rating = Conversion.ConvertRating(request);
            
            await _context.AddAsync(rating);

            await _context.SaveChangesAsync();
        }
    }
}
