namespace ProjectFinance.Domain.Dtos.Requests;

public class PaymentCreateRequest
{
    public DateOnly? Date { get; set; }
    
    public decimal? Amount { get; set; }
    
    public string? PayeeName { get; set; }

    public string? InvoiceNumber { get; set; }
}