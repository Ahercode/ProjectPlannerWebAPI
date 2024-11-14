namespace ProjectFinance.Domain.Dtos.Responses.projectschedule;

public class ProjectScheduleResponse
{
    public int Id { get; set; }
    
    public int? ProjectId { get; set; }
    
    public string? InvoiceNumber { get; set; }
    
    public decimal? Amount { get; set; }
    
    public DateOnly? Date { get; set; }
    
}