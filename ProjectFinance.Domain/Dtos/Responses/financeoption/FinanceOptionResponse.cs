namespace ProjectFinance.Domain.Dtos.Responses.financeoption;

public class FinanceOptionResponse
{
    public int Id { get; set; }
    
    public string? Description { get; set; }
    public string? OptionType { get; set; }
    
    public int? BankId { get; set; }
    public string? BankName { get; set; }
}