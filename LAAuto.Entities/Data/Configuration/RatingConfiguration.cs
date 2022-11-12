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
    internal class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasData(new Rating()
            {
                Id = Guid.Parse("eecdc117-7fbe-4c46-bbf9-8507b45c0d88"),
                ClientId = Guid.Parse("62448744-4356-44dc-a005-0bfb6ba9e8b2"),
                ServiceId = Guid.Parse("e17b327f-eee6-4011-9905-bc8360cd5e66"),
                Value = 4
            });
        }
    }
}
