using ASP.NET8.Identity.Data;
using ASP.NET8.Identity.Identity;

using FastEndpoints;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFastEndpoints();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Context") ??
    throw new InvalidOperationException("Connection string 'Context' not found.")));

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme);
    //.AddIdentityCookies();

builder.Services.AddAuthorizationBuilder();

builder.Services
    .AddIdentityApiEndpoints<ApplicationUser>()
    .AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

#region Seeding Roles
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await Role.SeedRoles(dbContext);
    // use context
}

#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.MapIdentityApi<ApplicationUser>();

app.UseFastEndpoints();

app.Run();


