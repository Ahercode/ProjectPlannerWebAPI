namespace ProjectFinance.Domain.Dtos.Responses.financeoption;

public class FinanceOptionScheduleResponse
{
    public int Id { get; set; }

    public int? FinanceOptionId { get; set; }
    
    public string? FinanceOptionName { get; set; }
    
    public decimal? Cost { get; set; }
    
    public decimal? Repayment { get; set; }
    
    public decimal? Disbursement { get; set; }

    public DateOnly? Date { get; set; }
}