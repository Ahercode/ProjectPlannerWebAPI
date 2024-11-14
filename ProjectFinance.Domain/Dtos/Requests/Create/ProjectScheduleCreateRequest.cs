namespace ProjectFinance.Domain.Dtos.Requests;

public class ProjectScheduleCreateRequest
{
    public int? ProjectId { get; set; }
    
    public string? InvoiceNumber { get; set; }
    
    public decimal? Amount { get; set; }
    
    public DateOnly? Date { get; set; }
}