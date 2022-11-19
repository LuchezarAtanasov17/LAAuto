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
    internal class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasData(new Appointment()
            {
                Id = Guid.Parse("9d8afaca-f28c-4fce-bc14-5c3363633323"),
                ServiceId = Guid.Parse("e17b327f-eee6-4011-9905-bc8360cd5e66"),
                UserId = Guid.Parse("62448744-4356-44dc-a005-0bfb6ba9e8b2"),
                CategoryId = Guid.Parse("7294f257-a657-4797-8fce-272319ade2f9"),
                StartDate = DateTime.Parse("19-12-2022"),
                EndDate = DateTime.Parse("20-12-2022")
            });
        }
    }
}
