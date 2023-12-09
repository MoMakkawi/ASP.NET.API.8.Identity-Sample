namespace ASP.NET8.Identity.EndPoints.UserManagement.ConsumerRegister;

public sealed record ConsumerRegisterModel(
    string FullName,
    string LocationText,
    string Password,
    string Email,
    string Nationality,
    string CardNumber,
    DateOnly CardExpDate);