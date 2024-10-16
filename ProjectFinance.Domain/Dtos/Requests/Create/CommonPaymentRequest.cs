namespace ProjectFinance.Domain.Dtos.Requests;

public class CommonPaymentRequest
{
    public int Id { get; set; }
    
    public string? PayeeName { get; set; }

    public string? InvoiceNumber { get; set; }
}