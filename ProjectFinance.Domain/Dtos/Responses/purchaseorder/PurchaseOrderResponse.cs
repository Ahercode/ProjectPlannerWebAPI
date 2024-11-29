namespace ProjectFinance.Domain.Dtos.Responses.purchaseorder;

public class PurchaseOrderResponse
{
    public int Id { get; set; }

    public int? ProjectId { get; set; }
    public string? ProjectName { get; set; }

    public int? ActivityId { get; set; }
    public string? ActivityName { get; set; }

    public int? SupplierId { get; set; }
    public string? SupplierName { get; set; }

    public DateOnly? Date { get; set; }

    public string? PONumber { get; set; }
    
    public string? Reference { get; set; }

    public string? FileName { get; set; }
}