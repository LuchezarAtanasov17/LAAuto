using LAAuto.Entities.Data.Configuration;
using LAAuto.Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LAAuto.Entities.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var timeOnlyToTimeSpanConverter = new TimeOnlyToTimeSpanConverter();

            modelBuilder.Entity<Appointment>(builder =>
            {
                builder.HasOne(x => x.Service)
                    .WithMany(x => x.Appointments)
                    .HasForeignKey(x => x.ServiceId)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(x => x.User)
                    .WithMany(x => x.Appointments)
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.NoAction);

                builder.HasOne(x => x.Category)
                    .WithMany(x => x.Appointments)
                    .HasForeignKey(x => x.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Rating>(builder =>
            {
                builder.HasOne(x => x.Service)
                    .WithMany(x => x.Ratings)
                    .HasForeignKey(x => x.ServiceId)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(x => x.User)
                    .WithMany(x => x.Ratings)
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Service>(builder =>
            {
                builder.Property(x => x.OpenTime)
                    .HasConversion(timeOnlyToTimeSpanConverter);
                
                builder.Property(x => x.CloseTime)
                    .HasConversion(timeOnlyToTimeSpanConverter);

                builder.HasOne(x => x.User)
                    .WithMany(x => x.Services)
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.NoAction);
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
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RatingConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<CategoryService> CategoryServices { get; set; }
    }
}