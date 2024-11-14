namespace ProjectFinance.Domain.Dtos.Responses.invoice;

public class InvoiceResponse
{
    public int Id { get; set; }
    
    public int? SupplierId { get; set; }
    
    public string? SupplierName { get; set; }
    
    public int? PurchaseOrderId { get; set; }
    
    public string? PurchaseOrderNumber { get; set; }
    
    public string? InvoiceNumber { get; set; }
    public int? ProjectId { get; set; }
    public string? ProjectName { get; set; }
}