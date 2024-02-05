using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class SubcategoryConfiguration : IEntityTypeConfiguration<Subcategory>
    {
        public void Configure(EntityTypeBuilder<Subcategory> builder)
        {
            builder.ToTable("subcategories");

            builder.HasKey(x => x.PkId);
        }
    }
}
