namespace ASP.NET8.Identity.Models.Helpers;

public record Result<T>(
    bool IsSuccess,
    IEnumerable<dynamic> Errors,
    T Data)
{
    public static Result<T> Success(T data) => new(true, null!, data);
    public static Result<T> Fail(IEnumerable<dynamic> errors)
        => new(false, errors, default!);
}
