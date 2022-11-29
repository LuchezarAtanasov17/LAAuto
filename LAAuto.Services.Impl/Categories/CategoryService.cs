using LAAuto.Entities.Data;
using LAAuto.Services.Categories;
using LAAuto.Services.Services;
using Microsoft.EntityFrameworkCore;

namespace LAAuto.Services.Impl.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceService _serviceService;

        public CategoryService(
            ApplicationDbContext context, 
            IServiceService serviceService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _serviceService = serviceService ?? throw new ArgumentNullException(nameof(serviceService));
        }

        public async Task<List<Category>> ListCategoriesAsync()
        {
            var entities = await _context.Categories.ToListAsync();

            List<Category> categories = new List<Category>();

            foreach (var entity in entities)
            {
                categories.Add(await GetCategoryAsync(entity.Id));
            }

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

            var category = Conversion.ConvertCategory(entity);
            category.Services = await _serviceService.ListServicesAsync();

            return category;
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
