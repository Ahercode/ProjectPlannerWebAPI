namespace ProjectFinance.Domain.Dtos.Requests;

public class PurchaseOrderRequest
{

    public int? ProjectId { get; set; }

    public int? ActivityId { get; set; }

    public int? CostDetailId { get; set; }

    public int? SupplierId { get; set; }

    public DateOnly? Date { get; set; }

    public decimal? Amount { get; set; }

    public string? PONumber { get; set; }
    
}