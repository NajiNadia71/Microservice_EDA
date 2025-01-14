using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Advertisement_Company.Entities
{

    public class Ad
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ProductionId { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public string Text { get; set; }

        public Production Production { get; set; }

    }
    public class AdConfiguration : IEntityTypeConfiguration<Ad>
    {
        public void Configure(EntityTypeBuilder<Ad> builder)
        {
            builder.ToTable("Ads", "dbo");
            builder.HasKey(x => x.Id).HasName("PK_Ads");
            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Title).HasColumnName(@"Title").HasColumnType("nvarchar(50)").IsRequired().HasMaxLength(50);
            builder.Property(x => x.ProductionId).HasColumnName(@"ProductionId").HasColumnType("int").IsRequired();
            builder.Property(x => x.CreateDate).HasColumnName(@"CreateDate").HasColumnType("datetimeoffset").IsRequired();
            builder.Property(x => x.Text).HasColumnName(@"Text").HasColumnType("nvarchar(50)").IsRequired().HasMaxLength(50);
            //Foregin key
            builder.HasOne(x => x.Production).WithMany().HasForeignKey(x => x.ProductionId).OnDelete(DeleteBehavior.Restrict).HasConstraintName("FK_Productions_Ads");

        }
    }

}
