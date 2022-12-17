using LAAuto.Entities.Data.Configuration;
using LAAuto.Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LAAuto.Entities.Data
{
    /// <summary>
    /// Represents the database context.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">the options</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var timeOnlyToTimeSpanConverter = new TimeOnlyToTimeSpanConverter();

            modelBuilder.Entity<Appointment>(builder =>
            {
                builder.HasOne(x => x.Service)
                    .WithMany(x => x.Appointments)
                    .HasForeignKey(x => x.ServiceId)
                    .HasPrincipalKey(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(x => x.User)
                    .WithMany(x => x.Appointments)
                    .HasForeignKey(x => x.UserId)
                    .HasPrincipalKey(x => x.Id)
                    .OnDelete(DeleteBehavior.NoAction);

                builder.HasOne(x => x.Category)
                    .WithMany(x => x.Appointments)
                    .HasForeignKey(x => x.CategoryId)
                    .HasPrincipalKey(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Rating>(builder =>
            {
                builder.HasOne(x => x.Service)
                    .WithMany(x => x.Ratings)
                    .HasForeignKey(x => x.ServiceId)
                    .HasPrincipalKey(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(x => x.User)
                    .WithMany(x => x.Ratings)
                    .HasForeignKey(x => x.UserId)
                    .HasPrincipalKey(x => x.Id)
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
                    .HasPrincipalKey(x => x.Id)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<CategoryService>(builder =>
            {
                builder.HasKey(cs => new { cs.CategoryId, cs.ServiceId });

                builder.HasOne(x => x.Category)
                    .WithMany(x => x.CategoryServices)
                    .HasForeignKey(x => x.CategoryId)
                    .HasPrincipalKey(x => x.Id);

                builder.HasOne(x => x.Service)
                    .WithMany(x => x.CategoryServices)
                    .HasForeignKey(x => x.ServiceId)
                    .HasPrincipalKey(x => x.Id);
            });

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryServiceConfiguration());
            modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
            modelBuilder.ApplyConfiguration(new RatingConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityRoleConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Gets or sets the appointments.
        /// </summary>
        public DbSet<Appointment> Appointments { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the ratings.
        /// </summary>
        public DbSet<Rating> Ratings { get; set; }

        /// <summary>
        /// Gets or sets the services.
        /// </summary>
        public DbSet<Service> Services { get; set; }

        /// <summary>
        /// Gets or sets the category services.
        /// </summary>
        public DbSet<CategoryService> CategoryServices { get; set; }
    }
}