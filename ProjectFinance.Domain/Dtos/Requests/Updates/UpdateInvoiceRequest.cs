namespace ProjectFinance.Domain.Dtos.Requests.Updates;

public class UpdateInvoiceRequest
{
    public int Id { get; set; }

    public int? SupplierId { get; set; }

    public int? PurchaseOrderId { get; set; }

    public string? InvoiceNumber { get; set; }
}