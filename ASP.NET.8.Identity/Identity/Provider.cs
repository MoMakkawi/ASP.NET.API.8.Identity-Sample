namespace Demo.API.Identity;

public class Provider : ApplicationUser
{
    public required string BusinessName { get; set; }
    public required string BankName { get; set; }
    public required string IBAN { get; set; }
    public required string SwiftCode { get; set; }
    public required string TradeLicenseNumber { get; set; }
    public required DateOnly TradeExpDate { get; set; }
    public required ProviderType Type { get; set; }
    public required Guid CreatorId { get; set; } = Guid.Empty;

}
