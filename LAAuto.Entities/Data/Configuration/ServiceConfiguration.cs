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
                OwnerId = Guid.Parse("95a9873f-d6f4-4496-bf75-62be88716460"),
                Name = "Carx",
                Location = "Гр.София, кв.Надежда, ул.Стамболийски 36",
                OpenTime = TimeOnly.Parse("9:00:00"),
                CloseTime = TimeOnly.Parse("18:00:00"),
            };
            services.Add(service);

            service = new Service()
            {
                Id = Guid.Parse("9226a3f4-35aa-4817-adcd-1c033cf739ad"),
                OwnerId = Guid.Parse("95a9873f-d6f4-4496-bf75-62be88716460"),
                Name = "Autox",
                Location = "Гр.Пловдив, кв.Кичука, ул.Македония 12",
                OpenTime = TimeOnly.Parse("8:00:00"),
                CloseTime = TimeOnly.Parse("18:00:00"),
            };
            services.Add(service);

            service = new Service()
            {
                Id = Guid.Parse("fce201d7-e941-4f41-b3be-0c265798ede9"),
                OwnerId = Guid.Parse("95a9873f-d6f4-4496-bf75-62be88716460"),
                Name = "CarKing",
                Location = "Гр.Varna, кв.Владиславово, ул.Георги Минков 3",
                OpenTime = TimeOnly.Parse("10:00:00"),
                CloseTime = TimeOnly.Parse("20:00:00"),
            };
            services.Add(service);

            return services;
        }
    }
}
