using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Testeger.Domain.Models.Entities;

namespace Testeger.Infra.Mappings;

public class TestCaseResultMapping : IEntityTypeConfiguration<TestCaseResult>
{
    public void Configure(EntityTypeBuilder<TestCaseResult> builder)
    {
        builder.ToTable("TestCaseResult");

        builder.HasKey(tr => tr.Id);

        builder.Property(tr => tr.TestCaseId)
            .HasColumnName("TestCaseId")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(tr => tr.ActualResult)
            .HasColumnName("ActualResult")
            .HasMaxLength(700)
            .IsRequired();

        builder.Property(t => t.IsSuccess)
            .HasColumnName("IsSuccess")
            .HasColumnType("bit")
            .IsRequired();

        builder.Property(e => e.SavedTimesJson)
            .HasColumnName("SavedTimes")
            .HasColumnType("json")
            .IsRequired();

        builder.Property(e => e.ElapsedTime)
            .HasColumnName("ElapsedTime")
            .HasColumnType("time")
            .IsRequired();

        builder.Property(e => e.AmountOfTimeSpentToTest)
            .HasColumnName("AmountOfTimeSpentToTest")
            .HasColumnType("time")
            .IsRequired();

        builder.HasOne(tr => tr.TestCase)
            .WithMany(tc => tc.Results)
            .HasForeignKey(tr => tr.TestCaseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(tr => tr.Images)
            .WithOne(i => i.TestCaseResult)
            .HasForeignKey(i => i.TestCaseResultId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
