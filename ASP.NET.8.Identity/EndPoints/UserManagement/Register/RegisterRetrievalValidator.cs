using System.Text.RegularExpressions;

using FastEndpoints;

using FluentValidation;

namespace ASP.NET8.Identity.EndPoints.UserManagement.Register;

public sealed class RegisterRetrievalValidator : Validator<RegisterModel>
{
    public RegisterRetrievalValidator()
    {
        RuleFor(x => x.Password)
            .NotNull().WithMessage("a password is null!")
            .NotEmpty().WithMessage("a password is required!")
            .Must(HasValidPassword)
            .WithMessage("The password must contain symbols, numbers, uppercase and lowercase letters");

        RuleFor(x => x.Email)
            .NotNull().WithMessage("email address is null!")
            .NotEmpty().WithMessage("email address is required!")
            .EmailAddress().WithMessage("the format of your email address is wrong!");

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("your name is required!")
            .MinimumLength(5).WithMessage("your name is too short!")
            .MaximumLength(50).WithMessage("your name is too long!");

    //    RuleFor(x => x.Role)
    //.IsInEnum().WithMessage("role not found")
    //.NotEqual(Identity.Role.Admin).WithMessage("you can not register as admin.");
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
