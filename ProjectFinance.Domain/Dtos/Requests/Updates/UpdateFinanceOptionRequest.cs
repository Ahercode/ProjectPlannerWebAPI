namespace ProjectFinance.Domain.Dtos.Requests.Updates;

public class UpdateFinanceOptionRequest
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public string? OptionType { get; set; }

    public int? BankId { get; set; }
}