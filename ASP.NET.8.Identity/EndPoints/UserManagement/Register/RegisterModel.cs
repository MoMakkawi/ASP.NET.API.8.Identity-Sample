using ASP.NET8.Identity.Identity;

using Microsoft.AspNetCore.Identity;

namespace ASP.NET8.Identity.EndPoints.UserManagement.Register;

public sealed record RegisterModel(
    string FullName,
    string LocationText,
    string Password,
    string Email)
{
    public string Role { get; } = Identity.Role.Provider;
};