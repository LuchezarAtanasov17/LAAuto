using SERVICES_CATEGORIES = LAAuto.Services.Categories;

namespace LAAuto.Web.Models.Categories
{
    public class Conversion
    {
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

        public static CreateCategoryRequest ConvertCreateCategoryRequest(SERVICES_CATEGORIES.CreateCategoryRequest source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            CreateCategoryRequest target = new CreateCategoryRequest()
            {
                Name = source.Name,
            };

            return target;
        }
    }
}
