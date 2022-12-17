using SERVICES_CATEGORIES = LAAuto.Services.Categories;

namespace LAAuto.Web.Models.Categories
{
    /// <summary>
    /// Represents a conversion class for converting web models.
    /// </summary>
    public class Conversion
    {
        /// <summary>
        /// Converts a service category to web category.
        /// </summary>
        /// <param name="source"></param>
        /// <returns>web model</returns>
        public static CategoryViewModel ConvertCategory(SERVICES_CATEGORIES.Category source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            CategoryViewModel target = new CategoryViewModel()
            {
                Id = source.Id,
                Name = source.Name,
            };

            return target;
        }

        /// <summary>
        /// Converts a service category to web select category.
        /// </summary>
        /// <param name="source"></param>
        /// <returns>web model</returns>
        public static SelectCategoryViewModel ConvertSelectCategory(SERVICES_CATEGORIES.Category source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            SelectCategoryViewModel target = new SelectCategoryViewModel()
            {
                Id = source.Id,
                Name = source.Name,
                IsSelected = false,
            };

            return target;
        }

        /// <summary>
        /// Converts a web select category to service category.
        /// </summary>
        /// <param name="source"></param>
        /// <returns>service model</returns>
        public static SERVICES_CATEGORIES.Category ConvertSelectCategory(SelectCategoryViewModel source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            SERVICES_CATEGORIES.Category target = new SERVICES_CATEGORIES.Category()
            {
                Id = source.Id,
                Name = source.Name,
            };

            return target;
        }

        /// <summary>
        /// Converts an web request to service request.
        /// </summary>
        /// <param name="source"></param>
        /// <returns>service model</returns>
        public static SERVICES_CATEGORIES.CreateCategoryRequest ConvertCreateCategoryRequest(CreateCategoryRequest source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            SERVICES_CATEGORIES.CreateCategoryRequest target = new SERVICES_CATEGORIES.CreateCategoryRequest()
            {
                Name = source.Name,
            };

            return target;
        }
    }
}
