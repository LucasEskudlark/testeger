using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Testeger.Domain.Models.Entities;

namespace Testeger.Infra.Mappings;

public class TestRequestMapping : IEntityTypeConfiguration<TestRequest>
{
    public void Configure(EntityTypeBuilder<TestRequest> builder)
    {
        builder.ToTable("TestRequest");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Number)
            .HasColumnName("Number")
            .HasColumnType("int")
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

        builder.Property(t => t.Description)
            .HasColumnName("Description")
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(t => t.StoryLink)
            .HasColumnName("StoryLink")
            .HasMaxLength(512)
            .IsRequired();

        builder.Property(t => t.UserAssignedId)
            .HasColumnName("UserAssignedId")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(t => t.Status)
            .HasColumnName("Status")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(t => t.PriorityLevel)
            .HasColumnName("PriorityLevel")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(t => t.DueDate)
            .HasColumnName("DueDate")
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(t => t.CreatedDate)
            .HasColumnName("CreatedDate")
            .HasColumnType("datetime")
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();

        builder.Property(t => t.CompletedDate)
            .HasColumnName("CompletedDate")
            .HasColumnType("datetime")
            .IsRequired();

        builder.OwnsMany(t => t.History, b => b.ApplyTestRequestHistoryMapping());

        builder.HasOne(t => t.Project)
          .WithMany(p => p.TestRequests)
          .HasForeignKey(t => t.ProjectId)
          .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.TestCases)
            .WithOne(tc => tc.TestRequest)
            .HasForeignKey(tc => tc.TestRequestId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
