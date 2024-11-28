namespace ProjectFinance.Domain.Dtos.Responses;

public class PODetailResponse
{
    public int Id { get; set; }

    public int? CostDetailId { get; set; }
    
    public string? CostDetailName { get; set; }
    
    public int? PurchaseOrderId { get; set; }

    public int? Quantity { get; set; }
    
    public decimal? Amount { get; set; }
    
    public DateOnly? Date { get; set; }
}