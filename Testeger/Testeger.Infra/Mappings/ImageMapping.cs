using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Testeger.Domain.Models.Entities;

namespace Testeger.Infra.Mappings;

public class ImageMapping : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.ToTable("Image");

        builder.HasKey(img => img.Id);

        builder.Property(img => img.FilePath)
            .HasMaxLength(2048)
            .IsRequired();

        builder.Property(img => img.FileName)
            .HasMaxLength(500)
            .IsRequired();

        builder.HasOne(img => img.TestCaseResult)
            .WithMany(tcr => tcr.Images)
            .HasForeignKey(img => img.TestCaseResultId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
