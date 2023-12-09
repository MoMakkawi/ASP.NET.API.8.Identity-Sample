using ASP.NET8.Identity.Identity;
using ASP.NET8.Identity.Models.Helpers;

using FastEndpoints;


using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET8.Identity.EndPoints.UserManagement.Register;

public sealed class ConsumerRegisterEndPoint : Endpoint<RegisterModel, Result<RegisterResponse>>
{
    public UserManager<ApplicationUser> UserManager { get; set; } = default!;
    public const string registerRoute = "api/v1/register";
    public override void Configure()
    {
        Post(registerRoute);
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterModel model, CancellationToken ct)
    {
        ApplicationUser appUser = MapToApplicationUser(model);

        IdentityResult identityResult = await UserManager.CreateAsync(appUser, model.Password);
 
        Result<RegisterResponse> registerResponse;

        if (!identityResult.Succeeded)
            registerResponse = Result<RegisterResponse>.Fail(identityResult.Errors);
        
        else
        {
            appUser = await GetUserByEmailAsync(model.Email, ct);

            var response = new RegisterResponse(Guid.Parse(appUser.Id));
            registerResponse = Result<RegisterResponse>.Success(response);
        }

        await SendAsync(registerResponse, cancellation: ct);
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
        => await UserManager.Users
            .FirstAsync(u => u.Email == email, cancellationToken: ct);

}
