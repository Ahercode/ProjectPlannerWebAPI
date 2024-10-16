namespace ProjectFinance.Domain.Dtos.Requests;

public class CommonPurchaseOrderRequest
{
    public int Id { get; set; }
    
    
    public int? ActivityId { get; set; }

    public int? CostDetailId { get; set; }

    public int? SupplierId { get; set; }
}