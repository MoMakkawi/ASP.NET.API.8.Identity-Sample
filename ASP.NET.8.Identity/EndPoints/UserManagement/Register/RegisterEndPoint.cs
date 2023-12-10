using ASP.NET8.Identity.Identity;
using ASP.NET8.Identity.Models.Helpers;

using FastEndpoints;


using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET8.Identity.EndPoints.UserManagement.Register;

public sealed class ConsumerRegisterEndPoint (UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    : Endpoint<RegisterModel, Result<RegisterResponse>>
{
    private readonly UserManager<ApplicationUser> userManager = userManager;
    private readonly RoleManager<IdentityRole> roleManager = roleManager;
    public const string registerRoute = "api/v1/register";
    public override void Configure()
    {
        Post(registerRoute);
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterModel model, CancellationToken ct)
    {
        ApplicationUser appUser = MapToApplicationUser(model);

        IdentityResult identityResult = await userManager.CreateAsync(appUser, model.Password);
 
        Result<RegisterResponse> registerResponse;

        if (!identityResult.Succeeded)
            registerResponse = Result<RegisterResponse>.Fail(identityResult.Errors);
        else
        {
            appUser = await GetUserByEmailAsync(model.Email, ct);

            identityResult = await userManager.AddToRoleAsync(appUser, Role.Provider);

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
            LocationText = model.LocationText,
            Role = model.Role,
        };
    }

    private async Task<ApplicationUser> GetUserByEmailAsync(string email, CancellationToken ct) 
        => await userManager.Users
            .FirstAsync(u => u.Email == email, cancellationToken: ct);

}
