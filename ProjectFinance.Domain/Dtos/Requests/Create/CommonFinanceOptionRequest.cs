namespace ProjectFinance.Domain.Dtos.Requests;

public class CommonFinanceOptionRequest
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public string? OptionType { get; set; }
}