namespace ProjectFinance.Domain.Dtos.Responses.invoice;

public class InvoiceResponse
{
    public int Id { get; set; }
    
    public int? SupplierId { get; set; }
    
    public int? PurchaseOrderId { get; set; }
    
    public string? InvoiceNumber { get; set; }
}