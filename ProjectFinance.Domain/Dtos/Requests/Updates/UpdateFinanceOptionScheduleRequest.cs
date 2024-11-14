namespace ProjectFinance.Domain.Dtos.Requests.Updates;

public class UpdateFinanceOptionScheduleRequest
{
    public int Id { get; set; }

    public int? FinanceOptionId { get; set; }
    public decimal? Cost { get; set; }

    public decimal? Repayment { get; set; }

    public decimal? Disbursement { get; set; }

    public DateOnly? Date { get; set; }
}