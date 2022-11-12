using LAAuto.Entities.Data.Configuration;
using LAAuto.Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LAAuto.Entities.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var timeOnlyToTimeSpanConverter = new TimeOnlyToTimeSpanConverter();

            modelBuilder.Entity<Service>(builder =>
            {
                builder.Property(x => x.OpenTime)
                    .HasConversion(timeOnlyToTimeSpanConverter);

                builder.Property(x => x.CloseTime)
                    .HasConversion(timeOnlyToTimeSpanConverter);
            });
            modelBuilder.Entity<CategoryService>(builder =>
            {
                builder.HasKey(cs => new { cs.CategoryId, cs.ServiceId });

                builder.HasOne(x => x.Category)
                    .WithMany(x => x.CategoryServices)
                    .HasForeignKey(x => x.CategoryId);

                builder.HasOne(x => x.Service)
                    .WithMany(x => x.CategoryServices)
                    .HasForeignKey(x => x.ServiceId);
            });

            modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new OwnerConfiguration());
            modelBuilder.ApplyConfiguration(new RatingConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<CategoryService> CategoryServices { get; set; }
    }
}