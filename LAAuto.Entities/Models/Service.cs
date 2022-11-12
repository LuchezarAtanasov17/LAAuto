using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LAAuto.Entities.Models
{
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [StringLength(300)]
        public string? Description { get; set; }

        [Required]
        [StringLength(80)]
        public string Location { get; set; } = null!;

        [Required]
        public TimeOnly OpenTime { get; set; }

        [Required]
        public TimeOnly CloseTime { get; set; }

        [Required]
        [ForeignKey(nameof(OwnerId))]
        public Owner Owner { get; set; }

//      public string Picture { get; set; } Ако снимката е растерна може да си го запиша като стринг и след това в базата само си го конвертира, може и като масив от байтове

        public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();

        public ICollection<Category> Categories { get; set; } = new HashSet<Category>();

        public ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();

        public ICollection<CategoryService> CategoryServices { get; set; } = new HashSet<CategoryService>();
    }
}
