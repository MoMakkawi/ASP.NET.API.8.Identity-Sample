namespace ASP.NET8.Identity.EndPoints.UserManagement.Register;

public sealed record RegisterModel(
    string FullName,
    string LocationText,
    string Password,
    string Email);