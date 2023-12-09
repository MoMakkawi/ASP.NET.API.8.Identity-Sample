using ASP.NET8.Identity.Data;
using ASP.NET8.Identity.Identity;
using ASP.NET8.Identity.Models.Helpers;

using FastEndpoints;


using Microsoft.AspNetCore.Identity;

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
        var mapper = new ConsumerRegisterMapper();

        Consumer consumer = mapper.Map(model);

        var registerResponse = await ConsumerRegisterHelpers
            .CreateConsumerAsync(consumer,
                                 model.Password,
                                 model.Email);

        registerResponse = await ConsumerRegisterHelpers
            .AssignConsumerRole(consumer,
                                 model.Email);


        await SendAsync(registerResponse, cancellation: ct);
    }



}
