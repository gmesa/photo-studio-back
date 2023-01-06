using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoStudio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Infrastructure.Data.Configurations
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder.Property(entity=> entity.Id)
                   .HasColumnName("MaterialId")
                   .ValueGeneratedOnAdd();

            builder.Property(entity => entity.MaterialName)
                   .IsRequired()
                   .HasColumnName("MaterialName")
                   .HasMaxLength(200);

            builder.HasIndex(entity => entity.MaterialName)
                    .IsUnique()
                    .HasDatabaseName("IX_Material")
                    .IsUnique()
                    .IsDescending(false);

            builder.Property(entity => entity.RowVersion)
                    .HasColumnType("Timestamp")
                    .IsRowVersion();

            builder.ToTable("Material");
        }
    }
}
