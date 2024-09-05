using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Testeger.Domain.Models.Entities;
using Testeger.Infra.Mappings;

namespace Testeger.Infra.Context;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Project> Projects { get; set; }
    public DbSet<TestRequest> TestRequests { get; set; }
    public DbSet<TestCase> TestCases { get; set; }
    public DbSet<TestCaseResult> TestCaseResults { get; set; }
    public DbSet<Image> Images { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new ProjectMapping())
            .ApplyConfiguration(new TestRequestMapping())
            .ApplyConfiguration(new TestCaseMapping())
            .ApplyConfiguration(new TestCaseResultMapping())
            .ApplyConfiguration(new ImageMapping());

        base.OnModelCreating(modelBuilder);
    }
}
