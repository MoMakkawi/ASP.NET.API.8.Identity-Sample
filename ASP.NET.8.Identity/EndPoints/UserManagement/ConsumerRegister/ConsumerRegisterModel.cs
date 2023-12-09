using System.Net.Mail;

namespace ASP.NET8.Identity.EndPoints.UserManagement.ConsumerRegister;

public sealed record ConsumerRegisterModel(
    string FullName,
    string LocationText,
    string Password,
    string Email,
    string Nationality,
    string CardNumber,
    DateOnly CardExpDate)
{
    public string UserName { get; } = new MailAddress(Email).User;
    public string Role { get; } = Identity.Role.Consumer;
};