namespace ProjectFinance.Domain.Dtos.Requests.Updates;

public class UpdatePurchaseOrderRequest
{
    public int Id { get; set; }

    public int? ProjectId { get; set; }

    public int? ActivityId { get; set; }

    public int? CostDetailId { get; set; }

    public int? SupplierId { get; set; }

    public DateOnly? Date { get; set; }

    public decimal? Amount { get; set; }

    public string? PONumber { get; set; }
}