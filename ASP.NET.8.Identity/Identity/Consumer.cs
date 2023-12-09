namespace ASP.NET8.Identity.Identity;

public class Consumer : ApplicationUser
{
    public required string Nationality { get; set; }
    public required string CardNumber { get; set; }
    public required DateOnly CardExpDate { get; set; }
}
