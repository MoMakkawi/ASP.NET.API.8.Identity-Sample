using ASP.NET8.Identity.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET8.Identity.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) 
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Consumer> Consumers { get; set; }
    public DbSet<Provider> Providers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    { 
        base.OnModelCreating(builder);
    }


}
