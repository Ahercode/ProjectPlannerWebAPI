namespace ProjectFinance.Domain.Dtos.Requests;

public class FinanceOptionScheduleCreateRequest
{
    
    public int? FinanceOptionId { get; set; }
    
    public decimal? Cost { get; set; }

    public decimal? Repayment { get; set; }

    public decimal? Disbursement { get; set; }

    public DateOnly? Date { get; set; }
}