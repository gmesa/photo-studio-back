using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoStudio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Infrastructure.Data.Configurations
{
    public class SizeConfiguration : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(entity=> entity.Id)
                   .HasColumnName("SizeId")
                   .ValueGeneratedOnAdd();

            builder.Property(entity => entity.SizeName)
                   .HasColumnName("Size")
                   .HasMaxLength(100);

            builder.HasIndex(x => x.SizeName)
                    .HasDatabaseName("IX_Size_Unique")
                    .IsUnique().IsDescending(false);

            builder.Property(entity => entity.RowVersion)
                   .HasColumnType("Timestamp")
                   .IsRowVersion();   
            
            builder.ToTable("Size");

        }
    }
}
