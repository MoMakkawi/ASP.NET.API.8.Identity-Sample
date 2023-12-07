using ASP.NET8.Identity.Data;

using Demo.API.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Context") ??
    throw new InvalidOperationException("Connection string 'Context' not found.")));

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme);
    //.AddIdentityCookies();

builder.Services.AddAuthorizationBuilder();

builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
    .AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapIdentityApi<ApplicationUser>();

app.Run();


