using LAAuto.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LAAuto.Entities.Data.Configuration
{
    internal class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasData(SeedServices());
        }

        private List<Service> SeedServices()
        {
            var services = new List<Service>();

            Service service = new()
            {
                Id = Guid.Parse("e17b327f-eee6-4011-9905-bc8360cd5e66"),
                UserId = Guid.Parse("62448744-4356-44dc-a005-0bfb6ba9e8b2"),
                Name = "Carx",
                Location = "Гр.София, кв.Надежда, ул.Стамболийски 36",
                OpenTime = TimeOnly.Parse("9:00:00"),
                CloseTime = TimeOnly.Parse("18:00:00"),
                Image = null
            };
            services.Add(service);

            service = new Service()
            {
                Id = Guid.Parse("9226a3f4-35aa-4817-adcd-1c033cf739ad"),
                UserId = Guid.Parse("62448744-4356-44dc-a005-0bfb6ba9e8b2"),
                Name = "Autox",
                Location = "Гр.Пловдив, кв.Кичука, ул.Македония 12",
                OpenTime = TimeOnly.Parse("8:00:00"),
                CloseTime = TimeOnly.Parse("18:00:00"),
                Image = null
            };
            services.Add(service);

            service = new Service()
            {
                Id = Guid.Parse("fce201d7-e941-4f41-b3be-0c265798ede9"),
                UserId = Guid.Parse("62448744-4356-44dc-a005-0bfb6ba9e8b2"),
                Name = "CarKing",
                Location = "Гр.Varna, кв.Владиславово, ул.Георги Минков 3",
                OpenTime = TimeOnly.Parse("10:00:00"),
                CloseTime = TimeOnly.Parse("20:00:00"),
                Image = null
            };
            services.Add(service);

            return services;
        }
    }
}
