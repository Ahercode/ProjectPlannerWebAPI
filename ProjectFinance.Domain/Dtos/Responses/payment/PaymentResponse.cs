using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectFinance.Domain.Dtos.Responses.payment;

public class PaymentResponse
{
    
    public int Id { get; set; }
    
    public DateOnly? Date { get; set; }
    public decimal? Amount { get; set; }
    public string? PayeeName { get; set; }
    
    public string? InvoiceNumber { get; set; }
}