using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Testeger.Domain.Models.Entities;

namespace Testeger.Infra.Mappings;

public class ProjectMapping : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Project");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasColumnName(nameof(Project.Name))
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.CreatedByUserId)
            .HasColumnName(nameof(Project.CreatedByUserId))
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(p => p.CreatedDate)
            .HasColumnName(nameof(Project.CreatedDate))
            .HasColumnType("datetime")
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();

        builder.HasMany(p => p.TestRequests)
            .WithOne(tr => tr.Project)
            .HasForeignKey(tr => tr.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
