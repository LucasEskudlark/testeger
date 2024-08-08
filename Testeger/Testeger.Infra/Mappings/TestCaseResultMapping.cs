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

        builder.Property(t => t.Number)
            .HasColumnName("Number")
            .HasColumnType("int")
            .IsRequired();

        builder.Property(tr => tr.TestCaseId)
            .HasColumnName("TestCaseId")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(tr => tr.ActualResult)
            .HasColumnName("ActualResult")
            .HasMaxLength(700);

        builder.Property(t => t.IsSuccess)
            .HasColumnName("IsSuccess")
            .HasColumnType("bit");

        builder.Property(e => e.ElapsedTime)
            .HasColumnName("ElapsedTime")
            .HasColumnType("time");

        builder.Property(e => e.AmountOfTimeSpentToTest)
            .HasColumnName("AmountOfTimeSpentToTest")
            .HasColumnType("time");

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
