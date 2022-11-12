using System.ComponentModel.DataAnnotations.Schema;

namespace LAAuto.Entities.Models
{
    public class CategoryService
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public Guid ServiceId { get; set; }

        public Category Category { get; set; }

        public Service Service { get; set; }
    }
}
