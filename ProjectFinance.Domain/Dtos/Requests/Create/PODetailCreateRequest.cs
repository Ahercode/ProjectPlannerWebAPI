namespace ProjectFinance.Domain.Dtos.Requests;

public class PODetailCreateRequest
{
    public int? CostDetailId { get; set; }

    public int? PurchaseOrderId { get; set; }

    public int? Quantity { get; set; }
    
    public decimal? Amount { get; set; }
}