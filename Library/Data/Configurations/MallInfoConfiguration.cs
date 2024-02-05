using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class MallInfoConfiguration : IEntityTypeConfiguration<MallInfo>
    {
        public void Configure(EntityTypeBuilder<MallInfo> builder)
        {
            builder.ToTable("mall-infos");

            builder.HasKey(x => x.PkId);
        }
    }
}
