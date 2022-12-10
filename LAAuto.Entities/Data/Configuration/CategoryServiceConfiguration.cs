using LAAuto.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LAAuto.Entities.Data.Configuration
{
    internal class CategoryServiceConfiguration : IEntityTypeConfiguration<CategoryService>
    {
        public void Configure(EntityTypeBuilder<CategoryService> builder)
        {
            builder.HasData(SeedCategoryServices());
        }

        private List<CategoryService> SeedCategoryServices()
        {
            var categories = new List<CategoryService>()
            {
                new CategoryService()
                {
                    Id = Guid.Parse("accfbc90-1486-44b8-9a97-caeecf550391"),
                    CategoryId = Guid.Parse("7294f257-a657-4797-8fce-272319ade2f9"),
                    ServiceId = Guid.Parse("9226a3f4-35aa-4817-adcd-1c033cf739ad")
                },
                new CategoryService()
                {
                    Id = Guid.Parse("8317f4c5-3f8b-4020-bbcf-2adb5e30639b"),
                    CategoryId = Guid.Parse("7294f257-a657-4797-8fce-272319ade2f9"),
                    ServiceId = Guid.Parse("e17b327f-eee6-4011-9905-bc8360cd5e66")
                },
                new CategoryService()
                {
                    Id = Guid.Parse("6a62c3f8-aa54-4857-9599-fcbba31da47d"),
                    CategoryId = Guid.Parse("0c1237b8-2fe4-43f7-b6dc-2a0a4ef0713d"),
                    ServiceId = Guid.Parse("e17b327f-eee6-4011-9905-bc8360cd5e66")
                },
            };

            return categories;
        }
    }
}
