using LAAuto.Services.Categories;
using ENTITIES = LAAuto.Entities.Models;

namespace LAAuto.Services.Impl.Categories
{
    /// <summary>
    /// Represents a conversion class for converting category models.
    /// </summary>
    public static class Conversion
    {
        /// <summary>
        /// Converts an entity category to service category.
        /// </summary>
        /// <param name="source">the source</param>
        /// <returns>service category</returns>
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

        /// <summary>
        /// Converts the service request to entity mode.
        /// </summary>
        /// <param name="source">the source</param>
        /// <returns>entity model</returns>
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
