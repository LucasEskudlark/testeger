using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Testeger.Domain.Models.Entities;

namespace Testeger.Infra.Mappings;

public class InvitationMapping : IEntityTypeConfiguration<Invitation>
{
    public void Configure(EntityTypeBuilder<Invitation> builder)
    {
        builder.ToTable("Invitations");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.UserId)
            .HasColumnName(nameof(Invitation.UserId))
            .HasMaxLength(36)
            .IsRequired(false);

        builder.Property(i => i.ProjectId)
            .HasColumnName(nameof(Invitation.ProjectId))
            .HasMaxLength(36)
            .IsRequired(false);

        builder.Property(i => i.Email)
            .HasColumnName(nameof(Invitation.Email))
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(i => i.InvitationToken)
            .HasColumnName(nameof(Invitation.InvitationToken))
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(i => i.IsConfirmed)
            .HasColumnName(nameof(Invitation.IsConfirmed))
            .HasColumnType("bit")
            .IsRequired();

        builder.Property(p => p.ExpiryDate)
            .HasColumnName(nameof(Invitation.ExpiryDate))
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(p => p.SentDate)
            .HasColumnName(nameof(Invitation.SentDate))
            .HasColumnType("datetime")
            .IsRequired();

        builder.HasOne(i => i.User)
            .WithMany(u => u.Invitations)
            .HasForeignKey(i => i.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(i => i.Project)
            .WithMany(p => p.Invitations)
            .HasForeignKey(i => i.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(i => i.InvitationToken)
            .IsUnique();

        builder.HasIndex(i => i.Email);

        builder.HasIndex(i => i.UserId);
    }
}
