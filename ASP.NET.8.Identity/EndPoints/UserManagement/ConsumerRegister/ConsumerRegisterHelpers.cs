using System.Text.RegularExpressions;

using ASP.NET8.Identity.Data;
using ASP.NET8.Identity.Identity;
using ASP.NET8.Identity.Models.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET8.Identity.EndPoints.UserManagement.ConsumerRegister;

internal class ConsumerRegisterHelpers
{
    private static AppDbContext _dbContext;
    public static UserManager<ApplicationUser> UserManager { get; set; } = default!;

    internal static async Task<Result<ConsumerRegisterResponse>> AssignConsumerRole(
    Consumer consumer,
    string email)
    {
        IdentityResult identityResult = await UserManager.AddToRoleAsync(consumer, Identity.Role.Consumer);
        return await CheckIdentityResultAsync(consumer, email, identityResult);
    }

    internal static async Task<Result<ConsumerRegisterResponse>> CreateConsumerAsync(
        Consumer consumer,
        string password,
        string email)
    {
        IdentityResult identityResult = await UserManager.CreateAsync(consumer, password);
        return await CheckIdentityResultAsync(consumer, email, identityResult);
    }

    internal static async Task<Result<ConsumerRegisterResponse>> CheckIdentityResultAsync(
        Consumer consumer,
        string email,
        IdentityResult identityResult)
    {
        Result<ConsumerRegisterResponse> registerResponse;

        if (!identityResult.Succeeded)
            registerResponse = Result<ConsumerRegisterResponse>.Fail(identityResult.Errors);

        else
        {
            consumer = await GetUserByEmailAsync(email);

            var response = new ConsumerRegisterResponse(Guid.Parse(consumer.Id));
            registerResponse = Result<ConsumerRegisterResponse>.Success(response);
        }

        return registerResponse;
    }

    internal static async Task<Consumer> GetUserByEmailAsync(string email)
        => await _dbContext.Consumers.FirstAsync(u => u.Email == email);

    internal static bool HasValidPassword(string? pw)
    {
        Regex lowercase = new("[a-z]+");
        Regex uppercase = new("[A-Z]+");
        Regex digit = new("(\\d)+");
        Regex symbol = new("(\\W)+");

        return pw is not null &&
            lowercase.IsMatch(pw) &&
            uppercase.IsMatch(pw) &&
            digit.IsMatch(pw) &&
            symbol.IsMatch(pw);
    }
}
