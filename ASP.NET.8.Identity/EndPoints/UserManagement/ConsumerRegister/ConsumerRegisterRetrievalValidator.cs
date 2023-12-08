using System.Text.RegularExpressions;

using FastEndpoints;

using FluentValidation;

namespace ASP.NET8.Identity.EndPoints.UserManagement.ConsumerRegister;

public sealed class ConsumerRegisterRetrievalValidator : Validator<ConsumerRegisterModel>
{
    public ConsumerRegisterRetrievalValidator()
    {
        RuleFor(x => x.Password)
            .NotNull().WithMessage("Password is null!")
            .NotEmpty().WithMessage("Password is required!")
            .Must(HasValidPassword)
            .WithMessage("The password must contain symbols, numbers, uppercase and lowercase letters");

        RuleFor(x => x.Email)
            .NotNull().WithMessage("Email address is null!")
            .NotEmpty().WithMessage("Email address is required!")
            .EmailAddress().WithMessage("the format of your email address is wrong!");

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full Name is required!")
            .MinimumLength(5).WithMessage("Full Name is too short!")
            .MaximumLength(50).WithMessage("Full Name is too long!");

        RuleFor(x => x.LocationText)
            .NotEmpty().WithMessage("Location Text is required!")
            .NotEmpty().WithMessage("Location Text is required!");
    }

    private bool HasValidPassword(string? pw)
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
