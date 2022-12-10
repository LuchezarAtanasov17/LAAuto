using LAAuto.Services.Categories;
using LAAuto.Services.Services;

namespace LAAuto.Services.CategoryServices
{
    public class CategoryService
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public Guid ServiceId { get; set; }

        public Category Category { get; set; }

        public Service Service { get; set; }
    }
}
