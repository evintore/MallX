using Core.Entities;
using Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(x => x.PkId);

            builder.Property(x => x.FullName)
                .HasMaxLength(100)
                .HasColumnType("varchar")
                .HasColumnName("fullName")
                .IsRequired();

            builder.Property(x => x.PkId)
                .HasMaxLength(100)
                .HasColumnType("int")
                .HasColumnName("pkid")
                .IsRequired();

            builder.Property(x => x.Mail)
                .HasMaxLength(100)
                .HasColumnType("varchar")
                .HasColumnName("mail")
                .IsRequired();

            builder.Property(x => x.Password)
                .HasMaxLength(100)
                .HasColumnType("varchar")
                .HasColumnName("password")
                .IsRequired();

            builder.Property(x => x.Status)
                .HasDefaultValue(UserStatus.Active)
                .IsRequired();
        }
    }
}
