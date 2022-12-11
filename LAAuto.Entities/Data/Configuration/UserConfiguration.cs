using LAAuto.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LAAuto.Entities.Data.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(new User()
            {
                Id = Guid.Parse("62448744-4356-44dc-a005-0bfb6ba9e8b2"),
                UserName = "User",
                NormalizedUserName = "CLIENT",
                Email = "client@mail.com",
                NormalizedEmail = "CLIENT@MAIL.COM",
                FirstName = "Pesho",
                LastName = "Peshov"
            });

            //builder.HasData(new User()
            //{
            //    Id = Guid.Parse("1456c79b-7080-4586-8467-900a3cb033fe"),
            //    UserName = "Administrator",
            //    NormalizedUserName = "ADMINISTRATOR",
            //    Email = "admin@gmail.com",
            //    NormalizedEmail = "ADMIN@GMAIL.COM",
            //    FirstName = "Luchezar",
            //    LastName = "Atanasov",
            //});
        }
    }
}
