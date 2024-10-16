namespace ProjectFinance.Domain.Dtos.Requests;

public class CommonInvoiceRequest
{
    public int Id { get; set; }

    public int? SupplierId { get; set; }

    public int? PurchaseOrderId { get; set; }
}