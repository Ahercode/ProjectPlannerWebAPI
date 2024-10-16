namespace ProjectFinance.Domain.Dtos.Responses.purchaseorder;

public class PurchaseOrderResponse
{
    public int Id { get; set; }

    public int? ProjectId { get; set; }
    
    public int? ActivityId { get; set; }

    public int? CostDetailId { get; set; }

    public int? SupplierId { get; set; }
}