using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LAAuto.Entities.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Name { get; set; } = null!;

        public ICollection<Service> Services { get; set; } = new HashSet<Service>();

        public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();

        public ICollection<CategoryService> CategoryServices { get; set; } = new HashSet<CategoryService>();
    }
}
