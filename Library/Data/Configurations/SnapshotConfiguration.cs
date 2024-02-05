using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class SnapshotConfiguration : IEntityTypeConfiguration<Snapshot>
    {
        public void Configure(EntityTypeBuilder<Snapshot> builder)
        {
            builder.ToTable("snapshots");

            builder.HasKey(x => x.PkId);
        }
    }
}
