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
    public class PhotoBookConfiguration : IEntityTypeConfiguration<PhotoBook>
    {
        public void Configure(EntityTypeBuilder<PhotoBook> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(entity => entity.Id)
                   .HasColumnName("PhotoBookId")
                   .ValueGeneratedOnAdd();

            builder.Property(entity => entity.MaterialId)
                   .HasColumnName("MaterialId")
                   .IsRequired();

            builder.Property(entity => entity.SizeId)
                   .HasColumnName("SizeId")
                   .IsRequired();

            builder.Property(entity => entity.PortadaPrice)
                   .HasColumnType("money")
                   .HasColumnName("PortadaPrice")
                   .IsRequired();

            builder.Property(entity => entity.PriceByPage)
                   .HasColumnType("money")
                   .HasColumnName("PriceByPage")
                   .IsRequired();

            builder.HasOne(entity => entity.Size)
                   .WithMany(s => s.PhotoBooks)
                   .HasForeignKey(e => e.SizeId)
                   .HasConstraintName("FK_PhotoBooks_Sizes")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(entity => entity.Material)
                   .WithMany(m => m.PhotoBooks)
                   .HasForeignKey(e => e.MaterialId)
                   .HasConstraintName("FK_PhotoBooks_Material").
                   OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(entity => new { entity.SizeId, entity.MaterialId })
                  .HasDatabaseName("IX_PhotoBook_MaterialId_SizeId_Unique")
                  .IsUnique();

            builder.Property(entity => entity.RowVersion)
                   .HasColumnType("Timestamp")
                   .IsRowVersion();

            builder.ToTable("PhotoBook");

        }
    }
}
