using Microsoft.AspNetCore.Http;

namespace ProjectFinance.Domain.Dtos.Requests;

public class PurchaseOrderCreateRequest
{

    public int? ProjectId { get; set; }

    public int? ActivityId { get; set; }

    public int? SupplierId { get; set; }

    public DateOnly? Date { get; set; }

    public string? PONumber { get; set; }
    
    public string? Reference { get; set; }
    
    public string? FileName { get; set; }
    
    public IFormFile? FileObject { get; set; }
    
}