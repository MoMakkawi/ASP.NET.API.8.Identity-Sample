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
<<<<<<<<< Temporary merge branch 1
        //builder.Entity<Consumer>()
        //    .HasOne(e => e.ApplicationUser)
        //    .WithOne()
        //    .HasForeignKey<ApplicationUser>()
        //    .IsRequired();

        //builder.Entity<Provider>()
        //    .HasMany(e => e.ApplicationUsers)
        //    .WithOne()
        //    .IsRequired();
=========
>>>>>>>>> Temporary merge branch 2

        base.OnModelCreating(builder);
    }


}
