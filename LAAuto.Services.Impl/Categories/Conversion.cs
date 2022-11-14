using LAAuto.Services.Categories;
using ENTITIES = LAAuto.Entities.Models;

namespace LAAuto.Services.Impl.Categories
{
    public static class Conversion
    {
        public static Category ConvertCategory(ENTITIES.Category source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var target = new Category()
            {
                Id = source.Id,
                Name = source.Name,
            };

            return target;
        }

        public static ENTITIES.Category ConvertCategory(CreateCategoryRequest source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var target = new ENTITIES.Category()
            {
                Name = source.Name,
            };

            return target;
        }
    }
}
