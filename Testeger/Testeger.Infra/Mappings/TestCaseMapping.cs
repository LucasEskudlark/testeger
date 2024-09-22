using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Testeger.Domain.Models.Entities;

namespace Testeger.Infra.Mappings;

public class TestCaseMapping : IEntityTypeConfiguration<TestCase>
{
    public void Configure(EntityTypeBuilder<TestCase> builder)
    {
        builder.ToTable("TestCase");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.TestRequestId)
            .HasColumnName("TestRequestId")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(t => t.ProjectId)
            .HasColumnName("ProjectId")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(t => t.CreatedByUserId)
            .HasColumnName("CreatedByUserId")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(t => t.Title)
            .HasColumnName("Title")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(t => t.CreatedDate)
            .HasColumnName("CreatedDate")
            .HasColumnType("datetime")
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();

        builder.Property(t => t.CompletedDate)
            .HasColumnName("CompletedDate")
            .HasColumnType("datetime");

        builder.Property(t => t.Status)
            .HasColumnName("Status")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(t => t.ScheduledDate)
            .HasColumnName("ScheduledDate")
            .HasColumnType("datetime");

        builder
            .OwnsOne(t => t.Details, b => b.ApplyTestCaseDetailsMapping())
            .OwnsMany(t => t.History, b => b.ApplyTestCaseHistoryMapping());

        builder.HasOne(t => t.TestRequest)
            .WithMany(tr => tr.TestCases)
            .HasForeignKey(t => t.TestRequestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.Results)
            .WithOne(tr => tr.TestCase)
            .HasForeignKey(tr => tr.TestCaseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
