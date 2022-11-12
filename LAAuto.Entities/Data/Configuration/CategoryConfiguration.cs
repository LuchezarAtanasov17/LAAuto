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
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(SeedCategories());
        }

        private List<Category> SeedCategories()
        {
            var categories = new List<Category>();

            Category category = new Category()
            {
                Id = Guid.Parse("7294f257-a657-4797-8fce-272319ade2f9"),
                Name = "Преглед",
            };
            categories.Add(category);

            category = new Category()
            {
                Id = Guid.Parse("e5dea772-dfa1-43ab-a89f-0a91df10123b"),
                Name = "Диагностика",
            };
            categories.Add(category);

            category = new Category()
            {
                Id = Guid.Parse("0c1237b8-2fe4-43f7-b6dc-2a0a4ef0713d"),
                Name = "Смяна на масло",
            };
            categories.Add(category);

            return categories;
        }
    }
}
