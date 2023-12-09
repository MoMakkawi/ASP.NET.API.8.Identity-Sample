using ASP.NET8.Identity.Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET8.Identity.Identity;

internal static class Role
{
    internal const string Admin = nameof(Admin);
    internal const string AdminId = "fa485500-3f43-4756-a45e-a9c4fa475789";
    private const string AdminConcurrencyStamp = "c23d091c-e477-4647-a864-e2d8914ed52f";

    internal const string Provider = nameof(Provider);
    internal const string ProviderId = "09889c40-5e95-4522-99f7-0bfab29fbfea";
    private const string ProviderConcurrencyStamp = "ff685fef-8349-425f-b9d5-a3bcc3eb4494";

    internal const string Consumer = nameof(Consumer);
    internal const string ConsumerId = "64410517-6b14-4833-886a-0f949e3c5955";
    private const string ConsumerConcurrencyStamp = "93464eb3-c99b-4c63-9070-8d3b3fd14733";


    internal static async Task SeedRoles(AppDbContext dbContext)
    {

        var roles = dbContext.Set<IdentityRole>();


        if (!roles.IsRoleExist(Role.Admin))
        {
            var AdminRole = CreateRole(
                Role.AdminId,
                Role.Admin,
                Role.AdminConcurrencyStamp);

            await roles.AddAsync(AdminRole);
        }

        if (!roles.IsRoleExist(Role.Provider))
        {
            var ProviderRole = CreateRole(
                Role.ProviderId,
                Role.Provider,
                Role.ProviderConcurrencyStamp);

            await roles.AddAsync(ProviderRole);
        }

        if (!roles.IsRoleExist(Role.Consumer))
        {
            var ConsumerRole = CreateRole(
                Role.ConsumerId,
                Role.Consumer,
                Role.ConsumerConcurrencyStamp);

            await roles.AddAsync(ConsumerRole);
        }

        await dbContext.SaveChangesAsync();
    }

    private static bool IsRoleExist(this DbSet<IdentityRole> roles, string roleName)
        => roles
        .ToList()
        .Exists(x => x.Name == roleName);

    private static IdentityRole CreateRole(string id, string name, string concurrencyStamp)
    => new()
    {
        Id = id,
        Name = name,
        ConcurrencyStamp = concurrencyStamp,
        NormalizedName = name.ToUpper()
    };
}