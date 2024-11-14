namespace ProjectFinance.Domain.Dtos.Requests.Updates;

public class UpdatePaymentRequest
{
    public int Id { get; set; }
    
    public DateOnly? Date { get; set; }
    
    public decimal? Amount { get; set; }
    
    public string? PayeeName { get; set; }

    public string? InvoiceNumber { get; set; }
}