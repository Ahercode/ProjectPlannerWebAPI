namespace ProjectFinance.Domain.Dtos.Requests;

public class InvoiceCreateRequest
{

    public int? SupplierId { get; set; }

    public int? PurchaseOrderId { get; set; }
    
    public string? InvoiceNumber { get; set; }
    public int? ProjectId { get; set; }
}