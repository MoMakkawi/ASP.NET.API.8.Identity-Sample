using ASP.NET8.Identity.Models.Helpers;

using Demo.API.Identity;

using FastEndpoints;


using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET8.Identity.EndPoints.UserManagement.ConsumerRegister;

public sealed class ConsumerRegisterEndPoint : Endpoint<ConsumerRegisterModel, Result<ConsumerRegisterResponse>>
{
    public UserManager<ApplicationUser> UserManager { get; set; } = default!;
    public readonly string consumerRegisterRoute = $"api/v1/register/{Identity.Role.Consumer}";
    public override void Configure()
    {
        Post(consumerRegisterRoute);
        AllowAnonymous();
    }
    public override async Task HandleAsync(ConsumerRegisterModel model, CancellationToken ct)
    {
        ApplicationUser appUser = MapToApplicationUser(model);

        IdentityResult identityResult = await UserManager.CreateAsync(appUser, model.Password);

        Result<ConsumerRegisterResponse> registerResponse;

        if (!identityResult.Succeeded)
            registerResponse = Result<ConsumerRegisterResponse>.Fail(identityResult.Errors);

        else
        {
            appUser = await GetUserByEmailAsync(model.Email, ct);

            var response = new ConsumerRegisterResponse(Guid.Parse(appUser.Id));
            registerResponse = Result<ConsumerRegisterResponse>.Success(response);
        }

        await SendAsync(registerResponse, cancellation: ct);
    }

    private ApplicationUser MapToApplicationUser(ConsumerRegisterModel model)
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
