using System.ComponentModel.DataAnnotations;

namespace LAAuto.Web.Models.Categories
{
    public class CreateCategoryRequest
    {
        [Required]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string Name { get; set; }
    }
}
