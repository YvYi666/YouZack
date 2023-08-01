using MediaEncoder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaEncoder.Infrastructure.Configs
{
    class EncodingItemConfig : IEntityTypeConfiguration<EncodingItem>
    {
        public void Configure(EntityTypeBuilder<EncodingItem> builder)
        {
            builder.ToTable("T_ME_EncodingItems");
            //todo:id需要非聚集索引。
            builder.HasKey(e => e.Id).IsClustered(false);
            //todo:复合索引。
            builder.HasIndex(e => new { e.FileSHA256Hash, e.FileSizeInBytes });//经常要按照这两个列进行查询，因此把它们两个组成复合索引，提高查询效率。
            builder.Property(e => e.Name).HasMaxLength(256);
            builder.Property(e => e.FileSHA256Hash).HasMaxLength(64).IsUnicode(false);
            builder.Property(e => e.OutputFormat).HasMaxLength(10).IsUnicode(false);
            builder.Property(e => e.Status).HasConversion<string>().HasMaxLength(10);
        }
    }
}
