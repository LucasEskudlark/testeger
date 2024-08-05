using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Testeger.Domain.Models.ValueObjects;

namespace Testeger.Infra.Mappings;

public static class ValueObjectsMappingHelper
{
    public static void ApplyTestCaseHistoryMapping<T>(this OwnedNavigationBuilder<T, TestCaseHistory> builder)
        where T : class
    {
        builder.ToTable("TestCaseHistory");

        builder.Property(th => th.TestCaseId)
            .HasColumnName("TestCaseId")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(th => th.ChangedByUserId)
            .HasColumnName("ChangedByUserId")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(th => th.OldStatus)
            .HasColumnName("OldStatus")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(th => th.NewStatus)
            .HasColumnName("NewStatus")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(th => th.ChangedDate)
            .HasColumnName("ChangedDate")
            .HasColumnType("datetime")
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();
    }

    public static void ApplyTestRequestHistoryMapping<T>(this OwnedNavigationBuilder<T, TestRequestHistory> builder)
        where T : class
    {
        builder.ToTable("TestRequestHistory");

        builder.Property(th => th.TestRequestId)
            .HasColumnName("TestRequestId")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(th => th.ChangedByUserId)
            .HasColumnName("ChangedByUserId")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(th => th.OldStatus)
            .HasColumnName("OldStatus")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(th => th.NewStatus)
            .HasColumnName("NewStatus")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(th => th.ChangedDate)
            .HasColumnName("ChangedDate")
            .HasColumnType("datetime")
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();
    }

    public static void ApplyTestCaseDetailsMapping<T>(this OwnedNavigationBuilder<T, TestCaseDetails> builder)
        where T : class
    {
        builder.Property(td => td.Description)
            .HasColumnName("Description")
            .HasMaxLength(1500)
            .IsRequired();

        builder.Property(td => td.PreConditions)
            .HasColumnName("PreConditions")
            .HasMaxLength(700)
            .IsRequired();

        builder.Property(td => td.Steps)
            .HasColumnName("Steps")
            .HasMaxLength(700)
            .IsRequired();

        builder.Property(td => td.ExpectedResult)
            .HasColumnName("ExpectedResult")
            .HasMaxLength(700)
            .IsRequired();

        builder.Property(td => td.Environment)
            .HasColumnName("Environment")
            .HasMaxLength(50)
            .IsRequired();
    }
}
