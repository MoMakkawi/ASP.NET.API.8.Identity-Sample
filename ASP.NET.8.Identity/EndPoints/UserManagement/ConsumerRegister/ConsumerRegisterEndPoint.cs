using ASP.NET8.Identity.Data;
using ASP.NET8.Identity.Models.Helpers;

using Demo.API.Identity;

using FastEndpoints;


using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET8.Identity.EndPoints.UserManagement.ConsumerRegister;

public sealed class ConsumerRegisterEndPoint(AppDbContext dbContext)
    : Endpoint<ConsumerRegisterModel, Result<ConsumerRegisterResponse>>
{
    private readonly AppDbContext _dbContext = dbContext;
    public UserManager<ApplicationUser> UserManager { get; set; } = default!;

    public const string consumerRegisterRoute = $"api/v1/register/{Identity.Role.Consumer}";
    public override void Configure()
    {
        Post(consumerRegisterRoute);
        AllowAnonymous();
    }
    public override async Task HandleAsync(ConsumerRegisterModel model, CancellationToken ct)
    {
        Consumer consumer = MapToConsumer(model);

        IdentityResult identityResult = await UserManager.CreateAsync(consumer, model.Password);

        Result<ConsumerRegisterResponse> registerResponse;

        if (!identityResult.Succeeded)
            registerResponse = Result<ConsumerRegisterResponse>.Fail(identityResult.Errors);

        else
        {
            consumer = await GetUserByEmailAsync(model.Email, ct);

            var response = new ConsumerRegisterResponse(Guid.Parse(consumer.Id));
            registerResponse = Result<ConsumerRegisterResponse>.Success(response);
        }

        await SendAsync(registerResponse, cancellation: ct);
    }

    private Consumer MapToConsumer(ConsumerRegisterModel model)
    {
        return new Consumer
        {
            Email = model.Email,
            UserName = model.Email,
            FullName = model.FullName,
            LocationText = model.LocationText,
            Nationality = model.Nationality,
            CardNumber = model.CardNumber,
            CardExpDate = model.CardExpDate,
            Role = Identity.Role.Consumer

        };
    }

    private async Task<Consumer> GetUserByEmailAsync(string email, CancellationToken ct)
        => await _dbContext.Consumers
            .FirstAsync(u => u.Email == email, cancellationToken: ct);

}
