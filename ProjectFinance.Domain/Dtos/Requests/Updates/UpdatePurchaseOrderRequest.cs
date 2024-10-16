namespace ProjectFinance.Domain.Dtos.Requests.Updates;

public class UpdatePurchaseOrderRequest
{
    public int Id { get; set; }
    
    
    public int? ActivityId { get; set; }

    public int? CostDetailId { get; set; }

    public int? SupplierId { get; set; }
}