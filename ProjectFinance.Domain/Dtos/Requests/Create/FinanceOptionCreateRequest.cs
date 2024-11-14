namespace ProjectFinance.Domain.Dtos.Requests;

public class FinanceOptionCreateRequest
{

    public string? Description { get; set; }
    public string? OptionType { get; set; }
    public int? BankId { get; set; }
}