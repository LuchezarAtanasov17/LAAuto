using LAAuto.Web.Models.Categories;
using System.ComponentModel.DataAnnotations;

namespace LAAuto.Web.Models.Services
{
    public class UpdateServiceRequest
    {
        public Guid Id { get; set; }

        public List<Guid> CategoryIds { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [Required]
        [StringLength(80, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        public string Location { get; set; } = null!;

        public string OpenTime { get; set; }

        public string CloseTime { get; set; }
        public IFormFile Image { get; set; }

        public List<SelectCategoryViewModel> Categories { get; set; }
    }
}
