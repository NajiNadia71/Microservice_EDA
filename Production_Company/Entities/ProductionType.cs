using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Production_Company.Entities
{
   
    public class ProductionType
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Production> Productions { get; set; } = new List<Production>();

    }
    public class ProductionTypeConfiguration : IEntityTypeConfiguration<ProductionType>
    {
        public void Configure(EntityTypeBuilder<ProductionType> builder)
        {
            builder.ToTable("ProductionTypes", "dbo");
            builder.HasKey(x => x.Id).HasName("PK_ProductionTypes");
            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Title).HasColumnName(@"Title").HasColumnType("nvarchar(50)").IsRequired().HasMaxLength(50);
        }
    }

}