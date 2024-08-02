using Microsoft.EntityFrameworkCore;

namespace Testeger.Infra.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

}
