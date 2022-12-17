using LAAuto.Entities.Data;
using LAAuto.Services.Categories;
using Microsoft.EntityFrameworkCore;
using ENTITIES = LAAuto.Entities.Models;

namespace LAAuto.Services.Impl.Categories
{
    /// <summary>
    /// Represents an category service.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initialize a new instance of the <see cref="CategoryService"/> class.
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public CategoryService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public async Task<List<Category>> ListCategoriesAsync(Guid? serviceId = null)
        {
            var entities = await _context.Categories
                .ToListAsync();

            if (serviceId != null)
            {
                entities = await _context.CategoryServices
                    .Where(x => x.ServiceId == serviceId)
                    .Select(x => x.Category)
                    .ToListAsync();
            }

            var categories = entities
                .Select(Conversion.ConvertCategory)
                .ToList();

            return categories;
        }

        /// <inheritdoc/>
        public async Task<Category> GetCategoryAsync(Guid id)
        {
            var entity = await _context.Categories
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
            {
                throw new ObjectNotFoundException($"Could not find category with ID {id}");
            }

            var appointment = Conversion.ConvertCategory(entity);

            return appointment;
        }

        /// <inheritdoc/>
        public async Task CreateCategoryAsync(CreateCategoryRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var entity = Conversion.ConvertCategory(request);

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteCategoryAsync(Guid id)
        {
            var entity = await _context.Categories
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
            {
                throw new ObjectNotFoundException($"Could not find category with ID {id}");
            }

            _context.Categories.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
