using ASP.NET8.Identity.Models;

using Demo.API.Identity;
using FastEndpoints;


using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET8.Identity.EndPoints.UserManagement.Register;

public sealed class RegisterEndPoint : Endpoint<RegisterModel, Result<RegisterResponse>>
{
    public UserManager<ApplicationUser> userManager { get; set; } = default!;
    public override void Configure()
    {
        Post("api/v1/register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterModel model, CancellationToken ct)
    {
        var appUser = MapToApplicationUser(model);

        var identityResult = await userManager.CreateAsync(appUser, model.Password);

        if (!identityResult.Succeeded)
        {
            var failResult = Result<RegisterResponse>.Fail(identityResult.Errors);
            await SendAsync(failResult, cancellation: ct);
            return;
        }

        appUser = await GetUserByEmailAsync(model.Email, ct);

        var response = new RegisterResponse(Guid.Parse(appUser.Id));
        var successResult = Result<RegisterResponse>.Success(response);

        await SendAsync(successResult, cancellation: ct);
    }

    private ApplicationUser MapToApplicationUser(RegisterModel model)
    {
        return new ApplicationUser
        {
            Email = model.Email,
            UserName = model.Email,
            FullName = model.FullName,
            LocationText = model.LocationText
        };
    }

    private async Task<ApplicationUser> GetUserByEmailAsync(string email, CancellationToken ct) 
        => await userManager.Users
            .FirstAsync(u => u.Email == email, cancellationToken: ct);

}
