using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.G04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Repository.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Name).HasMaxLength(maxLength: 200).IsRequired();
            builder.Property(P => P.PictureUrl).IsRequired(required: true);

            builder.Property(P => P.Price).HasColumnType(typeName: "decimal(18,2)");

            builder.HasOne(P => P.Brand)
                .WithMany()
                .HasForeignKey(P => P.BrandId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(P => P.Type)
                .WithMany()
                .HasForeignKey(P => P.TypeId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(P => P.BrandId).IsRequired(required: false);
            builder.Property(P => P.TypeId).IsRequired(required: false);


        }
    }
}
