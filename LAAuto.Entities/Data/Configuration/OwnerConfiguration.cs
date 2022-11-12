using LAAuto.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAAuto.Entities.Data.Configuration
{
    internal class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.HasData(new Owner()
            {
                Id = Guid.Parse("95a9873f-d6f4-4496-bf75-62be88716460"),
                UserName = "Owner",
                NormalizedUserName = "OWNER",
                Email = "owner@mail.com",
                NormalizedEmail = "OWNER@MAIL.COM",
                FirstName = "Ivan",
                LastName = "Ivanov"
            });
        }
    }
}
