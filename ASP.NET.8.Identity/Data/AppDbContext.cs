using Demo.API.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET8.Identity.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Consumer> Consumers { get; set; }
    public DbSet<Provider> Providers { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    { 
        base.OnModelCreating(builder);
    }


}
