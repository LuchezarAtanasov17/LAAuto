using LAAuto.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAAuto.Entities.Data.Configuration
{
    internal class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasData(new Client()
            {
                Id = Guid.Parse("62448744-4356-44dc-a005-0bfb6ba9e8b2"),
                UserName = "Client",
                NormalizedUserName = "CLIENT",
                Email = "client@mail.com",
                NormalizedEmail = "CLIENT@MAIL.COM",
                FirstName = "Pesho",
                LastName = "Peshov"
            });
        }
    }
}
