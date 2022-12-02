using LAAuto.Entities.Data;
using LAAuto.Services.Categories;
using Microsoft.EntityFrameworkCore;

namespace LAAuto.Services.Impl.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Category>> ListCategoriesAsync()
        {
            var entities = await _context.Categories
                .ToListAsync();

            var categories = entities
                .Select(Conversion.ConvertCategory)
                .ToList();

            return categories;
        }

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
