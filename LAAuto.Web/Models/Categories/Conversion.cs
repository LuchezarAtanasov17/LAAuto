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
